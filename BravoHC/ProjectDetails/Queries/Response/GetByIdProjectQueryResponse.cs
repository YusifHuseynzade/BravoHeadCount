using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Response;

public class GetByIdProjectQueryResponse
{
	public int Id { get; set; }
    public string ProjectCode { get; set; }
    public string ProjectName { get; set; }
    public bool? IsStore { get; set; }
    public bool? IsHeadOffice { get; set; }
    public bool? IsActive { get; set; }
    public string Format { get; set; }
    public string FunctionalArea { get; set; }
    public string OperationDirector { get; set; }
    public string OperationDirectorMail { get; set; }
    public string AreaManager { get; set; }
    public string AreaManagerBadge { get; set; }
    public string AreaManagerMail { get; set; }
    public string StoreManagerMail { get; set; }
    public string Recruiter { get; set; }
    public string RecruiterMail { get; set; }
    public List<int> SectionIds { get; set; }
    public string? StoreOpeningDate { get; set; }
    public string? StoreClosedDate { get; set; }
}
