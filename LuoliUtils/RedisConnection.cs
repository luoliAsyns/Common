using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliHelper.Utils
{
    public class RedisConnection
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public int DatabaseId { get; set; }


        public RedisConnection()
        {
            
        }
        public RedisConnection(string jsonFile)
        {
            var redisConn = System.Text.Json.JsonSerializer.Deserialize<RedisConnection>(System.IO.File.ReadAllText(jsonFile));

            Host = redisConn.Host; 
            Port= redisConn.Port;
            Password = redisConn.Password;
            DatabaseId = redisConn.DatabaseId;
           
            Console.WriteLine($"Redis Initialization passed. json config:[{jsonFile}]");
        }
    }
}
