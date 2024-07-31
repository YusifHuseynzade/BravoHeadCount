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
        private readonly IRoleRepository _roleRepository;
        private readonly ISmsService _smsService;

        public CreateAppUserCommandHandler(IAppUserRepository userRepository, IRoleRepository roleRepository, ISmsService smsService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _smsService = smsService;
        }

        public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateAppUserCommandResponse();

            var newUser = new AppUser();
            newUser.SetCreateUserDetails(request.UserName, request.FullName, request.PhoneNumber, request.Password, request.Email);
            newUser.RoleId = request.RoleId;

            var roleExists = await _roleRepository.IsExistAsync(r => r.Id == request.RoleId);
            if (!roleExists)
            {
                return new CreateAppUserCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Role with Id \"{request.RoleId}\" does not exist. Please provide a valid role."
                };
            }

            ValidationChecker.ValidateUserName(request.UserName);
            if (await _userRepository.IsExistAsync(u => u.UserName == request.UserName))
            {
                return new CreateAppUserCommandResponse
                {
                    IsSuccess = false,
                    Message = $"An account with username \"{request.UserName}\" already exists. Please use a different username."
                };
            }

            ValidationChecker.ValidateUserEmailAddress(request.Email);
            if (await _userRepository.IsExistAsync(u => u.Email == request.Email))
            {
                return new CreateAppUserCommandResponse
                {
                    IsSuccess = false,
                    Message = $"An account with username \"{request.Email}\" already exists. Please use a different username."
                };
            }

            //string generatedPassword = PasswordHelper.GenerateRandomPassword();
            //newUser.Password = generatedPassword;

            await _userRepository.AddAsync(newUser);
            await _userRepository.CommitAsync();


            //await _smsService.SendGeneratedPassword(newUser.PhoneNumber, newUser.UserName, generatedPassword);
            response.IsSuccess = true;
            response.Message = "User created successfully.";

            return response;
        }
    }
}
