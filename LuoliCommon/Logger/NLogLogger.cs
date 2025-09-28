using NLog;


namespace LuoliCommon.Logger
{

    public class NLogLogger : ILogger
    {

        private Action<string>? _afterLog = null;

        public NLogLogger(Action<string> afterLog)
        {
            _afterLog = afterLog;
        }


        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public void Debug(string msg)
        {
            _logger.Debug(msg);
            _afterLog?.Invoke(msg);
        }

        public void Error(string msg)
        {
            _logger.Error(msg);
            _afterLog?.Invoke(msg);
        }

        public void Info(string msg)
        {
            _logger.Info(msg);
            _afterLog?.Invoke(msg);
        }

        public void Warn(string msg)
        {
            _logger.Warn(msg);
            _afterLog?.Invoke(msg);
        }
    }
}