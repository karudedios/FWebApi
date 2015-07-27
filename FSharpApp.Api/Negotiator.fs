namespace FSharpApp.Api

open Nancy
open Nancy.Responses.Negotiation

module Negotiator =
  let WithContentType = (fun (contentType:string) (negotiator:Negotiator) ->
    (negotiator
      .WithContentType contentType)
      .WithAllowedMediaRange (MediaRange.FromString contentType))

  let WithModel = (fun (model: 'T) (negotiator:Negotiator) -> negotiator.WithModel model)