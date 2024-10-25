using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyOrderDetails.Queries.Response;

public class GetByIdMoneyOrderQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProjectName { get; set; }
    public string BranchName { get; set; }
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
    public string SealNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}