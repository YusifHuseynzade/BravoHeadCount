using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project: BaseEntity
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public bool? IsStore { get; set; }
        public bool? IsHeadOffice { get; set; }
        public bool? IsActive { get; set; }
        public string Format { get; set; }
        public string FunctionalArea {  get; set; }
        public string OperationDirector { get; set; }
        public string OperationDirectorMail { get; set; }
        public string AreaManager { get; set; }
        public string AreaManagerBadge { get; set; }
        public string AreaManagerMail { get; set; }
        public string StoreManagerMail { get; set; }
        public string Recruiter { get; set; }
        public string RecruiterMail { get; set; }
        public List<ProjectSections> ProjectSections { get; set; }
        public List<Employee> Employees { get; set; }
        public List<ScheduledData> ScheduledDatas { get; set; }
        public string? StoreOpeningDate { get; set; }
        public string? StoreClosedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public void SetDetails(string projectCode, string projectName, bool? isStore, bool? isHeadOffice, bool? isActive, string format, string functionalArea, string director,
            string directorEmail,
            string areaManager,
            string areaManagerEmail,
            string storeManagerEmail,
            string recruiter,
            string recruiterEmail)
        {
            ProjectCode = projectCode;
            ProjectName = projectName;
            IsStore = isStore;
            IsHeadOffice = isHeadOffice;
            IsActive = isActive;
            Format = format;
            FunctionalArea = functionalArea;
            OperationDirector = director;
            OperationDirectorMail = directorEmail;
            AreaManager = areaManager;
            AreaManagerMail = areaManagerEmail;
            StoreManagerMail = storeManagerEmail;
            Recruiter = recruiter;
            RecruiterMail = recruiterEmail;
        }
    }
}
