using System.Reflection;

namespace LuoliUtils
{
    public static class MethodTimeLogger
    {
        public static void Log(MethodBase methodBase, TimeSpan elapsed, string message)
        {
            var logger = ServiceLocator.GetService<LuoliCommon.Logger.ILogger>();

            logger.Debug($"end executing [{methodBase.ReflectedType}.{methodBase.Name}], cost {elapsed.TotalMilliseconds} ms");
        }
    }
}
