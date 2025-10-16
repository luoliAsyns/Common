using LuoliCommon.DTO.ConsumeInfo.Sexytea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ConsumeInfo
{
    public class SexyteaGoods
    {
        public int BranchId { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public string Platform { get; set; } = "APP_SELF_SERVICE";
        
        //"selectActivityId": [],
        //"selectCouponId": [],
   
        public bool UseBalance { get; set; } = true;
        public bool NeedToPack { get; set; } = false;
        public string ServiceType { get; set; } = "RESTAURANT";
        public int SelectPoint { get; set; } = 1; // 0:不使用积分 1:使用全部积分 2:使用自定义积分
        public int Type { get; set; } = 0;
        public string GroupBuyNo { get; set; } = string.Empty;
        public string CustomizeUsePoints { get; set; } = "0";
        public bool CalculateDelivery { get; set; } = false;

    }
}
