@echo off
cls
if not exist tools mkdir tools
if not exist tools\Nuget.exe @powershell "wget https://nuget.org/nuget.exe -OutFile tools\Nuget.exe"
"tools\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "packages" "-ExcludeVersion"
"packages\FAKE\tools\Fake.exe" build.fsx Target=%1 %*

set nuget=
if "%nuget%" == "" (
    set nuget=nuget
)

%nuget% pack "RazorContentChecker\RazorContentChecker.csproj" -Build -NoPackageAnalysis -OutputDirectory $buildArtifactsDirectory