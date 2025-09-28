

using System.Text;

namespace LuoliCommon.Logger
{

    public class LokiLogger : ILogger
    {
        // Loki 服务地址（通常是 Promtail 或 Loki 的 HTTP 端点）
        private readonly string _lokiEndpoint;
        // 日志标签（用于 Loki 查询过滤）
        private readonly Dictionary<string, string> _labels;
        private readonly HttpClient _httpClient;
        private Action<string>? _afterLog;

        // 构造函数：初始化 Loki 连接信息和标签
        public LokiLogger(string lokiEndpoint, Dictionary<string, string> labels)
        {
            _lokiEndpoint = lokiEndpoint ?? throw new ArgumentNullException(nameof(lokiEndpoint));
            _labels = labels ?? new Dictionary<string, string>();

            // 初始化 HttpClient（建议在 IOC 中注册为单例）
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(5)
            };

            // 添加默认标签（如应用名称、环境等）
            if (!_labels.ContainsKey("app"))
                _labels["app"] = "LuoliApplication";
            if (!_labels.ContainsKey("env"))
                _labels["env"] = "production";
        }

        // 日志后处理回调
        public Action<string>? AfterLog
        {
            get => _afterLog;
            set => _afterLog = value;
        }

        public void Debug(string msg) => Log("debug", msg);
        public void Error(string msg) => Log("error", msg);
        public void Info(string msg) => Log("info", msg);
        public void Warn(string msg) => Log("warn", msg);

        // 核心日志发送方法
        private void Log(string level, string message)
        {
            try
            {
                // 构建带级别标签的日志条目
                var logLabels = new Dictionary<string, string>(_labels)
                {
                    ["level"] = level
                };

                // 构建 Loki 要求的日志格式
                var logEntry = new
                {
                    streams = new[]
                    {
                        new
                        {
                            stream = logLabels,
                            values = new[] { new[]
                            { 
                                // Loki 时间戳（毫秒级 Unix 时间）
                                $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}",
                                // 日志内容（可包含结构化数据）
                                $"[{DateTime.UtcNow:o}] {message}"
                            }}
                        }
                    }
                };

                // 发送日志到 Loki
                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(logEntry),
                    Encoding.UTF8,
                    "application/json"
                );

                // 异步发送（实际项目中可根据需求改为同步）
                var response = _httpClient.PostAsync(_lokiEndpoint, content).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();


            }
            catch (Exception ex)
            {
                // 日志发送失败时的降级处理（如输出到控制台）
                Console.WriteLine($"Loki 日志发送失败: {ex.Message} | 原始日志: {message}");
            }
            // 执行日志后处理回调
            _afterLog?.Invoke(message);
        }

    }
}