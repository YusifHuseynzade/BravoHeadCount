using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HeadCountBackgroundColor: BaseEntity
    {
        public string ColorHexCode { get; set; }
        public void SetDetail(string name)
        {
            this.ColorHexCode = name;
        }
    }
}
