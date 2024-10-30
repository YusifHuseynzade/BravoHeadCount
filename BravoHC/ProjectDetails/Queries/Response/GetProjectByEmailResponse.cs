using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Response
{
    public class GetProjectByEmailResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ProjectCode { get; set; }
        public string SettingType { get; set; } // StoreManagerMail veya StoreUserMail bilgisi
        public GeneralSetting GeneralSettings { get; set; }
        public List<FinanceOperationResponse> FinanceOperations { get; set; } // Yeni eklenen alan
    }
}
