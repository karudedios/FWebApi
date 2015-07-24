[<AutoOpen>]
module Either

type Either<'a, 'b> =
  | Left of 'a
  | Right of 'b

let Match (left: 'a -> 'c) (right: 'b -> 'c) (either:Either<'a, 'b>) : 'c =
  match either with
  | Left l -> left l
  | Right r -> right r