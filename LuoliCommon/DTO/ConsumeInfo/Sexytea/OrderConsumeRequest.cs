using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ConsumeInfo.Sexytea
{
    public class OrderConsumeRequest
    {
        public string consume_coupon { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
        public string? remark { get; set; }
        public string lastName { get; set; }
    }
}
