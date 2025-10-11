using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ExternalOrder
{
    /// <summary>
    /// 订单项目DTO
    /// </summary>
    public class ExternalOrderItem
    {
        /// <summary>
        /// 订单项ID
        /// </summary>
        public string Id { get; set; }

        // 可以根据需要添加更多订单项属性
        // [ProtoMember(2)]
        // public string ProductName { get; set; }
    }
}
