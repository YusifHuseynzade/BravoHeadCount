using MediatR;
using MoneyOrderDetails.Commands.Response;

namespace MoneyOrderDetails.Commands.Request;

public class UpdateMoneyOrderCommandRequest : IRequest<UpdateMoneyOrderCommandResponse>
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int BranchId { get; set; }
    public int HundredAZN { get; set; }
    public float FiftyAZN { get; set; }
    public float TwentyAZN { get; set; }
    public float TenAZN { get; set; }
    public float FiveAZN { get; set; }
    public float OneAZN { get; set; }
    public float FiftyQapik { get; set; }
    public float TwentyQapik { get; set; }
    public float TenQapik { get; set; }
    public float FiveQapik { get; set; }
    public float ThreeQapik { get; set; }
    public float OneQapik { get; set; }
    public int TotalQuantity { get; set; }
    public int TotalAmount { get; set; }
    public string? Name { get; set; }
    public string SealNumber { get; set; }
}

