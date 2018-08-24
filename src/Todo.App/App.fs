module App.View

open Elmish
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma
open Fulma.FontAwesome
open Firebase
open Todo.Common.Types

let firebaseOptions = App.FirebaseOptions.defaultOptions()
//firebaseOptions
let firebaseApp = firebase.initializeApp(firebaseOptions, "todoapp")

type Msg =
    | ChangeValue of string
    | SubmitItem of string
    | DeleteItem of Item

let init _ = { Value = ""; Items = [] }, Cmd.none

let private update msg model =
    match msg with
    | ChangeValue newValue ->
        { model with Model.Value = newValue }, Cmd.none
    | SubmitItem newItem ->
        { model with Value = ""; Items = {Value = newItem; Id = System.Guid.NewGuid(); State = Created}::model.Items}, Cmd.none
    | DeleteItem item ->
        { model with Value = ""; Items = model.Items |> List.map (fun i -> if i.Id = item.Id then {i with State = Deleting} else i)}, Cmd.none


let private input model dispatch =
    form
        [
            OnSubmit (fun ev -> ev.preventDefault(); SubmitItem model.Value |> dispatch)
        ]
        [
          Field.div [ ]
            [ Label.label [ ]
                [ str "Enter your name" ]
              Control.div [ ]
                [ Input.text [ Input.OnChange (fun ev -> dispatch (ChangeValue ev.Value))
                               Input.Value model.Value
                               Input.Props [ AutoFocus true ] ] ] ]
        ]

let toClassName item =
    match item.State with
    | Creating -> "creating"
    | Created -> "created"
    | Updating -> "updating"
    | Updated -> "updated"
    | Deleting -> "deleting"


let itemClicked dispatch item =
    match item.State with
    | Creating
    | Updating
    | Updated
    | Created -> dispatch (DeleteItem item)
    | Deleting -> ()

let private itemView dispatch (item: Item) =
    li
        [
            OnClick (fun ev -> itemClicked dispatch item)
            ClassName (item |> toClassName)
        ] [str item.Value]

let private view model dispatch =
    Hero.hero [ Hero.IsFullHeight ]
        [ Hero.body [ ]
            [ Container.container [ ]
                [ Columns.columns [ Columns.CustomClass "has-text-centered" ]
                    [ Column.column [ Column.Width(Screen.All, Column.IsOneThird)
                                      Column.Offset(Screen.All, Column.IsOneThird) ]
                        [ Image.image [ Image.Is128x128
                                        Image.Props [ Style [ Margin "auto"] ] ]
                            [ img [ Src "assets/fulma_logo.svg" ] ]
                          input model dispatch
                          ul
                            []
                            (model.Items |> List.map (itemView dispatch))
                          Content.content [ ]
                            [ str "Hello, "
                              str model.Value
                              str " "
                              Icon.faIcon [ ]
                                [ Fa.icon Fa.I.SmileO ] ] ] ] ] ] ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR

Program.mkProgram init update view
#if DEBUG
|> Program.withHMR
#endif
|> Program.withReactUnoptimized "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
