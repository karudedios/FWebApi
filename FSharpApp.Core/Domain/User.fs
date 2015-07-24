namespace FSharpApp.Core

type User (userId: int, username: string, name: string, lastName: string) =
  member this.UserId = userId
  member this.Username = username
  member this.Name = name
  member this.LastName = lastName