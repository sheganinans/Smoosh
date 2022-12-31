module Smoosh.Utils

open System

type SByte with
  static member inline ToZigZag n = uint8 ((n <<< 1) ^^^ (n >>> 7))
  static member inline FromZigZag n = int8 ((n >>> 1) ^^^ (uint8 (- (int8 (n &&& 1uy)))))

type Int16 with
  static member inline ToZigZag n = uint16 ((n <<< 1) ^^^ (n >>> 15))
  static member inline FromZigZag n = int16 ((n >>> 1) ^^^ (uint16 (- (int16 (n &&& 1us)))))

type Int32 with
  static member inline ToZigZag n = uint32 ((n <<< 1) ^^^ (n >>> 31))
  static member inline FromZigZag n = int32 ((n >>> 1) ^^^ (uint32 (- (int32 (n &&& 1u)))))

type Int64 with
  static member inline ToZigZag n = uint64 ((n <<< 1) ^^^ (n >>> 63))
  static member inline FromZigZag n = int64 ((n >>> 1) ^^^ (uint64 (- (int64 (n &&& 1uL)))))

type UInt16 with
  member inline this.ToBytes =
    let bs = BitConverter.GetBytes this |> Array.rev
    if this <= 0xffus then bs[1..] else bs

let inline boolArrToByte (bs : bool []) =
  let mutable acc = 0uy
  let mutable i = 0
  for b in bs do
    if b then acc <- acc ||| (1uy <<< (7 - i))
    i <- i + 1
  acc

let inline byteArrayToBools (bs : byte []) =
    bs
    |> Array.map (fun b -> Array.init 8 (fun i -> ((0x80uy >>> i) &&& b) <> 0uy))
    |> Array.concat

let inline boolArrayToByteArray (bs : bool []) =
  seq {
    let mutable by = 0uy
    let mutable i = 0
    for b in bs do
      if i % 8 = 0 && i <> 0 then yield by; by <- 0uy
      if b then by <- by + byte (1 <<< (i % 8))
      i <- i + 1
    yield by
  } |> Array.ofSeq

let inline internal bytes bs = Seq.chunkBySize 8 bs

let inline internal asBytes bs = bs |> bytes |> Seq.map boolArrToByte |> Array.ofSeq

let inline bitsReqToStoreNumber (i : int) = int (ceil (Math.Log2 i))