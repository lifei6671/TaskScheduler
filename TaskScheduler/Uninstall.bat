@echo off 
echo 请按任意键开始卸载服务. . .
echo.
pause
echo.
echo 正在卸载服务. . .
cd /d %~dp0
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe /U TaskScheduler.exe >  InstallService.log
echo.
echo 卸载完成
echo.
echo 操作结束，请在 InstallService.log 中查看具体的操作结果。
echo.
pause