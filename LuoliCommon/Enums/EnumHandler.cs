using System.ComponentModel;
using System.Reflection;

namespace LuoliCommon.Enums
{
    public static class EnumHandler
    {
        public static Dictionary<int, string> EnumToDictionary<TEnum>()
          where TEnum : struct, Enum // 泛型约束：确保TEnum是枚举
        {
            var enumType = typeof(TEnum);
            var enumValues = Enum.GetValues(enumType).Cast<TEnum>();
            var dict = new Dictionary<int, string>();

            foreach (var enumValue in enumValues)
            {
                // 1. 获取枚举值的数值（键）
                int key = Convert.ToInt32(enumValue);

                // 2. 获取枚举值的成员信息（用于读取[Description]特性）
                MemberInfo? memberInfo = enumType.GetMember(enumValue.ToString()).FirstOrDefault();
                if (memberInfo == null)
                {
                    // 若成员信息不存在，直接用枚举名称
                    dict[key] = enumValue.ToString();
                    continue;
                }

                // 3. 读取[Description]特性的值
                DescriptionAttribute? descriptionAttr = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                string value = descriptionAttr != null
                    ? descriptionAttr.Description  // 有特性则用特性值（如“待付款”）
                    : enumValue.ToString();        // 无特性则用默认名称

                dict[key] = value;
            }

            return dict;
        }


        // 获取枚举值的Description特性
        public static string GetDescription<T>(T enumValue) where T : struct, Enum
        {
            // 获取枚举类型的成员信息
            MemberInfo[] memberInfo = typeof(T).GetMember(enumValue.ToString());

            if (memberInfo.Length > 0)
            {
                // 尝试获取Description特性
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    // 返回Description的值
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            // 如果没有找到Description，返回枚举值的字符串表示
            return enumValue.ToString();
        }
  
        
        public static bool TryParseIgnoringCaseAndSpaces<T>(string str, out T enumValue) where T : struct, Enum
        {
            enumValue = default;

            // 空字符串直接返回失败
            if (string.IsNullOrWhiteSpace(str))
                return false;

            // 移除所有空格并转换为大写（统一比对标准）
            string processedStr = str.Replace(" ", "").ToUpperInvariant();

            // 获取枚举所有命名值
            var enumValues = Enum.GetValues<T>();

            // 遍历枚举值查找匹配项
            foreach (var value in enumValues)
            {
                // 枚举名称处理（移除空格+大写）
                string enumName = Enum.GetName(value)?.Replace(" ", "").ToUpperInvariant();

                if (enumName == processedStr)
                {
                    enumValue = value;
                    return true;
                }
            }

            return false;
        }

    }
  
}
