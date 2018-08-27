namespace Todo.Functions

open Fable.Core
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