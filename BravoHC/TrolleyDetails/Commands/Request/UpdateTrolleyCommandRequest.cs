using MediatR;
using TrolleyDetails.Commands.Response;

namespace TrolleyDetails.Commands.Request;

public class UpdateTrolleyCommandRequest : IRequest<UpdateTrolleyCommandResponse>
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int TrolleyTypeId { get; set; }
    public DateTime CountDate { get; set; }
    public int WorkingTrolleysCount { get; set; }
    public int BrokenTrolleysCount { get; set; }
}

