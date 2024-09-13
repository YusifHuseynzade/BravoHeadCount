using Domain.Entities;
using Domain.IRepositories;
using EmployeeDetails.Commands.Request;
using EmployeeDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Handlers.CommandHandlers
{
    public class AddParentIdCommandHandler : IRequestHandler<AddParentIdCommandRequest, AddParentIdCommandResponse>
    {
        private readonly IHeadCountRepository _headCountRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AddParentIdCommandHandler(IHeadCountRepository headCountRepository, IEmployeeRepository employeeRepository)
        {
            _headCountRepository = headCountRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<AddParentIdCommandResponse> Handle(AddParentIdCommandRequest request, CancellationToken cancellationToken)
        {
            var failedEmployeeIds = new List<int>();
            var managers = new List<HeadCount>();

            try
            {
                // 1. İşlem yapılacak tüm employee'leri getir
                var employees = await _employeeRepository.GetAllAsync(e => request.EmployeeIds.Contains(e.Id));
                if (!employees.Any())
                {
                    return new AddParentIdCommandResponse
                    {
                        IsSuccess = false,
                        Message = "No employees found for the given employee IDs.",
                        FailedEmployeeIds = request.EmployeeIds
                    };
                }

                // 2. Her employee için ProjectId'yi bul ve Store Manager ya da Hypermarket Store Manager'ı bul
                foreach (var employee in employees)
                {
                    // Employee'nin HeadCount kaydını bul
                    var employeeHeadCount = await _headCountRepository.FirstOrDefaultAsync(hc => hc.EmployeeId == employee.Id);
                    if (employeeHeadCount == null)
                    {
                        failedEmployeeIds.Add(employee.Id);
                        continue;
                    }

                    // Bu employee'nin ProjectId'si ile aynı proje içinde olan manager'ları bul
                    var managersInProject = await _headCountRepository.GetAllAsync(hc =>
                        hc.ProjectId == employeeHeadCount.ProjectId &&
                        (hc.Employee.Position.Name == "Hypermarket Store Manager" || hc.Employee.Position.Name == "Store Manager"));

                    if (!managersInProject.Any())
                    {
                        failedEmployeeIds.Add(employee.Id);
                        continue;
                    }

                    // İlk bulduğumuz manager'ı ParentId olarak ekle
                    var manager = managersInProject.FirstOrDefault();
                    if (manager != null)
                    {
                        employeeHeadCount.ParentId = manager.Id;
                        await _headCountRepository.UpdateAsync(employeeHeadCount);
                    }
                    else
                    {
                        failedEmployeeIds.Add(employee.Id);
                    }
                }

                // Commit işlemi
                await _headCountRepository.CommitAsync();

                return new AddParentIdCommandResponse
                {
                    IsSuccess = true,
                    Message = "ParentId successfully updated for employees.",
                    FailedEmployeeIds = failedEmployeeIds
                };
            }
            catch (Exception ex)
            {
                return new AddParentIdCommandResponse
                {
                    IsSuccess = false,
                    Message = $"An error occurred while updating ParentId: {ex.Message}",
                    FailedEmployeeIds = failedEmployeeIds
                };
            }
        }
    }

}
