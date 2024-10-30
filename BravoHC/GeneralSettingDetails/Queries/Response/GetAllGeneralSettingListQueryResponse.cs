using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralSettingDetails.Queries.Response
{
    public class GetAllGeneralSettingListQueryResponse
    {
        public int TotalGeneralSettingCount { get; set; }
        public List<GetAllGeneralSettingQueryResponse> GeneralSettings { get; set; }
    }
}
