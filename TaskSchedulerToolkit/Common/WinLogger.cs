using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TaskSchedulerToolkit.Common
{
    public static class WinLogger
    {
        /// <summary>
        /// 记录一个错误日志
        /// </summary>
        /// <param name="message"></param>
        public static void LogEvent(string message)
        {
            LogEvent(EventLogEntryType.Error, message);
        }
        /// <summary>
        /// 记录一个指定类型的日志
        /// </summary>
        /// <param name="eventLogType"></param>
        /// <param name="message"></param>
        public static void LogEvent(EventLogEntryType eventLogType, string message)
        {
            LogEvent("TaskScheduler", eventLogType, message);
        }
        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="eventLogType"></param>
        /// <param name="message">事件内容</param>
        /// <param name="eventSourceName"></param>
        public static void LogEvent(string eventSourceName, EventLogEntryType eventLogType,string message)
        {
            if (EventLog.SourceExists(eventSourceName) == false)
            {
                EventLog.CreateEventSource(eventSourceName, "Application");
            }
            EventLog.WriteEntry(eventSourceName, message, eventLogType);
        }
    }
}
