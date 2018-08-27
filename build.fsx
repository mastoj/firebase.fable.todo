#r "paket: groupref netcorebuild //"
#load ".fake/build.fsx/intellisense.fsx"

#nowarn "52"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.JavaScript

let projects = !!"src/**/*.fsproj"

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    ++ "public"
    ++ "functions"
    |> Seq.iter Shell.cleanDir
)

Target.create "Restore" (fun _ ->
    projects
    |> Seq.iter (fun p ->
        DotNet.restore
            (DotNet.Options.withWorkingDirectory __SOURCE_DIRECTORY__)
            p
    )
)

Target.create "YarnInstall" (fun _ ->
    Yarn.install id
    Yarn.install (fun opts -> { opts with WorkingDirectory = "./src/Todo.App"})
)

Target.create "Build" (fun _ ->
    let result =
        DotNet.exec
            (DotNet.Options.withWorkingDirectory __SOURCE_DIRECTORY__)
            "fable"
            "webpack --port free -- -p"

    if not result.OK then failwithf "dotnet fable failed with code %i" result.ExitCode

    let functionsPackageJson = "./src/Todo.Functions/package.json"
    let functionsFolder = "./functions"
    Shell.copyFile functionsFolder functionsPackageJson
)

Target.create "Watch" (fun _ ->
    let result =
        DotNet.exec
            (DotNet.Options.withWorkingDirectory __SOURCE_DIRECTORY__)
            "fable"
            "webpack-dev-server --port free"

    if not result.OK then failwithf "dotnet fable failed with code %i" result.ExitCode
)

// Build order
"Clean"
    ==> "Restore"
    ==> "YarnInstall"
    ==> "Build"

"Watch"
    <== [ "YarnInstall" ]

// start build
Target.runOrDefault "Build"
