using EmployeeDetails.Commands.Request;
using EmployeeDetails.Commands.Response;
using AutoMapper;
using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeDetails.Handlers.CommandHandlers
{
    public class UpdateEmployeeImageCommandHandler : IRequestHandler<UpdateEmployeeImageCommandRequest, UpdateEmployeeImageCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IHostEnvironment _env;
        private readonly IOptions<FileSettings> _settings;

        public UpdateEmployeeImageCommandHandler(IEmployeeRepository employeeRepository, IHostEnvironment env, IOptions<FileSettings> settings, IHttpContextAccessor httpAccessor)
        {
            _employeeRepository = employeeRepository;
            _env = env;
            _settings = settings;
            _httpAccessor = httpAccessor;
        }

        public async Task<UpdateEmployeeImageCommandResponse> Handle(UpdateEmployeeImageCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateEmployeeImageCommandResponse();

            try
            {
                // Mevcut çalışanı getir
                var existingEmployee = await _employeeRepository.GetAsync(e => e.Id == request.Id);

                if (existingEmployee == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Employee not found.";
                    return response;
                }

                if (request.ImageFile != null && request.ImageFile.Length > 0)
                {
                    // Önceki resmi sil
                    if (!string.IsNullOrEmpty(existingEmployee.Image))
                    {
                        IFormFileExtensions.Delete(_settings.Value.Path, _settings.Value.Employee, existingEmployee.Image);
                    }

                    // Yeni resmi kaydet
                    string imagePath = await request.ImageFile.SaveAsync(_settings.Value.Path, _settings.Value.Employee);
                    if (imagePath != null)
                    {
                        existingEmployee.Image = imagePath;

                        // Base URL ile tam resim yolunu oluştur
                        var imageUrl = CreateImageUrl(imagePath);
                        response.ImageUrl = imageUrl;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Failed to save the image.";
                        return response;
                    }
                }

                // Çalışanı güncelle
                await _employeeRepository.UpdateAsync(existingEmployee);
                await _employeeRepository.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Employee image updated successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "An error occurred while updating the employee image.";
            }

            return response;
        }

        // Resim URL'sini dinamik olarak oluşturan yardımcı metod
        private string CreateImageUrl(string imagePath)
        {
            var request = _httpAccessor.HttpContext?.Request;
            if (request == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            return $"{request.Scheme}://{request.Host}/{imagePath}";
        }


    }
}
