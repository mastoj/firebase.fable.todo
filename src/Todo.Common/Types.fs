module Todo.Common.Types

type ItemState =
    | Creating = 0
    | Created = 1
    | Updating = 2
    | Updated = 3
    | Deleting = 4

type Item = {
    Value: string
    Id: System.Guid
    State: ItemState
}

type Model = {
    Value: string
    Items: Item list
}
