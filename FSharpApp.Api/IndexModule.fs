namespace FSharpApp.Api

open FSharpApp.Core
open Nancy
open Nancy.Responses.Negotiation

type IndexModule() as x =
    inherit NancyModule()

    do x.Get.["/"] <- fun _ ->
      box(x.Negotiate
          |> Negotiator.WithContentType "application/json"
          |> Negotiator.WithModel (User.create 1 "cdedios" "Carlos" "De Dios"))