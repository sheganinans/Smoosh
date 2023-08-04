module Smoosh.Decoder

open System
open System.Numerics
open System.Text

open TypeShape.HKT

open Smoosh.TypeBuilder
open Smoosh.Utils

open Shared.Decoder

open Smoosh.Attributes

type Dec<'a> = Func<Attr option, State, 'a>
type FieldDec<'a, 'b> = Func<Attr option, State, 'a, 'b>

type Decoder = static member Assign(_ : App<Decoder, 'a>, _ : Dec<'a>) = ()
type FieldDecoder = static member Assign(_ : App<FieldDecoder, 'a>, _ : FieldDec<'a,'b>) = ()

let private dSmooshedInt s =
  let mutable acc : byte list = []
  while dBool s do acc <- dByte s :: acc
  acc
  |> Array.ofList
  |> Array.rev
  |> Array.append (Array.zeroCreate (8 - acc.Length))

let inline private dSmooshedUint16 s = dSmooshedInt s |> Array.rev |> BitConverter.ToUInt16
let inline private dSmooshedUint32 s = dSmooshedInt s |> Array.rev |> BitConverter.ToUInt32
let inline private dSmooshedUint64 s = dSmooshedInt s |> Array.rev |> BitConverter.ToUInt64

let inline private dSmooshedInt16 s = dSmooshedUint16 s |> Int16.FromZigZag
let inline private dSmooshedInt32 s = dSmooshedUint32 s |> Int32.FromZigZag
let inline private dSmooshedInt64 s = dSmooshedUint64 s |> Int64.FromZigZag


let decoderBuilder =
  { new ITypeBuilder<Decoder, FieldDecoder> with
    member _.Unit () = HKT.pack (Dec (fun _ _ -> ()))
    member _.Bool () = HKT.pack (Dec (fun _ -> dBool))
    member _.Byte () = HKT.pack (Dec (fun _ -> dByte))
    member _.SByte() = HKT.pack (Dec (fun _ s -> dByte s |> SByte.FromZigZag))
    member _.Char () = HKT.pack (Dec (fun _ s -> [|dByte s; dByte s|] |> BitConverter.ToChar))

    member _.Int16 () = HKT.pack (Dec (fun a ->
      match a with
      | None -> dUint16 >> Int16.FromZigZag
      | Some Smoosh -> fun s -> dSmooshedInt16 s
      | _ -> raise (Exception "Utf8 encoding not supported for `int16`")))

    member _.Int32 () = HKT.pack (Dec (fun a ->
      match a with
      | None -> dUint32 >> Int32.FromZigZag
      | Some Smoosh -> fun s -> dSmooshedInt32 s
      | _ -> raise (Exception "Utf8 encoding not supported for `int32`")))

    member _.Int64 () = HKT.pack (Dec (fun a ->
      match a with
      | None -> dUint64 >> Int64.FromZigZag
      | Some Smoosh -> fun s -> dSmooshedInt64 s
      | _ -> raise (Exception "Utf8 encoding not supported for `int64`")))

    member _.UInt16 () = HKT.pack (Dec (fun a ->
      match a with
      | None -> dUint16
      | Some Smoosh -> fun s -> dSmooshedUint16 s
      | _ -> raise (Exception "Utf8 encoding not supported for `uint16`")))

    member _.UInt32 () = HKT.pack (Dec (fun a ->
      match a with
      | None -> dUint32
      | Some Smoosh -> fun s -> dSmooshedUint32 s
      | _ -> raise (Exception "Utf8 encoding not supported for `uint32`")))

    member _.UInt64 () = HKT.pack (Dec (fun a ->
      match a with
      | None -> dUint64
      | Some Smoosh -> fun s -> dSmooshedUint64 s
      | _ -> raise (Exception "Utf8 encoding not supported for `uint64`")))

    member _.Single () = HKT.pack (Dec (fun _ s -> [|dByte s; dByte s; dByte s; dByte s|] |> BitConverter.ToSingle))
    member _.Double () = HKT.pack (Dec (fun _ s -> [|dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s|] |> BitConverter.ToDouble))
    member _.Decimal() = HKT.pack (Dec (fun _ s -> [|dUint32 s; dUint32 s; dUint32 s; dUint32 s|] |> Array.map Int32.FromZigZag |> Decimal))
    member _.BigInt () = HKT.pack (Dec (fun _ s -> [|1..int <| dSmooshedUint32 s|] |> Array.map (fun _ -> dByte s) |> BigInteger))

    member _.String () = HKT.pack (Dec (fun a s ->
      if not (dBool s) then null
      else [|1..int <| dSmooshedUint32 s|]
           |> Array.map (fun _ -> dByte s)
           |> match a with
              | None -> Encoding.Unicode.GetString
              | Some Utf8 -> Encoding.UTF8.GetString
              | Some _ -> raise (Exception "Smoosh encoding not supported for `string`")))

    member _.Guid () = HKT.pack (Dec (fun _ s -> [|dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s
                                                   dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s|] |> Guid))

    member _.TimeSpan () = HKT.pack (Dec (fun _ s -> dUint64 s |> Int64.FromZigZag |> TimeSpan))
    member _.DateTime () = HKT.pack (Dec (fun _ s -> dUint64 s |> Int64.FromZigZag |> DateTime))
    member _.DateTimeOffset () = HKT.pack (Dec (fun _ s -> DateTimeOffset (dUint64 s |> Int64.FromZigZag, TimeSpan (dUint64 s |> Int64.FromZigZag))))

    member _.Nullable (HKT.Unpack dec) = HKT.pack (Dec (fun a s -> if not (dBool s) then Nullable () else Nullable (dec.Invoke (a,s))))

    member _.Enum (HKT.Unpack dec) = HKT.pack (Dec (fun a s -> LanguagePrimitives.EnumOfValue (dec.Invoke (a,s))))
    member _.Array (HKT.Unpack dec) = HKT.pack (Dec (fun a s -> [|1..int <| dSmooshedUint32 s|] |> Array.map (fun _ -> dec.Invoke (a,s))))

    member _.Option (HKT.Unpack dec) = HKT.pack (Dec (fun a s -> if dBool s then Some (dec.Invoke (a,s)) else None))

    member _.List (HKT.Unpack dec) = HKT.pack (Dec (fun a s ->
      let mutable acc : 'a list = []
      while dBool s do acc <- dec.Invoke (a,s) :: acc
      acc |> List.rev))

    member _.Set (HKT.Unpack dec) = HKT.pack (Dec (fun a s -> [|1..int <| dSmooshedUint32 s|] |> Array.map (fun _ -> dec.Invoke (a,s)) |> Set.ofArray))

    member _.Map (HKT.Unpack kdec) (HKT.Unpack vdec) = HKT.pack (Dec (fun a s ->
      [|1..int <| dSmooshedUint32 s|] |> Array.map (fun _ -> kdec.Invoke (a,s), vdec.Invoke (a,s)) |> Map.ofArray))

    member _.Field shape (HKT.Unpack dec) = HKT.pack (FieldDec (fun a src tgt -> shape.Set tgt (dec.Invoke (a,src))))

    member _.Tuple shape (HKT.Unpacks fields) = HKT.pack (Dec (fun a s ->
      let mutable r = shape.CreateUninitialized ()
      for f in fields do r <- f.Invoke (a,s,r)
      r))

    member _.Record shape (HKT.Unpacks fields) =
      let fns =
        Array.zip fields shape.Fields
        |> Array.map (fun (f, sf) ->
            let attrs =
              sf.MemberInfo.CustomAttributes
              |> Array.ofSeq
              |> Array.filter (fun a -> a.AttributeType = typeof<Utf8Attribute>
                                     || a.AttributeType = typeof<SmooshAttribute>)
            match attrs with
            | [||] -> fun s r -> f.Invoke (None, s, r)
            | [|a|] ->
              if a.AttributeType = typeof<Utf8Attribute>
              then fun s r -> f.Invoke (Some   Utf8, s, r)
              else fun s r -> f.Invoke (Some Smoosh, s, r)
            | _ -> raise (Exception "Cannot have both attributes"))

      HKT.pack (Dec (fun a s -> fns |> Array.fold (fun r f -> f s r) (shape.CreateUninitialized ())))

    member _.Union shape (HKT.Unpackss fieldss) =
      let header_len = shape.UnionCases.Length |> bitsReqToStoreNumber
      HKT.pack (Dec (fun a s ->
        let bs = Array.zeroCreate 8 |> Array.mapi (fun i x -> if i < 8 - header_len then x else dBool s)
        let idx = bs |> boolArrToByte |> int
        let case = shape.UnionCases[idx]
        let mutable ret = case.CreateUninitialized ()
        if case.Arity <> 0 then for f in fieldss[idx] do ret <- f.Invoke (a,s,ret)
        ret))

    member _.Ref (HKT.Unpack dec) = HKT.pack (Dec (fun a s -> ref (dec.Invoke (a,s))))

    member _.CliMutable shape (HKT.Unpacks fields) = HKT.pack (Dec (fun a s ->
      let mutable r = shape.CreateUninitialized ()
      for f in fields do r <- f.Invoke (a,s,r)
      r))

    member _.Delay f = HKT.pack (Dec (fun a s -> (HKT.unpack f.Value).Invoke (a,s)))
  }

open Smoosh.TypeHash

type DecodeError = HashDoesNotMatch

let mkDecoder<'t> () : byte [] -> Result<'t, DecodeError> =
    let action = TypeBuilder.fold decoderBuilder |> HKT.unpack
    fun bs ->
      use md5 = System.Security.Cryptography.MD5.Create ()
      let ty =  mkTyHash<'t> () |> System.Text.Encoding.Unicode.GetBytes |> md5.ComputeHash
      let hash = bs[..15]
      if ty = hash
      then
        Ok
          (action.Invoke (None, {
            Arr    = bs[16..]
            ArrIdx = 0
            BitIdx = 0
          }))
      else Error HashDoesNotMatch

