module Todo.Common.Types

type ItemState =
    | Creating
    | Created
    | Updating
    | Updated
    | Deleting

type Item = {
    Value: string
    Id: System.Guid
    State: ItemState
}

type Model = {
    Value: string
    Items: Item list
}
