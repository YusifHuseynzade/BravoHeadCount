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
    public class UpdateSickLeaveCommandHandler : IRequestHandler<UpdateSickLeaveCommandRequest, UpdateSickLeaveCommandResponse>
    {
        private readonly ISickLeaveRepository _sickLeaveRepository;

        public UpdateSickLeaveCommandHandler(ISickLeaveRepository sickLeaveRepository)
        {
            _sickLeaveRepository = sickLeaveRepository;
        }

        public async Task<UpdateSickLeaveCommandResponse> Handle(UpdateSickLeaveCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateSickLeaveCommandResponse();

            try
            {
                // Mevcut SickLeave kaydını al
                var sickLeave = await _sickLeaveRepository.GetAsync(p => p.Id == request.Id);
                if (sickLeave != null)
                {
                    // Geçmiş bir tarihe sick leave güncellemesini engelle (StartDate ve EndDate kontrolü)
                    if (request.StartDate < DateTime.UtcNow || request.EndDate < DateTime.UtcNow)
                    {
                        response.IsSuccess = false;
                        response.Message = "Sick leave cannot be updated with a past date.";
                        return response;
                    }

                    // Tarih aralığını kontrol et
                    var dateDifference = (request.EndDate - request.StartDate).TotalDays;
                    if (dateDifference >= 14)
                    {
                        response.IsSuccess = false;
                        response.Message = "The sick leave period cannot exceed 14 days.";
                        return response;
                    }

                    // Gelen request bilgilerini mevcut kayda ata
                    sickLeave.StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
                    sickLeave.EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

                    // Veritabanında güncelle
                    await _sickLeaveRepository.UpdateAsync(sickLeave);
                    await _sickLeaveRepository.CommitAsync();

                    response.IsSuccess = true;
                    response.Message = "SickLeave updated successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "SickLeave not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error updating sickLeave: {ex.Message}";
            }

            return response;
        }
    }
}
