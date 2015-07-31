namespace FSharpApp.Core

type Car (make:string, model:string) =
    member x.Make = make
    member x.Model = model