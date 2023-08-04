module Smoosh.TypeHash

open System
open System.Text

open TypeShape.Core.Utils
open TypeShape.HKT

open Smoosh.TypeBuilder

// Inspired by: https://hackage.haskell.org/package/typehash

type TyH<'a> = Action<StringBuilder, 'a>

type TyHash = static member Assign(_ : App<TyHash, 'a>, _ : TyH<'a>) = ()

let internal tyHashBuilder =
  let inline append (sb : StringBuilder) (value : string) = sb.Append(value) |> ignore
  let inline con (sb : StringBuilder) c (xs : (unit -> unit) []) =
    append sb c; append sb "("; xs |> Array.iter (fun f -> f (); append sb ","); append sb ")"

  { new ITypeBuilder<TyHash, TyHash> with
    member _.Unit () = HKT.pack (TyH (fun sb _ -> append sb "()"))
    member _.Bool () = HKT.pack (TyH (fun sb _ -> append sb "bool"))
    member _.Byte () = HKT.pack (TyH (fun sb _ -> append sb "byte"))
    member _.SByte() = HKT.pack (TyH (fun sb _ -> append sb "sbyte"))
    member _.Char () = HKT.pack (TyH (fun sb _ -> append sb "char"))
    member _.Int16 () = HKT.pack (TyH (fun sb _ -> append sb "int16"))
    member _.Int32 () = HKT.pack (TyH (fun sb _ -> append sb "int32"))
    member _.Int64 () = HKT.pack (TyH (fun sb _ -> append sb "int64"))
    member _.UInt16 () = HKT.pack (TyH (fun sb _ -> append sb "uint16"))
    member _.UInt32 () = HKT.pack (TyH (fun sb _ -> append sb "uint32"))
    member _.UInt64 () = HKT.pack (TyH (fun sb _ -> append sb "uint64"))
    member _.Single () = HKT.pack (TyH (fun sb _ -> append sb "single"))
    member _.Double () = HKT.pack (TyH (fun sb _ -> append sb "double"))
    member _.Decimal() = HKT.pack (TyH (fun sb _ -> append sb "decimal"))
    member _.BigInt () = HKT.pack (TyH (fun sb _ -> append sb "bigint"))
    member _.String () = HKT.pack (TyH (fun sb _ -> append sb "string"))
    member _.Guid () = HKT.pack (TyH (fun sb _ -> append sb "guid"))
    member _.TimeSpan () = HKT.pack (TyH (fun sb _ -> append sb "timespan"))
    member _.DateTime () = HKT.pack (TyH (fun sb _ -> append sb "datetime"))
    member _.DateTimeOffset () = HKT.pack (TyH (fun sb _ -> append sb "datetimeoffset"))

    member _.Nullable (HKT.Unpack tyh) = HKT.pack (TyH (fun sb _ ->
      con sb "nullable" [|fun _ -> tyh.Invoke (Unchecked.defaultof<_>)|]))

    member _.Enum _ = HKT.pack (TyH (fun sb (_ : 'e) ->
      let t = typeof<'e>
      append sb (t.Assembly.GetName().Name)
      append sb "."
      append sb t.Name))

    member _.Array (HKT.Unpack tyh) = HKT.pack (TyH (fun sb _ ->
      con sb "[]" [|fun _ -> tyh.Invoke (Unchecked.defaultof<_>)|]))
    member _.Option (HKT.Unpack tyh) = HKT.pack (TyH (fun sb _ ->
      con sb "option" [|fun _ -> tyh.Invoke (Unchecked.defaultof<_>)|]))
    member _.List (HKT.Unpack tyh) = HKT.pack (TyH (fun sb _ ->
      con sb "map" [|fun _ -> tyh.Invoke (sb, Unchecked.defaultof<_>)|]))
    member _.Set (HKT.Unpack tyh) = HKT.pack (TyH (fun sb _ ->
      con sb "set" [|fun _ -> tyh.Invoke (sb, Unchecked.defaultof<_>)|]))

    member _.Map (HKT.Unpack ktyh) (HKT.Unpack vtyh) =
      HKT.pack (TyH (fun sb _ -> con sb "map" [|
        fun _ -> ktyh.Invoke (sb, Unchecked.defaultof<_>)
        fun _ -> vtyh.Invoke (sb, Unchecked.defaultof<_>)|]))

    member _.Field shape (HKT.Unpack tyh) = HKT.pack (TyH (fun sb _ ->
      append sb shape.Label
      append sb ":"
      tyh.Invoke (sb, Unchecked.defaultof<_>)
      append sb ";"))

    member _.Tuple _ (HKT.Unpacks fields) = HKT.pack (TyH (fun sb _ ->
      append sb "("
      fields |> Array.iter (fun f -> f.Invoke (sb, Unchecked.defaultof<_>))
      append sb ")"))

    member _.Record _ (HKT.Unpacks fields) = HKT.pack (TyH (fun sb _ ->
      append sb "{"
      fields |> Array.iter (fun f -> f.Invoke (sb, Unchecked.defaultof<_>))
      append sb "}"))

    member _.Union _ _ = HKT.pack (TyH (fun sb (_ : 'u) ->
      let t = typeof<'u>
      append sb (t.Assembly.GetName().Name)
      append sb "."
      append sb t.Name))

    member _.Ref (HKT.Unpack tyh) = HKT.pack (TyH (fun sb _ ->
      con sb "ref" [|fun _ -> tyh.Invoke (sb, Unchecked.defaultof<_>)|]))

    member _.CliMutable _ (HKT.Unpacks fields) = HKT.pack (TyH (fun sb _ ->
      con sb "cli" (fields |> Array.map (fun f _ -> f.Invoke (sb, Unchecked.defaultof<_>)))))

    member _.Delay (_ : Cell<App<TyHash,'f>>) = HKT.pack (TyH (fun sb x ->
      con sb "delay" [|fun _ -> (HKT.unpack Unchecked.defaultof<App<TyHash,'f>>).Invoke (sb, Unchecked.defaultof<_>)|]))
  }

let mkTyHash<'t> () : string =
  let sb = StringBuilder ()
  (TypeBuilder.fold tyHashBuilder |> HKT.unpack).Invoke (sb, Unchecked.defaultof<'t>)
  sb.ToString ()
