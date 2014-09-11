using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using Microsoft.Practices.Unity;
using TaskSchedulerToolkit;
using TaskSchedulerToolkit.Common;

namespace TaskScheduler
{
    /// <summary>
    /// 定时服务
    /// </summary>
    public partial class TaskScheduler : ServiceBase
    {
        private static readonly Timer TaskTimer = new Timer(1000);
        public TaskScheduler()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 在服务启动时执行
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                TaskTimer.Elapsed += TaskTimer_Elapsed;
                TaskTimer.Start();
            }
            catch (Exception ex)
            {
                WinLogger.LogEvent(ex.Message);
            }
            
        }
        /// <summary>
        /// 每秒钟执行的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TaskTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IUnityContainer container = UnityHelper.GetUnityContainer;

            foreach (var service in container.ResolveAll<ITask>())
            {
                try
                {
                    service.Start();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                    WinLogger.LogEvent(ex.Message);
                }
            }
        }
        /// <summary>
        /// 当服务关闭时执行
        /// </summary>
        protected override void OnStop()
        {
            IUnityContainer container = UnityHelper.GetUnityContainer;

            foreach (var service in container.ResolveAll<ITask>())
            {
                try
                {
                    service.Stop();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                    WinLogger.LogEvent(ex.Message);
                }
            }
        }
    }
}
