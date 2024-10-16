using AutoMapper;
using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PositionDetails.Commands.Request;
using PositionDetails.Commands.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PositionDetails.Handlers.CommandHandlers
{
    public class UpdatePositionFileCommandHandler : IRequestHandler<UpdatePositionFileCommandRequest, UpdatePositionFileCommandResponse>
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IHostEnvironment _env;
        private readonly IOptions<FileSettings> _settings;

        public UpdatePositionFileCommandHandler(
            IPositionRepository positionRepository,
            IHostEnvironment env,
            IOptions<FileSettings> settings)
        {
            _positionRepository = positionRepository;
            _env = env;
            _settings = settings;
        }

        public async Task<UpdatePositionFileCommandResponse> Handle(UpdatePositionFileCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdatePositionFileCommandResponse();

            try
            {
                // Mevcut pozisyonu getir
                var existingPosition = await _positionRepository.GetAsync(p => p.Id == request.Id);

                if (existingPosition == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Position not found.";
                    return response;
                }

                if (request.JobDescriptionFile != null && request.JobDescriptionFile.Length > 0)
                {
                    // Önceki dosyayı sil
                    if (!string.IsNullOrEmpty(existingPosition.JobDescription))
                    {
                        IFormFileExtensions.Delete(_settings.Value.Path, _settings.Value.Position, existingPosition.JobDescription);
                    }

                    // Yeni dosyayı kaydet
                    string filePath = await request.JobDescriptionFile.SaveAsync(_settings.Value.Path, _settings.Value.Position);

                    if (filePath != null)
                    {
                        existingPosition.JobDescription = filePath;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Failed to save the job description file.";
                        return response;
                    }
                }

                // Pozisyonu güncelle
                await _positionRepository.UpdateAsync(existingPosition);
                await _positionRepository.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Job description file updated successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "An error occurred while updating the job description file.";
            }

            return response;
        }
    }
}
