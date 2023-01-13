module Smoosh.Encoder

open System
open System.Runtime.CompilerServices
open System.Text

open TypeShape.HKT

open Smoosh.TypeBuilder
open Smoosh.Utils


type Stream =
  | Bools of bool []
  | Bytes of byte []
  | End

open Smoosh.Attributes

type Enc<'a> = Func<Attr option, 'a, Stream seq>

type Encoder = static member Assign(_ : App<Encoder, 'a>, _ : Enc<'a>) = ()

let inline eSmooshed (xs : byte []) =
  xs
  |> Array.rev
  |> Array.skipWhile (fun x -> x = 0x00uy)
  |> fun arr ->
    seq {
      for b in arr do
        Bools [|true|]
        Bytes [|b|]
      Bools [|false|]
    }

let inline eSmooshedUint16 (x : uint16) = x |> BitConverter.GetBytes |> eSmooshed
let inline eSmooshedUint32 (x : uint32) = x |> BitConverter.GetBytes |> eSmooshed
let inline eSmooshedUint64 (x : uint64) = x |> BitConverter.GetBytes |> eSmooshed


let inline eSmooshedInt16 (x : int16) = x |> Int16.ToZigZag |> eSmooshedUint16
let inline eSmooshedInt32 (x : int32) = x |> Int32.ToZigZag |> eSmooshedUint32
let inline eSmooshedInt64 (x : int64) = x |> Int64.ToZigZag |> eSmooshedUint64

let eStr (enc : string -> byte []) (xs : string) =
  if isNull xs
  then seq { Bools [|false|] }
  else
    let bs = xs |> enc
    seq {
      Bools [|true|]
      yield! bs.Length |> uint32 |> eSmooshedUint32
      Bytes bs
    }

let rec internal encoderBuilder =
  { new ITypeBuilder<Encoder, Encoder> with
    member _.Unit () = HKT.pack (Enc (fun _ x -> seq { Bools [||] }))
    member _.Bool () = HKT.pack (Enc (fun _ x -> seq { Bools [|x|] }))
    member _.Byte () = HKT.pack (Enc (fun _ x -> seq { Bytes [|x|] }))
    member _.SByte() = HKT.pack (Enc (fun _ x -> seq { Bytes [|SByte.ToZigZag x|] }))
    member _.Char () = HKT.pack (Enc (fun _ x -> seq { Bytes (BitConverter.GetBytes x) }))

    member _.Int16 () = HKT.pack (Enc (fun a x -> seq { match a with
                                                        | None -> Bytes (Int16.ToZigZag x |> BitConverter.GetBytes)
                                                        | Some Smoosh -> yield! eSmooshedInt16 x
                                                        | _ -> raise (Exception "Utf8 encoding not supported for `int16`") }))
    member _.Int32 () = HKT.pack (Enc (fun a x -> seq { match a with
                                                        | None -> Bytes (Int32.ToZigZag x |> BitConverter.GetBytes)
                                                        | Some Smoosh -> yield! eSmooshedInt32 x
                                                        | _ -> raise (Exception "Utf8 encoding not supported for `int32`") }))
    member _.Int64 () = HKT.pack (Enc (fun a x -> seq { match a with
                                                        | None -> Bytes (Int64.ToZigZag x |> BitConverter.GetBytes)
                                                        | Some Smoosh -> yield! eSmooshedInt64 x
                                                        | _ -> raise (Exception "Utf8 encoding not supported for `int64`") }))

    member _.UInt16 () = HKT.pack (Enc (fun a x -> seq { match a with
                                                         | None -> Bytes (x |> BitConverter.GetBytes)
                                                         | Some Smoosh -> yield! eSmooshedUint16 x
                                                         | _ -> raise (Exception "Utf8 encoding not supported for `uint16`") }))
    member _.UInt32 () = HKT.pack (Enc (fun a x -> seq { match a with
                                                         | None -> Bytes (x |> BitConverter.GetBytes)
                                                         | Some Smoosh -> yield! eSmooshedUint32 x
                                                         | _ -> raise (Exception "Utf8 encoding not supported for `uint32`") }))
    member _.UInt64 () = HKT.pack (Enc (fun a x -> seq { match a with
                                                         | None -> Bytes (x |> BitConverter.GetBytes)
                                                         | Some Smoosh -> yield! eSmooshedUint64 x
                                                         | _ -> raise (Exception "Utf8 encoding not supported for `uint64`") }))

    member _.Single () = HKT.pack (Enc (fun _ x -> seq { Bytes (BitConverter.GetBytes x) }))
    member _.Double () = HKT.pack (Enc (fun _ x -> seq { Bytes (BitConverter.GetBytes x) }))
    member _.Decimal() = HKT.pack (Enc (fun _ x -> seq { Bytes (x |> Decimal.GetBits |> Array.collect (Int32.ToZigZag >> BitConverter.GetBytes)) }))

    member _.BigInt () = HKT.pack (Enc (fun _ x ->
      let bs = x.ToByteArray ()
      seq {
        yield! bs.Length |> uint32 |> eSmooshedUint32
        Bytes bs
      }))

    member _.String () = HKT.pack (Enc (fun a xs ->
      match a with
      | None -> eStr Encoding.Unicode.GetBytes xs
      | Some Utf8 -> eStr (fun s -> Encoding.Convert (Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes s)) xs
      | Some _ -> raise (Exception "")))

    member _.Guid () = HKT.pack (Enc (fun _ x -> seq { Bytes (x.ToByteArray ()) }))

    member _.TimeSpan () = HKT.pack (Enc (fun _ x -> seq { Bytes (x.Ticks |> Int64.ToZigZag |> BitConverter.GetBytes) }))
    member _.DateTime () = HKT.pack (Enc (fun _ x -> seq { Bytes (x.Ticks |> Int64.ToZigZag |> BitConverter.GetBytes) }))
    member _.DateTimeOffset () = HKT.pack (Enc (fun _ x ->
      seq {
        Bytes (x       .Ticks |> Int64.ToZigZag |> BitConverter.GetBytes)
        Bytes (x.Offset.Ticks |> Int64.ToZigZag |> BitConverter.GetBytes)
      }))

    member _.Nullable (HKT.Unpack enc) = HKT.pack (Enc (fun a x ->
      if not x.HasValue
      then seq { Bools [|false|] }
      else seq { Bools [|true|];  yield! enc.Invoke (a, x.Value) }))

    member _.Enum (HKT.Unpack enc) = HKT.pack (Enc (fun a x -> seq { yield! enc.Invoke (a, LanguagePrimitives.EnumToValue x) }))

    member _.Array (HKT.Unpack (enc : Enc<'t>)) =
        HKT.pack (Enc (fun a xs ->
          let ty = xs.GetType ()
          seq {
            yield! xs.Length |> uint32 |> eSmooshedUint32
            match ty.GetElementType () with
            | ty when ty = typeof<bool> -> Bools (xs |> unbox<bool []>)
            | ty when ty = typeof<byte> -> Bytes (xs |> unbox<byte []>)
            | _ -> for x in xs do yield! enc.Invoke (a, x)
          }))

    member _.Option (HKT.Unpack enc) = HKT.pack (Enc (fun a x ->
      if x.IsNone
      then seq { Bools [|false|] }
      else seq {
        Bools [|true|]
        yield! enc.Invoke (a, x.Value)
      }))

    member _.List (HKT.Unpack enc) = HKT.pack (Enc (fun a xs ->
      seq {
        for x in xs do
          yield Bools [|true|]
          yield! enc.Invoke (a, x)
        yield Bools [|false|]
      }))

    member _.Set (HKT.Unpack enc) = HKT.pack (Enc (fun a xs ->
      seq {
        yield! xs.Count |> uint32 |> eSmooshedUint32
        for x in xs do yield! enc.Invoke (a, x)
      }))

    member _.Map (HKT.Unpack kenc) (HKT.Unpack venc) = HKT.pack (Enc (fun a xs ->
      seq {
        yield! xs.Count |> uint32 |> eSmooshedUint32
        for kv in xs do
          yield! kenc.Invoke (a, kv.Key)
          yield! venc.Invoke (a, kv.Value)
      }))

    member _.Field shape (HKT.Unpack enc) = HKT.pack (Enc (fun a x -> seq { yield! enc.Invoke (a, shape.Get x) }))

    member _.Tuple shape (HKT.Unpacks fields) = HKT.pack (Enc (fun a x -> seq { for f in fields do yield! f.Invoke (a, x) }))

    member _.Record shape (HKT.Unpacks fields) =
      let fns : ('a -> Stream seq) [] =
        Array.zip fields shape.Fields
        |> Array.map (fun (f, sf) ->
            let attrs =
              sf.MemberInfo.CustomAttributes
              |> Array.ofSeq
              |> Array.filter (fun a -> a.AttributeType = typeof<Utf8Attribute>
                                     || a.AttributeType = typeof<SmooshAttribute>)
            match attrs with
            | [||] -> fun x -> f.Invoke (None, x)
            | [|a|] ->
              if a.AttributeType = typeof<Utf8Attribute>
              then fun x -> f.Invoke (Some   Utf8, x)
              else fun x -> f.Invoke (Some Smoosh, x)
            | _ -> raise (Exception "Cannot have both attributes"))

      HKT.pack (Enc (fun _ x -> fns |> Seq.collect (fun f -> f x)))

    member _.Union shape (HKT.Unpackss fieldss) =
      let header_len = shape.UnionCases.Length |> bitsReqToStoreNumber
      let headers =
        [|0..shape.UnionCases.Length-1|]
        |> Array.map (fun i ->
          i
            |> BitConverter.GetBytes
            |> Array.take ((header_len / 8) + 1)
            |> Array.rev
            |> byteArrayToBools
            |> fun padded_header -> padded_header |> Array.skip (padded_header.Length - header_len))

      HKT.pack (Enc (fun a x ->
        let idx = shape.GetTag x
        let header = headers[idx]
        seq {
          Bools header
          if shape.UnionCases[idx].Arity > 0 then for f in fieldss[idx] do yield! f.Invoke (a, x)
        }))

    member _.Ref (HKT.Unpack enc) = HKT.pack (Enc (fun a x -> seq { yield! enc.Invoke (a, x.Value) }))

    member _.CliMutable shape (HKT.Unpacks fields) = HKT.pack (Enc (fun a x -> seq { for f in fields do yield! f.Invoke (a, x) }))

    member _.Delay f = HKT.pack (Enc (fun a x -> seq { yield! (HKT.unpack f.Value).Invoke (a, x) }))
  }

let foldStream (xs : Stream seq) : byte seq =
  let mutable acc = 0x0uy
  let mutable idx = 0
  seq {
    for x in Seq.append xs [End] do
      match x with
      | Bools bs ->
        for b in bs do
          match idx with
          | 7 ->
            if b then acc <- acc ||| 1uy
            yield acc
            acc <- 0uy
            idx <- 0
          | _ ->
            if b then acc <- acc ||| (128uy >>> idx)
            idx <- idx + 1
      | Bytes bs ->
          match idx with
          | 0 -> for b in bs do yield b
          | _ ->
            for b in bs do
              yield acc ||| (b >>> idx)
              acc <- b <<< (8 - idx)
      | End -> yield acc
  }

let mkEncoder<'t> () : 't -> byte seq =
  let action = TypeBuilder.fold encoderBuilder |> HKT.unpack
  fun x -> action.Invoke (None, x) |> foldStream
