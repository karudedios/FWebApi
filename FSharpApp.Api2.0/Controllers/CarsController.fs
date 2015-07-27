namespace FSharpApp.Api2._0.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open System.Web.Http.Results
open FSharpApp.Api2._0.Models
open FSharpApp.Core

type Failure = { Message: string; StatusCode: int }
type Response<'a>(content: 'a) =
  member this.content = content

/// Retrieves values.
type CarsController() =
    inherit ApiController()

    let (|@|) a b = Option.fold (+) a b

    let values = [{ Make = "Ford"; Model = "Mustang" }; { Make = "Nissan"; Model = "Titan" }]

    let findOrFailure (predicate: (Car -> bool)) (cars: Car list) =
      let car = List.tryFind predicate cars

      match car with
      | None -> Either.Left { Message = "Could not find car"; StatusCode = 404 }
      | Some car -> Either.Right car

    let validate = (fun (carOrFailure:Either<Failure, Car>) ->
      match carOrFailure with
      | Left f -> Either.Left f
      | Right r -> 
        if r.Model = "Mustang" then
          Either.Right r
        else Either.Left { Message = "Should search for a Mustang"; StatusCode = 500 })

    member x.Get(make: string) =
      values
        |> findOrFailure (fun a -> a.Make = make)
        |> validate
        |> function
          | Left f -> new BadRequestErrorMessageResult(f.Message, x) :> IHttpActionResult
          | Right w -> new OkNegotiatedContentResult<Car>(w, x) :> IHttpActionResult
    
    /// Gets all values.
    member x.Get() =
      values
      |> x.Ok
      :> IHttpActionResult