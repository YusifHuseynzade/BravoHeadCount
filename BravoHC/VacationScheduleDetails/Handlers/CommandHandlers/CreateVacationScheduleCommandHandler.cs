using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using VacationScheduleDetails.Commands.Request;
using VacationScheduleDetails.Commands.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VacationScheduleDetails.Handlers.CommandHandlers
{
    public class CreateVacationScheduleCommandHandler : IRequestHandler<CreateVacationScheduleCommandRequest, CreateVacationScheduleCommandResponse>
    {
        private readonly IVacationScheduleRepository _vacationScheduleRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CreateVacationScheduleCommandHandler(IVacationScheduleRepository vacationScheduleRepository, IEmployeeRepository employeeRepository)
        {
            _vacationScheduleRepository = vacationScheduleRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<CreateVacationScheduleCommandResponse> Handle(CreateVacationScheduleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Employee kontrolü: EmployeeId geçerli mi?
                var employeeExists = await _employeeRepository.GetAsync(e => e.Id == request.EmployeeId);
                if (employeeExists == null)
                {
                    return new CreateVacationScheduleCommandResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "The specified employee does not exist."
                    };
                }

                // Geçerli tarih ve gelecek ayları belirle
                var currentDate = DateTime.UtcNow;
                var nextMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1);
                var monthAfterNext = nextMonth.AddMonths(1);

                if (currentDate.Day <= 9)
                {
                    // Ayın 25'ine kadar bir sonraki ay veya ondan sonraki ay için planlama yapılabilir
                    if ((request.StartDate.Year == nextMonth.Year && request.StartDate.Month == nextMonth.Month) ||
                        (request.StartDate.Year == monthAfterNext.Year && request.StartDate.Month == monthAfterNext.Month))
                    {
                        // İşlem başarılı bir şekilde devam eder
                    }
                    else
                    {
                        return new CreateVacationScheduleCommandResponse
                        {
                            IsSuccess = false,
                            ErrorMessage = "Before the 9th of the current month, you can only create a vacation schedule for the next month or the month after the next."
                        };
                    }
                }
                else
                {
                    // Ayın 25'ini geçtikten sonra, bir sonraki aydan sonraki herhangi bir ay için planlama yapılabilir
                    if (request.StartDate < monthAfterNext)
                    {
                        return new CreateVacationScheduleCommandResponse
                        {
                            IsSuccess = false,
                            ErrorMessage = "After the 9th of the current month, you cannot create a vacation schedule for the next month. Please select a later month."
                        };
                    }
                }

                // Yeni bir VacationSchedule nesnesi oluştur ve gelen request'ten verileri ata
                var vacationSchedule = new VacationSchedule
                {
                    EmployeeId = request.EmployeeId,
                    StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc)
                };

                // Veritabanına ekle ve işlemi kaydet
                await _vacationScheduleRepository.AddAsync(vacationSchedule);
                await _vacationScheduleRepository.CommitAsync();

                // İşlem başarılı olduğunda olumlu bir yanıt döndür
                return new CreateVacationScheduleCommandResponse
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                // İşlem sırasında bir hata oluşursa, hata mesajı ile birlikte olumsuz yanıt döndür
                return new CreateVacationScheduleCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessage = $"An error occurred while creating the vacation schedule: {ex.Message}"
                };
            }
        }
    }
}
