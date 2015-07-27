namespace FSharpApp.Api

open FSharpApp.Service

open Nancy
open Nancy.Responses.Negotiation

type IndexModule() as x =
    inherit NancyModule()

    let (?) (p: obj) (prop) = 
        let ddv = (p :?> DynamicDictionary).[prop] :?> DynamicDictionaryValue
        match ddv.HasValue with
        | false -> None
        | _ -> ddv.TryParse<'a>() |> Some

    do x.Get.["/{id}"] <- (fun parameters ->
      let response =
        x.Negotiate
            |> Negotiator.WithContentType "application/json"
            |> Negotiator.WithModel (UserService.find parameters?id)

      box response)
          
    do x.Post.["/"] <- (fun _ ->
      let response =
        x.Negotiate
          |> Negotiator.WithContentType "application/json"
          |> Negotiator.WithModel (UserService.create "cdedios" "Carlos" "De Dios")

      box response)