module Smoosh.Latency.Encoder

open System
open TypeShape.HKT
open System.Text

open Smoosh.Utils
open Smoosh.TypeBuilder

type State =
  {
    Limit           : int
    mutable Arr     : byte []
    mutable ByteAcc : byte
    mutable ArrIdx  : int
    mutable BitIdx  : int
  }

exception MessageLimitExceeded

let inline private eTrue (s : State) =
  match s.BitIdx with
  | 7 ->
    s.Arr[s.ArrIdx] <- (s.ByteAcc ||| 1uy)
    s.ArrIdx <- s.ArrIdx + 1
    s.ByteAcc <- 0uy
    s.BitIdx <- 0
  | _ ->
    if s.ArrIdx > s.Limit then raise MessageLimitExceeded
    s.ByteAcc <- s.ByteAcc ||| (128uy >>> s.BitIdx)
    s.BitIdx <- s.BitIdx + 1

let inline private eFalse (s : State) =
  match s.BitIdx with
  | 7 ->
    s.Arr[s.ArrIdx] <-  s.ByteAcc
    s.ArrIdx <- s.ArrIdx + 1
    s.ByteAcc <- 0uy
    s.BitIdx <- 0
  | _ ->
    if s.ArrIdx > s.Limit then raise MessageLimitExceeded
    s.BitIdx <- s.BitIdx + 1

let inline private eBool s b = (if b then eTrue else eFalse) s

let inline private eByte (s : State) b =
  if (s.BitIdx = 0 && s.ArrIdx > s.Limit) || s.ArrIdx = s.Limit then raise MessageLimitExceeded
  match s.BitIdx with
  | 0 -> s.Arr[s.ArrIdx] <-  b
  | _ ->
    s.Arr[s.ArrIdx] <- s.ByteAcc ||| (b >>> s.BitIdx)
    s.ByteAcc <- b <<< (8 - s.BitIdx)
  s.ArrIdx <- s.ArrIdx + 1

let inline private eUint16 s (i : uint16) = Array.iter (eByte s) (BitConverter.GetBytes i)
let inline private eUint32 s (i : uint32) = Array.iter (eByte s) (BitConverter.GetBytes i)
let inline private eUint64 s (i : uint64) = Array.iter (eByte s) (BitConverter.GetBytes i)

let inline private eSmooshUint16 s (i : uint16) =
  let bs = BitConverter.GetBytes i |> Array.rev
  if i <= 0xffus
  then eFalse s;             eByte s  bs[1]
  else eTrue  s; Array.iter (eByte s) bs

let inline private asIs s bs = Array.iter (eByte s) bs

exception LargeCollection of string * int

type Enc<'a> = Action<State, 'a>

type Encoder = static member Assign(_ : App<Encoder, 'a>, _ : Enc<'a>) = ()

let internal encoderBuilder =
  { new ITypeBuilder<Encoder, Encoder> with
    member _.Unit () = HKT.pack (Enc (fun s x -> ()))
    member _.Bool () = HKT.pack (Enc eBool)
    member _.Byte () = HKT.pack (Enc eByte)
    member _.SByte() = HKT.pack (Enc (fun s x -> eByte s (SByte.ToZigZag x)))
    member _.Char () = HKT.pack (Enc (fun s x -> x |> BitConverter.GetBytes |> asIs s))

    member _.Int16 () = HKT.pack (Enc (fun s x -> eUint16 s (Int16.ToZigZag x)))
    member _.Int32 () = HKT.pack (Enc (fun s x -> eUint32 s (Int32.ToZigZag x)))
    member _.Int64 () = HKT.pack (Enc (fun s x -> eUint64 s (Int64.ToZigZag x)))

    member _.UInt16 () = HKT.pack (Enc eUint16)
    member _.UInt32 () = HKT.pack (Enc eUint32)
    member _.UInt64 () = HKT.pack (Enc eUint64)

    member _.Single () = HKT.pack (Enc (fun s x -> x |> BitConverter.GetBytes |> asIs s))
    member _.Double () = HKT.pack (Enc (fun s x -> x |> BitConverter.GetBytes |> asIs s))
    member _.Decimal() = HKT.pack (Enc (fun s x -> x |> Decimal.GetBits |> Array.iter (Int32.ToZigZag >> eUint32 s)))
    member _.BigInt () = HKT.pack (Enc (fun s x ->
      let bs = x.ToByteArray ()
      if bs.Length > 256 then raise (LargeCollection ("BigInt", bs.Length))
      eByte s (byte bs.Length)
      bs |> asIs s))

    member _.String () = HKT.pack (Enc (fun s xs ->
      if isNull xs then eFalse s
      else
        eTrue s
        let bs = xs |> Encoding.Unicode.GetBytes
        if bs.Length > 65_536 then raise (LargeCollection ("String", xs.Length))
        eSmooshUint16 s (uint16 bs.Length)
        bs |> asIs s))

    member _.Guid () = HKT.pack (Enc (fun s x -> x.ToByteArray () |> asIs s))

    member _.TimeSpan () = HKT.pack (Enc (fun s x -> x.Ticks |> Int64.ToZigZag |> eUint64 s))
    member _.DateTime () = HKT.pack (Enc (fun s x -> x.Ticks |> Int64.ToZigZag |> eUint64 s))
    member _.DateTimeOffset () = HKT.pack (Enc (fun s x ->
      x.Ticks |> Int64.ToZigZag |> eUint64 s
      x.Offset.Ticks |> Int64.ToZigZag |> eUint64 s))

    member _.Nullable (HKT.Unpack enc) = HKT.pack (Enc (fun s x ->
      if not x.HasValue
      then eFalse s
      else eTrue s; enc.Invoke(s, x.Value)))

    member _.Enum (HKT.Unpack enc) = HKT.pack (Enc (fun s x -> enc.Invoke (s, LanguagePrimitives.EnumToValue x)))
    member _.Array (HKT.Unpack enc) =
      HKT.pack (Enc (fun s xs ->
        if xs.Length > 65_536 then raise (LargeCollection ("array", xs.Length))
        eSmooshUint16 s (uint16 xs.Length)
        xs |> Array.iter (fun x -> enc.Invoke(s,x))))

    member _.Option (HKT.Unpack enc) = HKT.pack (Enc (fun s x ->
      if x.IsSome then eTrue s else eFalse s
      x |> Option.iter (fun x -> enc.Invoke(s,x))))

    member _.List (HKT.Unpack enc) = HKT.pack (Enc (fun s xs ->
      xs |> List.iter (fun x -> eTrue s; enc.Invoke(s,x))
      eFalse s))

    member _.Set (HKT.Unpack enc) = HKT.pack (Enc (fun s xs ->
      if xs.Count > 65_536 then raise (LargeCollection ("set", xs.Count))
      eSmooshUint16 s (uint16 xs.Count)
      xs |> Set.iter (fun x -> enc.Invoke(s,x))))

    member _.Map (HKT.Unpack kenc) (HKT.Unpack venc) = HKT.pack (Enc (fun s xs ->
      if xs.Count > 65_536 then raise (LargeCollection ("map", xs.Count))
      eSmooshUint16 s (uint16 xs.Count)
      xs |> Map.iter (fun k v -> kenc.Invoke(s, k); venc.Invoke(s, v))))

    member _.Field shape (HKT.Unpack enc) = HKT.pack (Enc (fun s x -> enc.Invoke (s, shape.Get x)))

    member _.Tuple shape (HKT.Unpacks fields) = HKT.pack (Enc (fun s x -> fields |> Array.iter (fun f -> f.Invoke(s,x))))

    member _.Record shape (HKT.Unpacks fields) = HKT.pack (Enc (fun s x -> fields |> Array.iter (fun enc -> enc.Invoke(s,x))))

    member _.Union shape (HKT.Unpackss fieldss) =
      let header_len = shape.UnionCases.Length |> bitsReqToStoreNumber
      let headers =
        [|0..shape.UnionCases.Length-1|]
        |> Array.map (fun i ->
          i
            |> BitConverter.GetBytes
            |> Array.rev
            |> byteArrayToBools
            |> fun padded_header -> padded_header |> Array.skip (padded_header.Length - header_len))

      HKT.pack (Enc (fun s x ->
        let idx = shape.GetTag x
        headers[idx] |> Array.iter (eBool s)
        if shape.UnionCases[idx].Arity = 0 then () else fieldss[idx] |> Array.iter (fun f -> f.Invoke(s,x))))


    member _.Ref (HKT.Unpack enc) = HKT.pack (Enc (fun s x -> enc.Invoke(s,x.Value)))

    member _.CliMutable shape (HKT.Unpacks fields) = HKT.pack (Enc (fun s x -> fields |> Array.iter (fun f -> f.Invoke(s,x))))

    member _.Delay f = HKT.pack (Enc (fun s x -> (HKT.unpack f.Value).Invoke(s, x)))
  }

let private MTU_DEFAULT_LIMIT = 1_472 // 1500 - ip header (20 bytes) - udp header (8 bytes)

let private finalizeMsg (s : State) =
  if s.ArrIdx = MTU_DEFAULT_LIMIT - 1
  then
    s.Arr[s.Limit] <- s.ByteAcc
    s.ArrIdx
  else
    eByte s 0uy
    s.ArrIdx-1

let mkEncoderBase<'t> (n : int) : 't -> byte [] =
    let action = TypeBuilder.fold encoderBuilder |> HKT.unpack
    fun t ->
      let s = {
          Arr     = Array.zeroCreate n
          Limit   = n - 1
          ArrIdx  = 0
          BitIdx  = 0
          ByteAcc = 0uy
        }
      action.Invoke (s, t)
      s.Arr[..finalizeMsg s]
      
let mkEncoder<'t> () = mkEncoderBase<'t> MTU_DEFAULT_LIMIT
let mkMTUIgnoringEncoder<'t> (n : int) = mkEncoderBase<'t> n
