using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace TaskScheduler
{
    /// <summary>
    /// 依赖注入辅助类
    /// </summary>
    public class UnityHelper
    {
        private static readonly IUnityContainer UnityContainer = new UnityContainer();
        public static IUnityContainer GetUnityContainer { get { return UnityContainer; } }
        /// <summary>
        /// 注册类型
        /// </summary>
        public static void Register()
        {
            IUnityContainer unityContainer = UnityContainer;

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "Unity.config" };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection("unity");

            unityContainer.LoadConfiguration(section);

        }
    }
}
