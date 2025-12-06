using LuoliCommon.DTO.ConsumeInfo.Sexytea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LuoliUtils
{
    public class SexyteaAccRecommend
    {



        public SexyteaAccRecommend()
        {
            
        }


        public bool ExistValidAcc(Dictionary<string, Account> accounts)
        {
            foreach(var pair in accounts)
            {
                var account = pair.Value;
                if ((account.Exp - DateTime.Now)> TimeSpan.FromMinutes(10))
                    return true;
            }
            
            return false;
        }


        public Account Recommend(string coupon, Dictionary<string, Account> accounts)
        {
            var validAccounts = accounts.Values.Where(t => (t.Exp - DateTime.Now) > TimeSpan.FromMinutes(10)).ToList();

            // 边界校验：列表不能为空
            if (validAccounts == null || validAccounts.Count == 0)
            {
                throw new ArgumentNullException(nameof(validAccounts), "元素列表不能为空");
            }
            if(validAccounts.Count == 1)
                return validAccounts[0];

            string hash16Str = coupon.Substring(0, 16);

            // 将十六进制字符串转为无符号长整型（ulong），忽略大小写
            ulong hashInt = Convert.ToUInt64(hash16Str, 16);

            // 取模得到列表索引
            int index = (int)(hashInt % (ulong)validAccounts.Count);

            // 返回对应索引的元素
            return validAccounts[index];

        }
    }
}
