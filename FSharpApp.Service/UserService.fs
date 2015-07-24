namespace FSharpApp.Service

open FSharpApp.Core

module UserService =
  let mutable private users = List.empty<User>

  let create username name lastName =
    let currentId =
      users
        |> List.map (fun x -> x.UserId)
        |> List.max

    let user = new User(currentId + 1, username, name, lastName)
    users <- user :: users
    user

  let find userId =
    users |> List.find (fun x -> x.UserId = userId)

  let delete userId =
    users <- users |> List.filter (fun x -> x.UserId <> userId)

  let length = users.Length