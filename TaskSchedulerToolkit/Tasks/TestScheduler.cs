using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSchedulerToolkit.Common;

namespace TaskSchedulerToolkit
{
    public class TestScheduler : ITask
    {
        public void Start()
        {
            if (DateTime.Now.Second == 0)
            {
                Logger.Info("我被写入的时间为："+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }

        public void Stop()
        {
            Logger.Warning("服务已停止！");
        }
    }
}
