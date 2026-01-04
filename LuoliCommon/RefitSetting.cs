using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LuoliCommon
{
    public static class RefitSetting
    {

        public static RefitSettings LuoliRefitSettings()
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
            var refitSetting = new RefitSettings()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(jsonSerializerOptions)
            };

            return refitSetting;

        }

    }
}
