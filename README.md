TaskScheduler
=============

一个用Unity实现依赖注入的定时任务Windows服务。

通过Unity.config配置文件，让实现ITask接口的派生类注入到服务中，并执行。

通过该方式，可以在不了解服务细节的情况下，来动态添加任务到服务中。
