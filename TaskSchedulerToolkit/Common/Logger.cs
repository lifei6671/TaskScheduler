using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedulerToolkit.Common
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public static class Logger
    {
        private static readonly ConcurrentQueue<LogMessage> LoggerQueue = new ConcurrentQueue<LogMessage>();

        private static readonly string LoggerPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Log" + Path.DirectorySeparatorChar;
        private static bool _queueState = false;
        private static readonly ConcurrentDictionary<string, List<Message>> Container = null;
        private static Task _writeTask = new Task(new Action(Start));
        static Logger()
        {
            Container = new ConcurrentDictionary<string, List<Message>>();
            Container.AddOrUpdate("error", new List<Message>(), (s, list) => new List<Message>());
            Container.AddOrUpdate("info", new List<Message>(), (s, list) => new List<Message>());
            Container.AddOrUpdate("warning", new List<Message>(), (s, list) => new List<Message>());
            Container.AddOrUpdate("debug", new List<Message>(), (s, list) => new List<Message>());
            Container.AddOrUpdate("fatal", new List<Message>(), (s, list) => new List<Message>());

        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            Enqueue("error", msg);
        }
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            Enqueue("info", msg);
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Warning(string msg)
        {
            Enqueue("warning", msg);
        }
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            Enqueue("debug", msg);
        }
        /// <summary>
        /// 失败日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            Enqueue("fatal", msg);
        }

        private static void Enqueue(string level, string msg)
        {
            LogMessage log = new LogMessage { Level = level, Message = msg, Time = DateTime.Now };
            LoggerQueue.Enqueue(log);
            if (_writeTask.Status == TaskStatus.RanToCompletion || _writeTask.Status == TaskStatus.Faulted)
            {
                _writeTask = new Task(Start);
            }
            if (_writeTask.Status == TaskStatus.Created)
            {
                _writeTask.Start();
            }
        }
        private static void Start()
        {
            if (_queueState == false)
            {
                _queueState = true;
                Thread.Sleep(1000);
                //string path = LoggerPath + level + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd");
                if (LoggerQueue.Count > 0)
                {

                    while (LoggerQueue.Count > 0)
                    {
                        LogMessage log;
                        if (LoggerQueue.TryDequeue(out log))
                        {
                            Container[log.Level].Add(new Message { Msg = log.Message, Time = log.Time });
                        }
                    }
                    foreach (var dic in Container)
                    {
                        if (Container[dic.Key].Count > 0)
                        {

                            Write(Container[dic.Key], LoggerPath + dic.Key + Path.DirectorySeparatorChar);
                            Container[dic.Key].Clear();
                        }
                    }
                }
                _queueState = false;
            }
        }
        private static void Write(List<Message> log, string path)
        {
            if (log == null || log.Count <= 0)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            foreach (var message in log)
            {
                builder.AppendFormat("Time: " + message.Time.ToString("yyyy-MM-dd HH:mm:ss"));
                builder.AppendLine();
                builder.AppendFormat("Message:" + message.Msg);
                builder.AppendLine();
                builder.Append("------------------------------------------------------------");
                builder.AppendLine();
            }
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string filePath = path + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            File.AppendAllText(filePath, builder.ToString());
        }

        private class Message
        {
            public DateTime Time { set; get; }
            public string Msg { set; get; }
        }
    }

    internal class LogMessage
    {
        public string Level { set; get; }
        public DateTime Time { set; get; }
        public string Message { set; get; }
    }
}
