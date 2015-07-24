namespace FWebApi

type Type = { Success: bool;  Name: string }
type RegularResponse = { content: string }

open Nancy
open Nancy.Responses.Negotiation

type IndexModule() as x =
    inherit NancyModule()

    do x.Get.["/"] <- fun _ ->
      box(x.Negotiate
          |> Negotiator.WithContentType "application/json"
          |> Negotiator.WithModel { content = "Hello Paul, from F#!" })