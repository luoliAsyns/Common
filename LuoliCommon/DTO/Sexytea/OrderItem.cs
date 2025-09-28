using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliHelper.Entities.Sexytea
{
    public class OrderItem
    {
        public int Count { get; set; }
        public string GoodsCode { get; set; }
        public int GoodsId { get; set; }
        public string ServiceType { get; set; }
        public List<SpecGroup> SpecGroups { get; set; }
    }

    public class SpecGroup
    {
        public int GroupId { get; set; }
        public List<SpecItem> Items { get; set; }
    }

    public class SpecItem
    {
        public int Count { get; set; }
        public string SpecCode { get; set; }
        public int SpecId { get; set; }
    }
}
