using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuoliCommon.Logger;
using Polly;

namespace LuoliUtils
{
    public static class ActionsOperator
    {

        private static  ILogger _logger;

        // 静态初始化方法，在程序启动时调用以注入日志器
        public static void Initialize(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "日志器不能为空");
        }

        public static void TryCatchAction(Action business, Action onError = null)
        {
            try
            {
                business();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                onError?.Invoke();
            }
        }


        public static async Task<bool> ReTryAction(Func<Task> business, int retryCount = 3, int waitMs = 200)
        {

            try
            {
              await  Policy
                    .Handle<Exception>() // 捕获所有异常，可以指定具体异常类型如HttpRequestException
                    .WaitAndRetryAsync(
                        retryCount: retryCount, // 重试次数
                        sleepDurationProvider: retryAttempt => TimeSpan.FromMilliseconds(waitMs), // 每次重试前等待200ms
                        onRetry: (exception, timespan, retryAttempt, context) =>
                        {
                            _logger.Warn($"第 {retryAttempt} 次重试，等待 {timespan.TotalMilliseconds} ms，错误：{exception.Message}");
                        }

                     ).ExecuteAsync(() =>  business() );

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"所有重试都失败了：{ex.Message}");
                return false;
            }

        }
    }
}
