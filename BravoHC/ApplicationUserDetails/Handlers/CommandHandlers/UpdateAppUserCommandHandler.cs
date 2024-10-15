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
        private readonly IAppUserRoleRepository _userRoleRepository;  // AppUserRole repository eklendi
        private readonly IRoleRepository _roleRepository;
        private readonly IHostEnvironment _env;

        public UpdateAppUserCommandHandler(IAppUserRepository userRepository, IAppUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IHostEnvironment env)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;  // Dependency atandı
            _roleRepository = roleRepository;
            _env = env;
        }

        public async Task<UpdateAppUserCommandResponse> Handle(UpdateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateAppUserCommandResponse();

            try
            {
                // Var olan kullanıcıyı getir
                var existingUser = await _userRepository.GetAsync(u => u.Id == request.Id);

                if (existingUser == null)
                {
                    return new UpdateAppUserCommandResponse
                    {
                        IsSuccess = false,
                        Message = $"User with Id \"{request.Id}\" not found."
                    };
                }

                // Kullanıcı bilgilerini güncelle
                existingUser.SetUpdateUserDetails(request.UserName, request.FullName, request.PhoneNumber, request.Email);
                await _userRepository.UpdateAsync(existingUser);
                await _userRepository.CommitAsync();

                // Rol atama işlemi (varsa)
                if (request.RoleIds != null && request.RoleIds.Any())
                {
                    // Mevcut rollerin silinmesi (istenirse)
                    var currentRoles = await _userRoleRepository.GetListAsync(r => r.AppUserId == existingUser.Id);
                    foreach (var userRole in currentRoles)
                    {
                        await _userRoleRepository.DeleteAsync(userRole);
                    }
                    await _userRoleRepository.CommitAsync();

                    // Yeni rollerin eklenmesi
                    foreach (var roleId in request.RoleIds)
                    {
                        var roleExists = await _roleRepository.IsExistAsync(r => r.Id == roleId);
                        if (!roleExists)
                        {
                            return new UpdateAppUserCommandResponse
                            {
                                IsSuccess = false,
                                Message = $"Role with Id \"{roleId}\" does not exist. Please provide a valid role."
                            };
                        }

                        var newUserRole = new AppUserRole
                        {
                            AppUserId = existingUser.Id,
                            RoleId = roleId
                        };

                        await _userRoleRepository.AddAsync(newUserRole);
                    }

                    await _userRoleRepository.CommitAsync();
                }

                response.IsSuccess = true;
                response.Message = "User updated successfully.";
            }
            catch (Exception ex)
            {
                // Hata günlüğü eklenebilir
                response.IsSuccess = false;
                response.Message = "An error occurred while updating the user.";
            }

            return response;
        }
    }
}
