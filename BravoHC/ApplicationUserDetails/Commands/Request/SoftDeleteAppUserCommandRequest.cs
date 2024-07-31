using ApplicationUserDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.Commands.Request
{
    public class SoftDeleteAppUserCommandRequest : IRequest<SoftDeleteAppUserCommandResponse>
    {
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
