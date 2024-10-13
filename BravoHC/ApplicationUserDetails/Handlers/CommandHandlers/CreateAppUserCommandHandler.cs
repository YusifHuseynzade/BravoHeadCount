using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using MediatR;

namespace Application.ApplicationUserDetails.Handlers.CommandHandlers
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, CreateAppUserCommandResponse>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IAppUserRoleRepository _userRoleRepository; // AppUserRole repository eklendi
        private readonly IRoleRepository _roleRepository;
        private readonly ISmsService _smsService;

        public CreateAppUserCommandHandler(
            IAppUserRepository userRepository,
            IAppUserRoleRepository userRoleRepository,  // Dependency eklendi
            IRoleRepository roleRepository,
            ISmsService smsService)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository; // Dependency atandı
            _roleRepository = roleRepository;
            _smsService = smsService;
        }

        public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateAppUserCommandResponse();

            // Yeni kullanıcı oluşturma
            var newUser = new AppUser();
            newUser.SetCreateUserDetails(request.UserName, request.FullName, request.PhoneNumber, request.Password, request.Email);

            // Kullanıcı adı kontrolü
            ValidationChecker.ValidateUserName(request.UserName);
            if (await _userRepository.IsExistAsync(u => u.UserName == request.UserName))
            {
                return new CreateAppUserCommandResponse
                {
                    IsSuccess = false,
                    Message = $"An account with username \"{request.UserName}\" already exists. Please use a different username."
                };
            }

            // Email kontrolü
            ValidationChecker.ValidateUserEmailAddress(request.Email);
            if (await _userRepository.IsExistAsync(u => u.Email == request.Email))
            {
                return new CreateAppUserCommandResponse
                {
                    IsSuccess = false,
                    Message = $"An account with email \"{request.Email}\" already exists. Please use a different email."
                };
            }

            // Kullanıcının eklenmesi
            await _userRepository.AddAsync(newUser);
            await _userRepository.CommitAsync();

            // Rollerin eklenmesi
            if (request.RoleIds != null && request.RoleIds.Any())
            {
                foreach (var roleId in request.RoleIds)
                {
                    var roleExists = await _roleRepository.IsExistAsync(r => r.Id == roleId);
                    if (!roleExists)
                    {
                        return new CreateAppUserCommandResponse
                        {
                            IsSuccess = false,
                            Message = $"Role with Id \"{roleId}\" does not exist. Please provide a valid role."
                        };
                    }

                    // Kullanıcıya rol ataması
                    var userRole = new AppUserRole
                    {
                        AppUserId = newUser.Id,
                        RoleId = roleId
                    };

                    await _userRoleRepository.AddAsync(userRole);
                }

                await _userRoleRepository.CommitAsync();
            }

            // SMS ile bilgilendirme (eğer istenirse)
            // await _smsService.SendGeneratedPassword(newUser.PhoneNumber, newUser.UserName, generatedPassword);

            response.IsSuccess = true;
            response.Message = "User created successfully.";
            return response;
        }
    }
}
