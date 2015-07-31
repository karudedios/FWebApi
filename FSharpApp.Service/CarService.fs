namespace FSharpApp.Service

open FSharpApp.Core

module CarService =
  let mutable private cars = new Car("Honda", "Civic") :: new Car("Audi", "A5") :: List.empty<Car>

  let GetAll () = cars

  let Create car =
    try 
      cars <- car :: cars
      Either.Right car
    with
      ex -> Either.Left { Message = "Could not create car"; StatusCode = 500 }

  let Find predicate =
    match List.tryFind predicate cars with
    | None -> Either.Left { Message = "Could not find car"; StatusCode = 404 }
    | Some car -> Either.Right car

  let Delete make =
    cars <- cars |> List.filter (fun car -> car.Make <> make)

  let Length = cars.Length