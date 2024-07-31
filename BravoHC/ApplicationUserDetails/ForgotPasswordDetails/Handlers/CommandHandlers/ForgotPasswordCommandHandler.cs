using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using System.Net.Mail;
using System.Net;
using Core.Helpers;
using ApplicationUserDetails.ForgotPasswordDetails.Commands.Request;
using ApplicationUserDetails.ForgotPasswordDetails.Commands.Response;
using Domain.IServices;

namespace ApplicationUserDetails.ForgotPasswordDetails.Handlers.CommandHandlers
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, ForgotPasswordCommandResponse>
    {
        private readonly IAppUserRepository _repository;
        private readonly ISmsService _smsService;

        public ForgotPasswordCommandHandler(IAppUserRepository repository, ISmsService smsService)
        {
            _repository = repository;
            _smsService = smsService;
        }

        public async Task<ForgotPasswordCommandResponse> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _repository.FirstOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);

            if (user != null)
            {
                string otpToken = (RandomGenerator.NextInt() % 10000).ToString("0000");

                user.OTPToken = otpToken;
                user.OTPTokenCreated = DateTime.Now.ToUniversalTime();
                user.OTPTokenExpires = DateTime.Now.AddMinutes(20).ToUniversalTime();

                await _repository.CommitAsync();

                //await _smsService.SendOtpCode(request.PhoneNumber, otpToken);


                return new ForgotPasswordCommandResponse
                {
                    IsSuccess = true,
                    Message = "Security code sent to email"
                };
            }

            return new ForgotPasswordCommandResponse
            {
                IsSuccess = false,
                Message = "User with this email address doens't exist"
            };
        }
      
    }
}

public static class RandomGenerator
{
    private static readonly ThreadLocal<System.Security.Cryptography.RandomNumberGenerator> crng = new ThreadLocal<System.Security.Cryptography.RandomNumberGenerator>(System.Security.Cryptography.RandomNumberGenerator.Create);
    private static readonly ThreadLocal<byte[]> bytes = new ThreadLocal<byte[]>(() => new byte[sizeof(int)]);
    public static int NextInt()
    {
        crng.Value.GetBytes(bytes.Value);
        return BitConverter.ToInt32(bytes.Value, 0) & int.MaxValue;
    }
    public static double NextDouble()
    {
        while (true)
        {
            long x = NextInt() & 0x001FFFFF;
            x <<= 31;
            x |= (long)NextInt();
            double n = x;
            const double d = 1L << 52;
            double q = n / d;
            if (q != 1.0)
                return q;
        }
    }
}