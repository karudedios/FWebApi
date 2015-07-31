namespace FSharpApp.Service

open FSharpApp.Core

module CarService =
  let mutable private cars = new Car("Honda", "Civic") :: new Car("Audi", "A5") :: List.empty<Car>

  let GetAll () = cars

  let Create make model =
    let car = new Car(make, model)
    cars <- car :: cars
    car

  let Find predicate =
    match List.tryFind predicate cars with
    | None -> Either.Left { Message = "Could not find car"; StatusCode = 404 }
    | Some car -> Either.Right car

  let Delete make =
    cars <- cars |> List.filter (fun car -> car.Make <> make)

  let Length = cars.Length