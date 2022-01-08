using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogInfo(UserRequestProperties request);
        void LogError(UserRequestProperties request);
        void LogWarn(UserRequestProperties request);
    }
}
