namespace FSharpApp.Api.Controllers

open FSharpApp.Api
open FSharpApp.Core
open FSharpApp.Service
open System.Web.Http
open System.Web.Http.Results

/// Retrieves values.
type UserController() =
    inherit ApiController()
    
    let values = List.empty<Car>

    let findOrFailure (predicate: Car -> bool) (cars: Car list) =
      match List.tryFind predicate cars with
      | None -> Either.Left { Message = "Could not find car"; StatusCode = 404 }
      | Some car -> Either.Right car

    let validate = fun (carOrFailure:Either<Failure, Car>) ->
      match carOrFailure with
      | Left f -> Either.Left f
      | Right r ->
        if r.Model = "Mustang" then
          Either.Right r
        else Either.Left { Message = "Should search for a Mustang"; StatusCode = 500 }

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