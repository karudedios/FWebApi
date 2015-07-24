namespace FSharpApp.Core

type UserType (userId: int option, username: string option, name: string option, lastName: string option) =
  let mutable _userId: int option = userId
  let mutable _username: string option = username
  let mutable _name: string option = name
  let mutable _lastName: string option = lastName

  member this.UserId
    with get() = _userId
    and set(value) = _userId <- value

module User =
  let u = new UserType(Some 1, Some "admin", Some "Admin", Some "Admin")

  let create userId username name lastName =
    new UserType(Some userId, Some username, Some name, Some lastName)