using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{

    public class EnumConverter : JsonConverter<Enum>
    {
        public override Enum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // 反序列化（前端传字符串时转换为枚举）
            if (reader.TokenType == JsonTokenType.String)
            {
                var enumName = reader.GetString();
                foreach (var enumValue in Enum.GetValues(typeToConvert))
                {
                    var memberInfo = typeToConvert.GetMember(enumValue.ToString()).FirstOrDefault();
                    var description = memberInfo?.GetCustomAttribute<DescriptionAttribute>()?.Description;
                    if (description == enumName)
                    {
                        return (Enum)enumValue;
                    }
                }
            }
            // 支持数字反序列化（兼容旧逻辑）
            return (Enum)Enum.ToObject(typeToConvert, reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, Enum value, JsonSerializerOptions options)
        {
            // 序列化时输出 Description
            var memberInfo = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var description = memberInfo?.GetCustomAttribute<DescriptionAttribute>()?.Description;
            writer.WriteStringValue(description ?? value.ToString()); // 无 Description 则用枚举名
        }
    }
}
