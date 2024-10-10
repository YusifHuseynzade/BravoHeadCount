using MediatR;
using ScheduledDataDetails.Commands.Response;

namespace ScheduledDataDetails.Commands.Response;

public class CreateScheduledDataCommandResponse
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }

}
