using MediatR;
using TransactionPageDetails.Commands.Response;

namespace TransactionPageDetails.Commands.Request;

public class UpdateTransactionPageCommandRequest : IRequest<UpdateTransactionPageCommandResponse>
{
    public int Id { get; set; }
}

