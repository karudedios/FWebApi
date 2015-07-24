namespace FSharpApp.Core

type UserType (userId: int, username: string, name: string, lastName: string) =
  member this.UserId = userId
  member this.Username = username
  member this.Name = name
  member this.LastName = lastName

module User =
  let AdminUser = new UserType(1, "admin", "Admin", "Admin")

  let create userId username name lastName =
    new UserType(userId, username, name, lastName)