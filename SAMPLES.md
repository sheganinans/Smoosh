# Samples

## Using attributes

You'll notice that adding and removing the attributes will change the length of the encoded message, at the cost of greater runtime overhead during conversion.

```fsharp
open Smoosh.Attributes
open Smoosh.Decoder
open Smoosh.Encoder

type Person =
  {
    [<Utf8>]
    Name     : string
    BirthDay : DateTime
    [<Utf8>]
    Phone    : string
    [<Smoosh>]
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

eAce |> BitConverter.ToString |> printfn "%s"
eAce |> personDec |> fun dAce -> printfn $"%i{eAce.Length}: %b{dAce = ace}"
```

You should expect to see:

```text
C6-88-2C-6C-A4-0A-8D-0C-A4-0A-0D-8C-2C-6C-BE-1E-A5-C3-51-FA-36-A2-38-D0-AC-D4-D4-D4-B4-D4-D4-D4-B4-D4-D4-D4-D6-08-4D-4C-CC-CC-B8-90-75-A0-00
47: true
```

Without the attributes, you would expect to see:

```text
CD-08-20-0C-60-0C-A0-04-00-0A-80-0D-00-0C-A0-04-00-0A-00-0D-80-0C-20-0C-60-0C-A0-11-16-77-8A-B1-FA-36-A2-39-A0-AC-00-D4-00-D4-00-D4-00-B4-00-D4-00-D4-00-D4-00-B4-00-D4-00-D4-00-D4-00-D4-00-10-00-00-01-35-33-33-32-E2-41-D6-80
75: true
```

## `mkMTUIgnoringEncoder`


```fsharp
open Smoosh.Latency.Decoder
open Smoosh.Latency.Encoder

type Tree =
  | Leaf
  | Node of byte * Tree * Tree

let rec mkTree d =
  if d = 0
  then Leaf
  else Node (0xF0uy, mkTree (d-1), mkTree (d-1))

let tree = mkTree 12

let treeEnc = mkMTUIgnoringEncoder<Tree> 5_200
let treeDec = mkDecoder<Tree> ()

let eTree = tree |> treeEnc |> Array.ofSeq

eTree |> BitConverter.ToString |> printfn "%s"
eTree |> treeDec |> fun dTree -> printfn $"%i{eTree.Length}: %b{dTree = tree}"
```

Using the normal `Smoosh.Latency.mkEncoder` would result in a runtime exception for such a large tree, instead we get:

```text
F8-7C-3E-1F-0F-87-C3-E1-F0-...-F8-7C-3E-07-C0-F8-7C-0F-80
5119: true

```