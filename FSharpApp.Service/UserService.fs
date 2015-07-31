namespace FSharpApp.Service

open FSharpApp.Core

module UserService =
  let mutable private users = List.empty<User>

  let GetAll() = users

  let Create username name lastName =
    let currentId =
      users
        |> List.map (fun x -> x.UserId)
        |> List.max

    let user = new User(currentId + 1, username, name, lastName)
    users <- user :: users
    user

  let Find predicate =
    match List.tryFind predicate users with
    | None -> Either.Left { Message = "Could not find user"; StatusCode = 404 }
    | Some user -> Either.Right user

  let Delete userId =
    users <- users |> List.filter (fun x -> x.UserId <> userId)

  let Length = users.Length