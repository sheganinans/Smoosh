module Shared.Decoder

open System

type State =
  {
    Arr             : byte []
    mutable ArrIdx  : int
    mutable BitIdx  : int
  }

let inline dBool (s : State) : bool =
  let ret = (s.Arr[s.ArrIdx] >>> (7 - s.BitIdx)) &&& 1uy = 1uy
  if s.BitIdx = 7
  then
    s.BitIdx <- 0
    s.ArrIdx <- s.ArrIdx + 1
  else
    s.BitIdx <- s.BitIdx + 1
  ret

let inline dByte (s : State) : byte =
  let b1 = s.Arr[s.ArrIdx]
  s.ArrIdx <- s.ArrIdx + 1
  if s.BitIdx = 0
  then b1
  else (b1 <<< s.BitIdx) ||| (s.Arr[s.ArrIdx] >>> (8 - s.BitIdx))

let inline dUint16 s = [|dByte s; dByte s|] |> BitConverter.ToUInt16
let inline dUint32 s = [|dByte s; dByte s; dByte s; dByte s|] |> BitConverter.ToUInt32
let inline dUint64 s = [|dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s; dByte s|] |> BitConverter.ToUInt64

let inline asIs (s : State) (n : int) : byte [] = [|1 .. n|] |> Array.map (fun _ -> dByte s)
