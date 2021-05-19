using BepInEx.Logging;

namespace RedirectDebugOutput
{
    public static class TempLog
    {
        private const bool _DEBUG_ = false;

        private static readonly ManualLogSource _logger;
        static TempLog()
        {
            if (_DEBUG_)
            {
                _logger = new ManualLogSource(BepInExLoader.MODNAME);
                Logger.Sources.Add(_logger);
            }
        }
        public static void Verbose(object msg)
        {
            if(_DEBUG_)
            _logger.LogInfo(msg);
        }

        public static void Debug(object msg)
        {
            if (_DEBUG_)
                _logger.LogDebug(msg);
        }

        public static void Message(object msg)
        {
            if (_DEBUG_)
                _logger.LogMessage(msg);
        }

        public static void Error(object msg)
        {
            if (_DEBUG_)
                _logger.LogError(msg);
        }

        public static void Warn(object msg)
        {
            if (_DEBUG_)
                _logger.LogWarning(msg);
        }
    }
}
