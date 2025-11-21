using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Admin
{
    public class BannedBranchesRequest
    {

        public string targetProxy { get; set; }
            
        public string[] branches { get; set; }
    }
}
