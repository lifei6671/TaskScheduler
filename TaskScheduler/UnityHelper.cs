using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace TaskScheduler
{
    /// <summary>
    /// 依赖注入辅助类
    /// </summary>
    public class UnityHelper
    {
        //静态依赖注入对象
        private static readonly IUnityContainer UnityContainer = new UnityContainer();
        /// <summary>
        /// 获取依赖注入对象
        /// </summary>
        public static IUnityContainer GetUnityContainer { get { return UnityContainer; } }
        /// <summary>
        /// 注册类型
        /// </summary>
        public static void Register() 
        {
            //获取依赖注入对象
            IUnityContainer unityContainer = UnityContainer;
            //初始化配置文件路径
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "Unity.config" };
            //读取配置文件
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            //获取配置节点
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection("unity");
            //加载配置数据
            unityContainer.LoadConfiguration(section);

        }
    }
}
