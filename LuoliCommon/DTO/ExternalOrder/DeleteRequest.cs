using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ExternalOrder
{
    public class DeleteRequest
    {
        public string from_platform { get; set; }
        public string tid { get; set; }
    }
}
