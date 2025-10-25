using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ConsumeInfo.Sexytea
{
    public class Token
    {
        public Token()
        {
            
        }
        public string token { get; set; }
        public string status { get; set; }
        public DateTime exp { get; set; } 
        public string code { get; set; }
        public string openId { get; set; }
        public string unionId { get; set; }


    }
}
