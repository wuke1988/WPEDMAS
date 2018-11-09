@echo Generating Resources...
@cd zh-Hans
@"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools\ResGen" Telerik.Windows.Controls.Strings.zh-Hans.resx
@echo Generating Resources DLL...
@"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools\al" /nologo /t:lib /embed:Telerik.Windows.Controls.Strings.zh-Hans.resources,Telerik.Windows.Controls.Strings.zh-Hans.resources /culture:zh-Hans /out:Telerik.Windows.Controls.resources.dll /v:2015.2.623.40
@cd ..
@echo Finished.
@pause