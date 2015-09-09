@echo off
cls
if not exist tools mkdir tools
@powershell "wget https://nuget.org/nuget.exe -OutFile tools\Nuget.exe"
"tools\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "packages" "-ExcludeVersion"
"packages\FAKE\tools\Fake.exe" build.fsx Target=%1 %*