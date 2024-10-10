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
    public class UpdateVacationScheduleCommandHandler : IRequestHandler<UpdateVacationScheduleCommandRequest, UpdateVacationScheduleCommandResponse>
    {
        private readonly IVacationScheduleRepository _vacationScheduleRepository;

        public UpdateVacationScheduleCommandHandler(IVacationScheduleRepository vacationScheduleRepository)
        {
            _vacationScheduleRepository = vacationScheduleRepository;
        }

        public async Task<UpdateVacationScheduleCommandResponse> Handle(UpdateVacationScheduleCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateVacationScheduleCommandResponse();

            try
            {
                // Mevcut VacationSchedule kaydını al
                var vacationSchedule = await _vacationScheduleRepository.GetAsync(p => p.Id == request.Id);
                if (vacationSchedule != null)
                {
                    // Geçerli tarih ve gelecek ayları belirle
                    var currentDate = DateTime.UtcNow;
                    var nextMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1);
                    var monthAfterNext = nextMonth.AddMonths(1);

                    if (currentDate.Day <= 25)
                    {
                        // Ayın 25'ine kadar bir sonraki ay veya ondan sonraki ay için update yapılabilir
                        if ((request.StartDate.Year == nextMonth.Year && request.StartDate.Month == nextMonth.Month) ||
                            (request.StartDate.Year == monthAfterNext.Year && request.StartDate.Month == monthAfterNext.Month))
                        {
                            // İşlem başarılı bir şekilde devam eder
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Before the 25th of the current month, you can only update a vacation schedule for the next month or the month after the next.";
                            return response;
                        }
                    }
                    else
                    {
                        // Ayın 25'ini geçtikten sonra, bir sonraki aydan sonraki herhangi bir ay için update yapılabilir
                        if (request.StartDate < monthAfterNext)
                        {
                            response.IsSuccess = false;
                            response.Message = "After the 25th of the current month, you cannot update the vacation schedule for the next month. Please select a later month.";
                            return response;
                        }
                    }

                    // Gelen request bilgilerini mevcut kayda ata
                    vacationSchedule.StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
                    vacationSchedule.EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);
                    vacationSchedule.EmployeeId = request.EmployeeId;

                    // Veritabanında güncelle
                    await _vacationScheduleRepository.UpdateAsync(vacationSchedule);
                    await _vacationScheduleRepository.CommitAsync();

                    response.IsSuccess = true;
                    response.Message = "VacationSchedule updated successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "VacationSchedule not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error updating VacationSchedule: {ex.Message}";
            }

            return response;
        }
    }
}
