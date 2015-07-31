namespace FSharpApp.Core

type User (userId: int, username: string, name: string, lastName: string) =
  member x.UserId = userId
  member x.Username = username
  member x.Name = name
  member x.LastName = lastName