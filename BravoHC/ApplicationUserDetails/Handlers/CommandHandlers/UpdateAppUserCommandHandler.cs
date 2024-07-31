using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using AutoMapper;
using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationUserDetails.Handlers.CommandHandlers
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommandRequest, UpdateAppUserCommandResponse>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHostEnvironment _env;

        public UpdateAppUserCommandHandler(IAppUserRepository userRepository, IRoleRepository roleRepository, IHostEnvironment env)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _env = env;
        }

        public async Task<UpdateAppUserCommandResponse> Handle(UpdateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateAppUserCommandResponse();

            try
            {
                // Retrieve existing user
                var existingUser = await _userRepository.GetAsync(u => u.Id == request.Id);

                existingUser.SetUpdateUserDetails(request.UserName, request.FullName, request.PhoneNumber, request.Email);
                
                // Update user
                await _userRepository.UpdateAsync(existingUser);
                await _userRepository.CommitAsync();

                // Assign roles if provided

                response.IsSuccess = true;
                response.Message = "User updated successfully.";
            }
            catch (Exception ex)
            {
                // Log the exception
                response.IsSuccess = false;
                response.Message = "An error occurred while updating the user.";
                // You can log the exception here
            }

            return response;
        }
    }
}
