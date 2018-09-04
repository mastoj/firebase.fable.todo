module TodoDb

open Fable.Core.JsInterop
open Fable.PowerPack
open Fable.Import.Firebase
open FirestoreHelpers

let initialize credentialsFile databseUrl =
    AppOptions.create()
    |> AppOptions.withDatabaseUrl databseUrl
    |> AppOptions.withCredentialsFile credentialsFile
    |> AppOptions.initializeApp
    |> AppOptions.getFirestoreDb

let addItem (db:Admin.Firestore.Firestore) ok error docPath (item) =
    db.doc(docPath).set(item)
    |> Promise.either (fun x -> !^(ok x)) (fun x -> !^(error x))
    |> Promise.start
    |> ignore
