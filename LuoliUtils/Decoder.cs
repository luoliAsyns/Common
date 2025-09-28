using System.Text;

namespace LuoliUtils
{
    public static class Decoder
    {
        public static (bool, string) Base64(string base64UrlStr)
        {
            // 1. 替换 Base64URL 字符为标准 Base64
            string base64Str = base64UrlStr.Replace('-', '+').Replace('_', '/');
            // 2. 补全 = 填充符
            switch (base64Str.Length % 4)
            {
                case 2: base64Str += "=="; break;
                case 3: base64Str += "="; break;
            }

            string result = null;
            try
            {
                if (string.IsNullOrEmpty(base64Str))
                    return (false, result);

                byte[] bytes = Convert.FromBase64String(base64Str);
                result = Encoding.UTF8.GetString(bytes);
                return (true, result);
            }
            catch (FormatException fex)
            {
                return (false, "Format Exception:" + fex.Message);
            }
            catch (ArgumentException aex)
            {
                return (false, "Argument Exception:" + aex.Message);
            }
        }

        public static string SHA256(string input)
        {
            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                // 将字符串转换为字节数组
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // 将字节数组转换为十六进制字符串
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    // 格式化为两位十六进制数，不足两位前面补0
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}