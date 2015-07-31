namespace FSharpApp.Api2._0.Controllers

open FSharpApp.Core
open FSharpApp.Service
open System.Web.Http
open System.Web.Http.Results

/// Retrieves values.
type CarController() =
    inherit ApiController()

    member x.Get(make: string) =
      CarService.Find (fun a -> a.Make = make)
      |> function
        | Left f -> new BadRequestErrorMessageResult(f.Message, x) :> IHttpActionResult
        | Right w -> new OkNegotiatedContentResult<Car>(w, x) :> IHttpActionResult

    /// Gets all values.
    member x.Get() =
      CarService.GetAll()
      |> x.Ok
      :> IHttpActionResult