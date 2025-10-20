using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ConsumeInfo
{

    public class DeleteRequest
    {
        public string GoodsType { get; set; } // 与请求体中的goodsType对应（注意大小写，可通过特性调整）
        public int Id { get; set; } // 假设id是整数类型，根据实际情况调整
    }
}
