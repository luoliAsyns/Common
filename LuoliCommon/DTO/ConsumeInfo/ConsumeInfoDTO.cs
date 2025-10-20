using LuoliCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ConsumeInfo
{
    public class ConsumeInfoDTO
    {
        public long Id { get; set; }
        public string Source { get; set; }
        public string TargetProxy { get; set; }
        public DateTime CreateTime { get; set; }
        public string Coupon { get; set; }
        public object Goods { get; set; }
        public string GoodsType { get; set; }
        public EConsumeInfoStatus Status { get; set; }
        public string Remark { get; set; }
        public string LastName { get; set; }


    }


    // 泛型版本用于内部处理
    public class ConsumeInfoDTO<T> : ConsumeInfoDTO
    {
        public new T Goods
        {
            get => (T)base.Goods;
            set
            {
                base.Goods = value;
            }
        }
    }

}
