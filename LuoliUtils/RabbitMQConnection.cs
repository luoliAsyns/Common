using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliHelper.Utils
{
    public class RabbitMQConnection
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool Persit { get; set; } = true;

        public RabbitMQConnection()
        {

        }
        public RabbitMQConnection(string jsonFile)
        {
            var rabbitMQConn = System.Text.Json.JsonSerializer.Deserialize<RabbitMQConnection>(System.IO.File.ReadAllText(jsonFile));

            Host = rabbitMQConn.Host;
            Port = rabbitMQConn.Port;
            UserId = rabbitMQConn.UserId;
            Password = rabbitMQConn.Password;
            Persit = rabbitMQConn.Persit;
        }

        
    }
}
