open System
open Smoosh.Attributes
open Smoosh.Latency.Decoder
open Smoosh.Latency.Encoder

type Nested =
  { UnWrap : Nested option }
  member this.Wrap = { UnWrap = Some this }
      
let NestedEx3 = { UnWrap = None }.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap

mkEncoder<Nested> () NestedEx3 |> Array.ofSeq |> BitConverter.ToString |> printfn "%s"

mkEncoder<Nested> () NestedEx3 |> Array.ofSeq |> mkDecoder<Nested> () |> fun v -> printfn $"%A{v = NestedEx3}"

let n = [Some DateTime.Now;Some DateTime.Now]

//mkEncoder<DateTime option list>() n |> BitConverter.ToString |> printfn "%s"

//mkEncoder<DateTime option list> () n |> mkDecoder<DateTime option list> () |> fun v -> printfn $"%A{v = n}"

type Person =
  {
    Name     : string
    BirthDay : DateTime
    Phone    : string
    Siblings : int
    Spouse   : bool
    Money    : float
  }

let ace =
  {
      Name     = "Ace The Place"
      BirthDay = DateTime.Now
      Phone    = "+555-555-5555"
      Siblings = 2
      Spouse   = false
      Money    = 55_555.55
  }


let personEnc = mkEncoder<Person> ()
let personDec = mkDecoder<Person> ()

let eAce = personEnc ace |> Array.ofSeq

printfn "ace"

eAce |> BitConverter.ToString |> printfn "%s"
eAce |> personDec |> fun dAce -> printfn $"%i{eAce.Length}: %b{dAce = ace}"



type Dir = N | S | C | E | W

mkEncoder<Dir array> () [|N;S|] |> Array.ofSeq |> BitConverter.ToString |> printfn "%s"

mkEncoder<Dir array> () [|N;S|] |> Array.ofSeq |> mkDecoder<Dir array> () |> fun v -> printfn $"%A{v}"


type Tree =
  | Leaf
  | Node of byte * Tree * Tree

let rec mkTree d =
  if d = 0
  then Leaf
  else Node (0xF0uy, mkTree (d-1), mkTree (d-1))

let tree = mkTree 12

let treeEnc = mkMTUIgnoringEncoder<Tree> 6000
let treeDec = mkDecoder<Tree> ()

let eTree = tree |> treeEnc |> Array.ofSeq

eTree |> BitConverter.ToString |> printfn "%s"
eTree |> treeDec |> fun dTree -> printfn $"%i{eTree.Length}: %b{dTree = tree}"
//tree |> treeEnc |> treeDec |> fun dTree -> printfn $"%i{eTree.Length}: %b{dTree = tree}"

//mkEncoder () [|true;false;true|] |> Array.ofSeq |> fun xs -> printfn $"{xs.Length}"; xs |> BitConverter.ToString |> printfn "%s"

//mkEncoder<byte []>() [|0xffuy;0x00uy;0xffuy|] |> Array.ofSeq |> BitConverter.ToString |> printfn "%s"

//mkEncoder<Person> () ace |> Array.ofSeq |> mkDecoder<Person> () |> fun v -> printfn $"%A{v}"

Map.empty.Add ("foo", 345) |> mkEncoder<Map<string, int>> () |> mkDecoder<Map<string, int>> () |> printfn "%A"