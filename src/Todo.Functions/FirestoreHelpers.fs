module FirestoreHelpers
open Fable.Import.Firebase
open Fable.Import.Firebase.Admin
open Fable.Core

module AppOptions =
    let create() = Admin.AppOptions.defaultOptions()

    let withCredentials credentials (appOptions: AppOptions) =
        appOptions.credential <- Some credentials
        appOptions

    let withCredentialsFile filePath =
        Admin.credential.cert(U2.Case1(filePath))
        |> withCredentials

    let withDatabaseUrl url (appOptions: AppOptions) =
        appOptions.databaseURL <- Some url
        appOptions

    let initializeApp (appOptions: AppOptions) =
        admin.initializeApp(appOptions)

    let getFirestoreDb (app: App.App) =
        app.firestore()