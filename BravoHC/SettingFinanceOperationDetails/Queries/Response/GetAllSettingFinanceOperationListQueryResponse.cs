using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingFinanceOperationDetails.Queries.Response
{
    public class GetAllSettingFinanceOperationListQueryResponse
    {
        public int TotalSettingFinanceOperationCount { get; set; }
        public List<GetAllSettingFinanceOperationQueryResponse> SettingFinanceOperations { get; set; }
    }
}
