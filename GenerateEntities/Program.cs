
using LuoliDatabase;
using SqlSugar;
using LuoliCommon.Logger;
using Microsoft.Extensions.DependencyInjection;
using LuoliHelper.Utils; // Add this using directive


namespace GenerateEntities
{

    public class Program  // 显式声明为public
    {
        // 正确的Main方法签名：static，返回void或int，参数为string[]
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            var services = new ServiceCollection();

            // 2. 配置NLog（通常在应用启动时执行一次）
            // LogManager.LoadConfiguration("nlog.config");

            // 3. 注册ILogger服务
            // 这里注册NLogLogger作为ILogger的实现，并指定构造函数参数
            services.AddSingleton<ILogger>(provider =>
                new NLogLogger(afterLog: msg =>
                {
                    Console.WriteLine(msg);

                })
            );

            // 4. 构建服务提供器（容器）
            var serviceProvider = services.BuildServiceProvider();

            // 5. 从容器中获取ILogger实例
            var logger = serviceProvider.GetRequiredService<ILogger>();


            // 测试日志
            logger.Info("程序启动成功");
            logger.Debug("这是一条调试信息");
            logger.Warn("这是一条警告信息");



            string dbConfigPath = @"/home/luoli/Downloads/configs/master_db.json";
            string targetFolder = @"/home/luoli/RiderProjects/Common/LuoliDatabase/Entities";


            // dbConfigPath = @"E:\Code\repos\ExternalOrderService\ExternalOrderService\bin\Debug\net8.0\debugConfigs\master_db.json";
            // targetFolder = @"E:\Code\repos\Common\LuoliDatabase\Entities";

            var sqlSugarConnection =  new SqlSugarConnection(dbConfigPath);




            SqlSugarScope sqlClient = new SqlSugarScope(new SqlSugar.ConnectionConfig
                {
                    // 从配置文件读取连接字符串
                    ConnectionString = $"server={sqlSugarConnection.Host}; port={sqlSugarConnection.Port}; user id={sqlSugarConnection.User}; password={sqlSugarConnection.Password}; database={sqlSugarConnection.Database};AllowLoadLocalInfile=true;",
                    DbType = (DbType)sqlSugarConnection.DBType,
                    IsAutoCloseConnection = true,

                    InitKeyType = InitKeyType.Attribute // 通过特性识别主键和自增列
                }
            );

           

            Dictionary<string, string> nameMapper = new Dictionary<string, string>();
            nameMapper.Add("partners", "PartnerEntity");
            nameMapper.Add("coupon", "CouponEntity");
            nameMapper.Add("customer", "CustomerEntity");
            nameMapper.Add("external_order", "ExternalOrderEntity");
            nameMapper.Add("sexytea_order", "SexyteaOrderEntity");
            nameMapper.Add("qrcode", "QRCodeEntity");
            nameMapper.Add("orders", "OrderEntity");
            nameMapper.Add("products", "ProductEntity");
            nameMapper.Add("questions", "QuestionEntity");
            nameMapper.Add("users", "UserEntity");

            Func<string, string> tableNameFunc = (old) =>
            {
                if (nameMapper.ContainsKey(old))
                    return nameMapper[old];
                else
                    return old;/*修改old值替换*/
            };

            sqlClient.DbFirst
                   .IsCreateAttribute()//创建sqlsugar自带特性
        .FormatFileName(it => tableNameFunc(it)) //格式化文件名（文件名和表名不一样情况）
        .FormatClassName(it => tableNameFunc(it))//格式化类名 （类名和表名不一样的情况）
        .CreateClassFile(targetFolder, "LuoliDatabase.Entities");


            logger.Info("实体类生成完毕");
        }


    }
}
