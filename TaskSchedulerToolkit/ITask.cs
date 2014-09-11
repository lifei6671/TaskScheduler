namespace TaskSchedulerToolkit
{
    /// <summary>
    /// 任务接口
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// 任务启动时执行
        /// </summary>
        void Start();
        /// <summary>
        /// 服务停止时执行
        /// </summary>
        void Stop();
    }
}
