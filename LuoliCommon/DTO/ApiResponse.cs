using LuoliCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Entities
{
    public class ApiResponse<T>
    {
        public EResponseCode code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
        public bool ok { get {
                return code == EResponseCode.Success;
            }
        }

    }

    public class PageResult<T>
    {
        public long Total { get; set; }      // 总记录数
        public int Page { get; set; }        // 当前页码
        public int Size { get; set; }        // 每页大小
        public List<T> Items { get; set; }   // 数据列表
    }
}
