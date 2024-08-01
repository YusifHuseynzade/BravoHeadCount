using MediatR;
using SubSectionDetails.Commands.Response;

namespace SubSectionDetails.Commands.Request;

public class DeleteSubSectionCommandRequest : IRequest<DeleteSubSectionCommandResponse>
{
    public int Id { get; set; }

}
