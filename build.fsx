// include Fake lib
#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open System

let buildDir = "./build/"
let testDir = "./test/"
let nugetPath = @"tools\NuGet.exe"

RestorePackages()

Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
)

Target "BuildApp" (fun _ ->
    !! "RazorContentChecker/RazorContentChecker.csproj"
        |> MSBuildRelease buildDir "Build"
        |> Log "AppBuild-Output: "
)

//Target "BuildTests" (fun _ ->
//    !! "Tests/Tests.csproj"
//        |> MSBuildDebug testDir "Build"
//        |> Log "TestBuild-Output: "
//)
//
//Target "Tests" (fun _ ->
//    !! (testDir + "/RazorContentChecker.Tests.dll")
//        |> NUnit (fun p ->
//            { p with
//                DisableShadowCopy = true;
//                OutputFile = testDir + "TestResults.xml"
//                ToolPath = @"packages\NUnit.Runners.2.6.4\tools\" })
//)

// Default target
Target "Default" (fun _ ->
    trace "Compiling Sei Bella Website"
)

// Dependencies
"Clean"
    ==> "BuildApp"
    ==> "Default"
    
// start build
RunTargetOrDefault "Default"
