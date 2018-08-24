// ts2fable 0.6.1
module rec Firebase
open System
open Fable.Core
open Fable.Import.JS

let [<Import("*","firebase")>] firebase: App.FirebaseNamespace = jsNative

module App =
    type [<AllowNullLiteral>] IExports =
        abstract FirebaseApp: FirebaseAppStatic

    type [<AllowNullLiteral>] FirebaseOptions =
        abstract apiKey: string option with get, set
        abstract authDomain: string option with get, set
        abstract databaseURL: string option with get, set
        abstract projectId: string option with get, set
        abstract storageBucket: string option with get, set
        abstract messagingSenderId: string option with get, set

    module FirebaseOptions =
        let defaultOptions() =
            let mutable apiKey: string option = None
            let mutable authDomain: string option = None
            let mutable databaseURL: string option = None
            let mutable projectId: string option = None
            let mutable storageBucket: string option = None
            let mutable messagingSenderId: string option = None

            { new FirebaseOptions with
                member this.apiKey
                    with get() = apiKey
                    and set v = apiKey <- v
                member this.authDomain
                    with get() = authDomain
                    and set v = authDomain <- v
                member this.databaseURL
                    with get() = databaseURL
                    and set v = databaseURL <- v
                member this.projectId
                    with get() = projectId
                    and set v = projectId <- v
                member this.storageBucket
                    with get() = storageBucket
                    and set v = storageBucket <- v
                member this.messagingSenderId
                    with get() = messagingSenderId
                    and set v = messagingSenderId <- v
            }

    type [<AllowNullLiteral>] FirebaseAppConfig =
        abstract name: string option with get, set
        abstract automaticDataCollectionEnabled: bool option with get, set

    type [<AllowNullLiteral>] FirebaseApp =
        /// The (read-only) name (identifier) for this App. '[DEFAULT]' is the default
        /// App.
        abstract name: string with get, set
        /// The (read-only) configuration options from the app initialization.
        abstract options: FirebaseOptions with get, set
        /// The settable config flag for GDPR opt-in/opt-out
        abstract automaticDataCollectionEnabled: bool with get, set
        /// Make the given App unusable and free resources.
        abstract delete: unit -> Promise<unit>

    type [<AllowNullLiteral>] FirebaseAppStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> FirebaseApp

    type [<AllowNullLiteral>] FirebaseNamespace =
        /// <summary>Create (and initialize) a FirebaseApp.</summary>
        /// <param name="options">Options to configure the services used in the App.</param>
        /// <param name="config">The optional config for your firebase app</param>
        abstract initializeApp: options: FirebaseOptions * ?config: FirebaseAppConfig -> FirebaseApp
        /// <summary>Create (and initialize) a FirebaseApp.</summary>
        /// <param name="options">Options to configure the services used in the App.</param>
        /// <param name="name">The optional name of the app to initialize ('[DEFAULT]' if
        /// omitted)</param>
        abstract initializeApp: options: FirebaseOptions * ?name: string -> FirebaseApp
        abstract app: obj with get, set
        /// A (read-only) array of all the initialized Apps.
        abstract apps: ResizeArray<FirebaseApp> with get, set
        abstract Promise: obj with get, set
        abstract SDK_VERSION: string with get, set