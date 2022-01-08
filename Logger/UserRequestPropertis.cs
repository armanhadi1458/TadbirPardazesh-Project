using System;

namespace Logger
{
    public class UserRequestProperties
    {
        public string LogMessage { get; set; }
        public string MessageBody { get; set; }
        public string Code { get; set; }
        public string Response { get; set; }
        public string Duration { get; set; }
        public string Url { get; set; }
        public ERunningStatus? Status { get; set; }
        public Exception Exception { get; set; }
    }

    public enum ERunningStatus
    {
        Beginning,
        Running,
        Failed,
        Done
    }
}
