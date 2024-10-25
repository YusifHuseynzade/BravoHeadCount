using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncashmentDetails.Queries.Response;

public class GetByIdEncashmentQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProjectName { get; set; }  // Proje adı
    public string BranchName { get; set; }   // Şube adı
    public float AmountFromSales { get; set; }
    public float AmountFoundOnSite { get; set; }
    public float SafeSurplus { get; set; }
    public float TotalAmount { get; set; }
    public string SealNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public List<string> Attachments { get; set; }  // Dosya URL'leri
}