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
        public string Status { get; set; }
        public string Code { get; set; }   
        public DateTime Exp { get; set; }
        public string Phone { get; set; }


        //luoli加的
        public int TodayOrdersCount { get; set; }
        public bool Enable { get; set; }

    }
}
