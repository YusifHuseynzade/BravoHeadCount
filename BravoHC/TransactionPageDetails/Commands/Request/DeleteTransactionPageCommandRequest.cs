using MediatR;
using TransactionPageDetails.Commands.Response;

namespace TransactionPageDetails.Commands.Request;

public class DeleteTransactionPageCommandRequest : IRequest<DeleteTransactionPageCommandResponse>
{
    public int Id { get; set; }
}
