using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace TaskScheduler
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //注册类型
            UnityHelper.Register();
            ServiceBase[] servicesToRun  =
            { 
                new TaskScheduler() 
            };
            ServiceBase.Run(servicesToRun);
            
        }
    }
}
