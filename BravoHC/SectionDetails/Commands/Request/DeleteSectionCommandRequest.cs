using MediatR;
using SectionDetails.Commands.Response;

namespace SectionDetails.Commands.Request;

public class DeleteSectionCommandRequest : IRequest<DeleteSectionCommandResponse>
{
    public int Id { get; set; }
}
