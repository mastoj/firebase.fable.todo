namespace Todo.Functions
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Firebase.Functions
open Fable.Import
open System
open Todo.Common.Types

module App =

    let private db = TodoDb.initialize
                        "/Users/tomasjansson/credentials/uc-test-tomas-firebase-adminsdk-om5at-706c53e692.json"
                        "https://uc-test-tomas.firebaseio.com"

    let app = express.Invoke()

    app
    |> ExpressHelpers.get "/hello" (fun (req:express.Request) (res:express.Response) _ ->
        res.send(sprintf "Hello" ) |> box)
    |> ExpressHelpers.get "/:taskId" (fun (req:express.Request) (res:express.Response) _ ->
        let (taskId: int) = Int32.Parse (req.``params``?taskId |> unbox)
        res.send(sprintf "You gave id: %i" taskId) |> box)
    |> ExpressHelpers.post "/" (fun (req:express.Request) (res:express.Response) next ->
        let item = {
            Value = "Does this work"
            Id = System.Guid.NewGuid()
            State = ItemState.Created
        }
        let ok _ = res.send(sprintf "Created: %A" item) |> box
        let error _ = res.send(sprintf "Created: %A" item) |> box
        let docPath = sprintf "todo/%s" (item.Id.ToString())
        item
        |> TodoDb.addItem db ok error docPath
        |> box)
    |> ignore

    let todo = https.onRequest app
