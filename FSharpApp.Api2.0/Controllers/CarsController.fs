namespace FSharpApp.Api2._0.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open FSharpApp.Api2._0.Models
open FSharpApp.Core

type Failure = { Message: string; StatusCode: int }
type Response(content: 'a) =
  member this.content = content

/// Retrieves values.
type CarsController() =
    inherit ApiController()

    let values = [{ Make = "Ford"; Model = "Mustang" }; { Make = "Nissan"; Model = "Titan" }]

    let findOrFailure (predicate: (Car -> bool)) (cars: Car list) =
      let car = List.tryFind predicate cars

      match car with
      | None -> Either.Left { Message = "Could not find car"; StatusCode = 404 }
      | Some car -> Either.Right car

    member x.Get(make: string) =
      values
        |> findOrFailure (fun a -> a.Make = make)
        |> Either.Match (fun f -> None) (fun w -> Some w)
        |> x.Ok
        :> IHttpActionResult

    /// Gets all values.
    member x.Get() =
      values
      |> x.Ok
      :> IHttpActionResult
