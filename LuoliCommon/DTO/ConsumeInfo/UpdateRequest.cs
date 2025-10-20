using LuoliCommon.DTO.Coupon;
using LuoliCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ConsumeInfo
{
    public class UpdateRequest
    {
        public ConsumeInfoDTO CI { get; set; }
        public EEvent Event { get; set; }


        public UpdateRequest()
        {

        }

        public bool UpdateStatus(ConsumeInfoDTO CI, EEvent e)
        {

            if (CI.Status == EConsumeInfoStatus.Default && e == EEvent.Received_CI)
            {
                CI.Status = EConsumeInfoStatus.Pulled;
                return true;
            }


            if (CI.Status == EConsumeInfoStatus.Pulled && e == EEvent.Placed)
            {
                CI.Status = EConsumeInfoStatus.Placed;
                return true;
            }

            if (CI.Status == EConsumeInfoStatus.Pulled && e == EEvent.PlaceFailed)
            {
                CI.Status = EConsumeInfoStatus.PlaceFailed;
                return true;
            }

            return false;
        }
    }
}
