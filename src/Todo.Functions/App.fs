namespace Todo.Functions
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Firebase.Functions
open Fable.Import
open System

module App =

    let app = express.Invoke()

    app
    |> ExpressHelpers.get "/hello" (fun (req:express.Request) (res:express.Response) _ ->
                res.send(sprintf "Hello" ) |> box)
    |> ExpressHelpers.get "/:taskId" (fun (req:express.Request) (res:express.Response) _ ->
        let (taskId: int) = Int32.Parse (req.``params``?taskId |> unbox)
        res.send(sprintf "You gave id: %i" taskId) |> box)
    |> ignore

    let todo = https.onRequest app
