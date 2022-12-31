# Smoosh: Every last bit out of your messaging protocol!

## Bit oriented

If your type fits in a bit, it's encoded as a bit. Some types are even encoded as *zero* bits!


## Blazing Fast

* [Read the Benchmarks](./BENCHMARKS.md)
* Powered with [TypeShape](https://github.com/eiriktsarpalis/TypeShape) and Bit Arithmetic™
* Focus on IP fragmentation reduction, for more information see: [IMPLEMENTATION_NOTES.md](./IMPLEMENTATION_NOTES.md)


## Basic Usage

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

let tree = mkTree 10

let treeEnc = mkEncoder<Tree> () 
let treeDec = mkDecoder<Tree> ()

let eTree = tree |> treeEnc

eTree |> BitConverter.ToString |> printfn "%s"
eTree |> treeDec |> fun dTree -> printfn $"%i{eTree.Length}: %b{dTree = tree}"
```

You should expect to see:

```text
F8-7C-3E-1F-0F-87-C3-E1-...-F8-7C-3E-07-C0-F8-7C-0F-80
1279: true
```

[Read more samples here!](./SAMPLES.md)


## Read the Specs!

[General Encoder](./SPEC.md)

[Latency Sensitive Encoder](./SPEC.LATENCY.md)


## TODO

[TODO.md](./TODO.md)


## Special Thanks

This project was inspired by: [Flat](https://hackage.haskell.org/package/flat)