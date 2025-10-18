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



        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly Random _random = new Random();

        /// <summary>
        /// 将输入字符串转换为 6 位短链编码
        /// </summary>
        /// <param name="input">完整url  例如 https://consume.huoshan.asynspetfood.top?coupon=12346578901234567890123456789012</param>
        /// <returns>6 位短链编码</returns>
        public static string GenerateShortCode(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("输入字符串不能为空", nameof(input));

            // 1. 使用 SHA256 哈希计算（比 MD5 更安全，生成 256 位哈希值）
            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // 2. 从哈希值中取前 4 个字节（32 位）作为种子，确保相同输入生成相同短链
                uint seed = BitConverter.ToUInt32(hashBytes, 0);

                // 3. 基于种子生成 6 位短链（保证相同输入输出一致，不同输入大概率不同）
                var random = new Random((int)seed);
                var shortCode = new char[6];
                for (int i = 0; i < 6; i++)
                {
                    // 从字符集中随机选择（基于种子的伪随机，确保一致性）
                    shortCode[i] = Chars[random.Next(Chars.Length)];
                }

                return new string(shortCode);
            }
        }
    }
}