using LuoliCommon.DTO.ExternalOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Mock
{
    public static class ExternalOrderDtoMock
    {
        // 平台编码列表
        private static readonly List<string> _platforms = new List<string>
    {
        "TbAcs", "TbAlds", "TbArs", "Print", "Acpr", "PddAlds",
        "AldsIdle", "AldsJd", "AldsDoudian", "AldsKwai",
        "AldsYouzan", "AldsWeidian", "AldsWxVideoShop", "AldsXhs"
    };

        // 订单状态列表
        private static readonly List<string> _statuses = new List<string>
    {
        "WAIT_PAY", "PAID", "SHIPPED", "RECEIVED", "CANCELED", "REFUNDED"
    };

        // 交易类型列表
        private static readonly List<string> _types = new List<string>
    {
        "NORMAL", "GROUP_BUY", "SECONDS_KILL", "PREORDER", "RETURN"
    };

        // 随机生成器
        private static readonly Random _random = new Random();

        /// <summary>
        /// 生成指定数量的ExternalOrderDTO模拟数据
        /// </summary>
        /// <param name="count">需要生成的数量</param>
        /// <param name="minItems">每个订单包含的最小订单项数量</param>
        /// <param name="maxItems">每个订单包含的最大订单项数量</param>
        /// <returns>模拟的订单列表</returns>
        public static List<ExternalOrderDTO> GenerateMockOrders(int count, int minItems = 1, int maxItems = 5)
        {
            if (count <= 0)
                throw new ArgumentException("生成数量必须大于0", nameof(count));

            if (minItems < 1 || maxItems < minItems)
                throw new ArgumentException("订单项数量范围无效", nameof(minItems));

            var orders = new List<ExternalOrderDTO>();

            for (int i = 0; i < count; i++)
            {
                // 随机生成订单创建时间（过去30天内）
                var createTime = DateTime.Now.AddDays(-_random.Next(30)).AddMinutes(_random.Next(1440));
                // 更新时间晚于创建时间
                var updateTime = createTime.AddMinutes(_random.Next(30, 1440));

                orders.Add(new ExternalOrderDTO
                {
                    Tid = GenerateOrderNumber(),
                    Status = _statuses[_random.Next(_statuses.Count)],
                    SellerNick = GenerateSellerNick(),
                    SellerOpenUid = GenerateUserId("S"),
                    BuyerNick = GenerateBuyerNick(),
                    BuyerOpenUid = GenerateUserId("B"),
                    FromPlatform = _platforms[_random.Next(_platforms.Count)],
                    PayAmount = Math.Round((decimal)(_random.NextDouble() * 9999 + 1), 2),
                    Type = _types[_random.Next(_types.Count)],
                    CreateTime = createTime,
                    UpdateTime = updateTime,
                    ExternalOrderItems = GenerateOrderItems(_random.Next(minItems, maxItems + 1))
                });
            }

            return orders;
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        private static string GenerateOrderNumber()
        {
            // 生成20位订单号（年月日时分秒+随机4位数）
            return DateTime.Now.ToString("yyyyMMddHHmmss") +
                   _random.Next(1000, 9999).ToString();
        }

        /// <summary>
        /// 生成卖家昵称
        /// </summary>
        private static string GenerateSellerNick()
        {
            var prefixes = new[] { "诚信", "优质", "官方", "品牌", "优选" };
            var suffixes = new[] { "专营店", "旗舰店", "专卖店", "折扣店", "直销店" };
            return $"{prefixes[_random.Next(prefixes.Length)]}{_random.Next(100, 999)}{suffixes[_random.Next(suffixes.Length)]}";
        }

        /// <summary>
        /// 生成买家昵称
        /// </summary>
        private static string GenerateBuyerNick()
        {
            var prefixes = new[] { "用户", "买家", "剁手", "购物", "快乐" };
            var suffixes = new[] { "达人", "小能手", "爱好者", "常客", "VIP" };
            return $"{prefixes[_random.Next(prefixes.Length)]}{_random.Next(1000, 9999)}{suffixes[_random.Next(suffixes.Length)]}";
        }

        /// <summary>
        /// 生成用户ID
        /// </summary>
        private static string GenerateUserId(string prefix)
        {
            return $"{prefix}{DateTime.Now.Year}{_random.Next(10000000, 99999999)}";
        }

        /// <summary>
        /// 生成订单项
        /// </summary>
        private static List<ExternalOrderItem> GenerateOrderItems(int count)
        {
            var items = new List<ExternalOrderItem>();

            for (int i = 0; i < count; i++)
            {
                items.Add(new ExternalOrderItem
                {
                    Id = $"{_random.Next(100000, 999999)}",
                });
            }

            return items;
        }

        /// <summary>
        /// 生成商品标题
        /// </summary>
        private static string GenerateProductTitle()
        {
            var categories = new[] { "男士T恤", "女士连衣裙", "儿童玩具", "家居用品", "电子配件" };
            var adjectives = new[] { "新款", "时尚", "优质", "特惠", "限量版" };
            return $"{adjectives[_random.Next(adjectives.Length)]}{categories[_random.Next(categories.Length)]}";
        }

    }
}
