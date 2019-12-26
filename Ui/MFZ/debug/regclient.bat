@echo off
cls
echo *
echo *   Regasm Services.
echo **********************

C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\regsvcs /nologo /appname:UpdateSystemTools UpdateSystemTools.dll
C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\regsvcs /nologo /appname:RegisterDLL RegisterDLL.dll

C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\RegAsm /u UpdateSystem_Svr.dll 
C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\RegAsm  UpdateSystem_Svr.dll 