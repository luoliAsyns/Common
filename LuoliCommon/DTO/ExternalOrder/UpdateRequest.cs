using LuoliCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ExternalOrder
{
    public class UpdateRequest
    {
        public ExternalOrderDTO EO { get; set; }
        public EEvent Event { get; set; }


        public UpdateRequest()
        {
            
        }


        public bool UpdateStatus (ExternalOrderDTO eo, EEvent e)
            {

            if(eo.Status == EExternalOrderStatus.Default && e == EEvent.Received_Paid_EO)
            {
                eo.Status = EExternalOrderStatus.Pulled;
                return true;
            }

            if (eo.Status == EExternalOrderStatus.Pulled && e == EEvent.Received_Refund_EO)
            {
                eo.Status = EExternalOrderStatus.Refunding;
                return true;
            }

            if (eo.Status == EExternalOrderStatus.Refunding && e == EEvent.Receive_Manual_Recover_Coupon)
            {
                eo.Status = EExternalOrderStatus.Pulled;
                return true;
            }


            return false;
        }
    }
}
