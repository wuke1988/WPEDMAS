@echo Generating Resources...
@cd zh-Hans
@"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools\ResGen" Catel.Properties.Resources.zh-Hans.resx
@echo Generating Resources DLL...
@"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools\al" /nologo /t:lib /embed:Catel.Properties.Resources.zh-Hans.resources,Catel.Properties.Resources.zh-Hans.resources /culture:zh-Hans /out:Catel.MVVM.resources.dll /v:4.3.0.0
@cd ..
@xcopy /E /Y zh-Hans\*.dll ..\..\packages\Catel.MVVM.4.3.0\lib\net40\zh-Hans\
@echo Finished.
@pause