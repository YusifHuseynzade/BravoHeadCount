using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using SickLeaveDetails.Commands.Request;
using SickLeaveDetails.Commands.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SickLeaveDetails.Handlers.CommandHandlers
{
    public class CreateSickLeaveCommandHandler : IRequestHandler<CreateSickLeaveCommandRequest, CreateSickLeaveCommandResponse>
    {
        private readonly ISickLeaveRepository _sickLeaveRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CreateSickLeaveCommandHandler(ISickLeaveRepository sickLeaveRepository, IEmployeeRepository employeeRepository)
        {
            _sickLeaveRepository = sickLeaveRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<CreateSickLeaveCommandResponse> Handle(CreateSickLeaveCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Employee kontrolü: EmployeeId geçerli mi?
                var employeeExists = await _employeeRepository.GetAsync(e => e.Id == request.EmployeeId);
                if (employeeExists == null)
                {
                    return new CreateSickLeaveCommandResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "The specified employee does not exist."
                    };
                }

                // Tarih aralığını kontrol et
                var dateDifference = (request.EndDate - request.StartDate).TotalDays;
                if (dateDifference >= 14)
                {
                    return new CreateSickLeaveCommandResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "The sick leave period cannot exceed 14 days."
                    };
                }

                // Yeni bir SickLeave nesnesi oluştur ve gelen request'ten verileri ata
                var sickLeave = new SickLeave
                {
                    EmployeeId = request.EmployeeId,
                    StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc)
                };

                // Veritabanına ekle ve işlemi kaydet
                await _sickLeaveRepository.AddAsync(sickLeave);
                await _sickLeaveRepository.CommitAsync();

                // İşlem başarılı olduğunda olumlu bir yanıt döndür
                return new CreateSickLeaveCommandResponse
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                // İşlem sırasında bir hata oluşursa, hata mesajı ile birlikte olumsuz yanıt döndür
                return new CreateSickLeaveCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessage = $"An error occurred while creating the sick leave: {ex.Message}"
                };
            }
        }
    }
}
