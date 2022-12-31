module Smoosh.Latency.Decoder

open System
open System.Numerics
open System.Text

open TypeShape.HKT

open Smoosh.TypeBuilder
open Smoosh.Utils

open Shared.Decoder

let inline private dSmooshUint16 (s : State) : uint16 =
  (if not (dBool s) then [|dByte s; 0uy|] else [|dByte s; dByte s|] |> Array.rev)
  |> BitConverter.ToUInt16

type Dec<'a> = Func<State, 'a>
type FieldDec<'a, 'b> = Func<State, 'a, 'b>

type Decoder = static member Assign(_ : App<Decoder, 'a>, _ : Dec<'a>) = ()
type FieldDecoder = static member Assign(_ : App<FieldDecoder, 'a>, _ : FieldDec<'a,'b>) = ()

let rec decoderBuilder =
  { new ITypeBuilder<Decoder, FieldDecoder> with
    member _.Unit () = HKT.pack (Dec (fun s -> ()))
    member _.Bool () = HKT.pack (Dec dBool)
    member _.Byte () = HKT.pack (Dec dByte)
    member _.SByte() = HKT.pack (Dec (fun s -> dByte s |> SByte.FromZigZag))
    member _.Char () = HKT.pack (Dec (fun s -> [|dByte s; dByte s|] |> BitConverter.ToChar))

    member _.Int16 () = HKT.pack (Dec (fun s -> dUint16 s |> Int16.FromZigZag))
    member _.Int32 () = HKT.pack (Dec (fun s -> dUint32 s |> Int32.FromZigZag))
    member _.Int64 () = HKT.pack (Dec (fun s -> dUint64 s |> Int64.FromZigZag))

    member _.UInt16 () = HKT.pack (Dec dUint16)
    member _.UInt32 () = HKT.pack (Dec dUint32)
    member _.UInt64 () = HKT.pack (Dec dUint64)

    member _.Single () = HKT.pack (Dec (fun s -> [|dByte s; dByte s; dByte s; dByte s|] |> BitConverter.ToSingle))
    member _.Double () = HKT.pack (Dec (fun s -> [|dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s|] |> BitConverter.ToDouble))
    member _.Decimal() = HKT.pack (Dec (fun s -> [|dUint32 s; dUint32 s; dUint32 s; dUint32 s|] |> Array.map Int32.FromZigZag |> Decimal))
    member _.BigInt () = HKT.pack (Dec (fun s -> [|1..int (dByte s)|] |> Array.map (fun _ -> dByte s) |> BigInteger))

    member _.String () = HKT.pack (Dec (fun s ->
      if not (dBool s) then null
      else [|1..dSmooshUint16 s |> int|] |> Array.map (fun _ -> dByte s) |> Encoding.Unicode.GetString ))

    member _.Guid () = HKT.pack (Dec (fun s -> [|dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s
                                                 dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s|] |> Guid))

    member _.TimeSpan () = HKT.pack (Dec (fun s -> dUint64 s |> Int64.FromZigZag |> TimeSpan))
    member _.DateTime () = HKT.pack (Dec (fun s -> dUint64 s |> Int64.FromZigZag |> DateTime))
    member _.DateTimeOffset () = HKT.pack (Dec (fun s -> DateTimeOffset (dUint64 s |> Int64.FromZigZag, TimeSpan (dUint64 s |> Int64.FromZigZag))))

    member _.Nullable (HKT.Unpack dec) = HKT.pack (Dec (fun s -> if not (dBool s) then Nullable () else Nullable (dec.Invoke s)))

    member _.Enum (HKT.Unpack dec) = HKT.pack (Dec (fun s -> LanguagePrimitives.EnumOfValue (dec.Invoke s)))
    member _.Array (HKT.Unpack dec) = HKT.pack (Dec (fun s -> [|1..dSmooshUint16 s |> int|] |> Array.map (fun _ -> dec.Invoke s)))

    member _.Option (HKT.Unpack dec) = HKT.pack (Dec (fun s -> if dBool s then Some (dec.Invoke s) else None))

    member _.List (HKT.Unpack dec) = HKT.pack (Dec (fun s ->
      let mutable acc : 'a list = []
      while dBool s do acc <- dec.Invoke s :: acc
      acc |> List.rev))

    member _.Set (HKT.Unpack dec) = HKT.pack (Dec (fun s -> [|1..dSmooshUint16 s |> int|] |> Array.map (fun _ -> dec.Invoke s) |> Set.ofArray))

    member _.Map (HKT.Unpack kdec) (HKT.Unpack vdec) = HKT.pack (Dec (fun s ->
      [|1..dSmooshUint16 s |> int|] |> Array.map (fun _ -> kdec.Invoke s, vdec.Invoke s) |> Map.ofArray))

    member _.Field shape (HKT.Unpack dec) = HKT.pack (FieldDec (fun src tgt -> shape.Set tgt (dec.Invoke src)))

    member _.Tuple shape (HKT.Unpacks fields) = HKT.pack (Dec (fun s ->
      let mutable r = shape.CreateUninitialized ()
      for f in fields do r <- f.Invoke (s,r)
      r))

    member _.Record shape (HKT.Unpacks fields) = HKT.pack (Dec (fun s ->
      let mutable r = shape.CreateUninitialized ()
      for f in fields do r <- f.Invoke (s,r)
      r))

    member _.Union shape (HKT.Unpackss fieldss) =
      let header_len = shape.UnionCases.Length |> bitsReqToStoreNumber
      HKT.pack (Dec (fun s ->
        let bs = ResizeArray<bool>()
        [|1..8-header_len|] |> Array.iter (fun _ -> bs.Add false)
        [|1..header_len|] |> Array.iter (fun _ -> bs.Add (dBool s))
        let idx = bs.ToArray () |> boolArrToByte |> int
        let case = shape.UnionCases[idx]
        let mutable ret = case.CreateUninitialized ()
        if case.Arity <> 0 then for f in fieldss[idx] do ret <- f.Invoke (s, ret)
        ret))

    member _.Ref (HKT.Unpack dec) = HKT.pack (Dec (fun s -> ref (dec.Invoke s)))

    member _.CliMutable shape (HKT.Unpacks fields) = HKT.pack (Dec (fun s ->
      let mutable r = shape.CreateUninitialized ()
      for f in fields do r <- f.Invoke (s,r)
      r))

    member _.Delay f = HKT.pack (Dec (fun s -> (HKT.unpack f.Value).Invoke s))
  }

let mkDecoder<'t> () : byte [] -> 't =
    let action = TypeBuilder.fold decoderBuilder |> HKT.unpack
    fun bs ->
      action.Invoke {
        Arr    = bs
        ArrIdx = 0
        BitIdx = 0
      }

