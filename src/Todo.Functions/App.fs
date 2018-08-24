namespace Todo.Functions
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Firebase.Functions
open Fable.Import
open System

module ExpressHelpers =
    let toRequestHandler handler =
        new System.Func<express.Request, express.Response, (obj -> unit), obj>(handler)

    let returnJson (res: express.Response) x =
        res.setHeader("Content-Type", U2.Case1 "application/json")
        res.json(x) |> box

    let post url handler (app: express.Express) =
        let requestHandler = handler |> toRequestHandler
        app.post (U2.Case1 url, requestHandler) |> ignore
        app

    let get path handler (app: express.Express) =
        let requestHandler = handler |> toRequestHandler
        app.get(U2.Case1 path, requestHandler) |> ignore
        app

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
