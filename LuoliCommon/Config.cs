using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LuoliCommon
{
    public class Config
    {
        public string BindAddr { get; set; }
        public int SemaphoreSlim { get; set; }
        public string ServiceName { get; set; }
        public int ServiceId { get; set; }
      
        public Dictionary<string, string> KVPairs { get; set; }

        public Config()
        {
            KVPairs = new Dictionary<string, string>();
        }

        public Config(string jsonFile)
        {
            var config=  System.Text.Json.JsonSerializer.Deserialize<Config>(System.IO.File.ReadAllText(jsonFile));
            // 手动赋值每个属性
            this.BindAddr = config.BindAddr;
            this.SemaphoreSlim = config.SemaphoreSlim;
            this.KVPairs = config.KVPairs;
            this.ServiceName = config.ServiceName;
            this.ServiceId = config.ServiceId;

           Console.WriteLine($"Config loaded passed. json config:[{jsonFile}]");
        }
    }
}
