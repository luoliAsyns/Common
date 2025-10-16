using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ConsumeInfo.Sexytea
{
    public class Account
    {
        public string OpenId { get; set; }
        public string UnionId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredTime { get; set; }
        public DateTime GeneratedTime { get; set; }

    }
}
