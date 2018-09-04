module Todo.Common.Types

open Fable.Core

type ItemState =
    | Creating
    | Created
    | Updating
    | Updated
    | Deleting

[<Pojo>]
type Item = {
    Value: string
    Id: System.Guid
    State: ItemState
}

type Model = {
    Value: string
    Items: Item list
}
