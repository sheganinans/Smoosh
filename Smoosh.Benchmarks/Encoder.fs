module Smoosh.Tests.Encoder

open Microsoft.FSharp.Core
open System

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Exporters

open Smoosh.Encoder

type MyConfig () as this =
  inherit ManualConfig ()
  do this.AddExporter [| RPlotExporter.Default; MarkdownExporter.GitHub |] |> ignore

module Seq = let rec cycle xs = seq { yield! xs; yield! cycle xs }

[<Config(typeof<MyConfig>)>]
type BoolArrBench () =
  static member val private Array00000 = [||] : bool [] with get
  static member val private Array05500 = (Seq.cycle [|true;false|] |> Seq.take  5_500) with get
  static member val private Array11000 = (Seq.cycle [|true;false|] |> Seq.take 11_000) with get
  [<Benchmark(Baseline=true)>] member this.Bench00000 () = encode BoolArrBench.Array00000 |> ignore
  [<Benchmark>]                member this.Bench05500 () = encode BoolArrBench.Array05500 |> ignore
  [<Benchmark>]                member this.Bench11000 () = encode BoolArrBench.Array11000 |> ignore


[<Config(typeof<MyConfig>)>]
type IntArrBench () =
  static member val private Array000 = [||] : int [] with get
  static member val private Array175 = [|1..175|] with get
  static member val private Array350 = [|1..350|] with get
  [<Benchmark(Baseline=true)>] member this.Bench000 () = encode IntArrBench.Array000 |> ignore
  [<Benchmark>]                member this.Bench175 () = encode IntArrBench.Array175 |> ignore
  [<Benchmark>]                member this.Bench350 () = encode IntArrBench.Array350 |> ignore


[<Config(typeof<MyConfig>)>]
type IntListBench () =
  static member val private List000 = [] : int list with get
  static member val private List175 = [1..175] with get
  static member val private List350 = [1..350] with get
  
  [<Benchmark(Baseline=true)>] member this.Bench000 () = encode IntListBench.List000 |> ignore
  [<Benchmark>]                member this.Bench175 () = encode IntListBench.List175 |> ignore
  [<Benchmark>]                member this.Bench350 () = encode IntListBench.List350 |> ignore


[<Config(typeof<MyConfig>)>]
type IntSeqBench () =
  static member val private Seq000 = Seq.empty : int seq with get
  static member val private Seq175 = seq {1..175} with get
  static member val private Seq350 = seq {1..350} with get
  
  [<Benchmark(Baseline=true)>] member this.Bench000 () = encode IntSeqBench.Seq000 |> ignore
  [<Benchmark>]                member this.Bench175 () = encode IntSeqBench.Seq175 |> ignore
  [<Benchmark>]                member this.Bench350 () = encode IntSeqBench.Seq350 |> ignore


[<Config(typeof<MyConfig>)>]
type IntSetBench () =
  static member val private Set000 = Set.empty : int Set    with get
  static member val private Set175 = [1..175] |> Set.ofList with get
  static member val private Set350 = [1..350] |> Set.ofList with get
  [<Benchmark(Baseline=true)>] member this.Bench000 () = encode IntSetBench.Set000 |> ignore
  [<Benchmark>]                member this.Bench175 () = encode IntSetBench.Set175 |> ignore
  [<Benchmark>]                member this.Bench350 () = encode IntSetBench.Set350 |> ignore


[<Config(typeof<MyConfig>)>]
type FloatArrBench () =
  static member val private Array000 = [||] : float [] with get
  static member val private Array087 = [|1. ..87|]  with get
  static member val private Array175 = [|1. ..175|] with get
  [<Benchmark(Baseline=true)>] member this.Bench000 () = encode FloatArrBench.Array000 |> ignore
  [<Benchmark>]                member this.Bench087 () = encode FloatArrBench.Array087 |> ignore
  [<Benchmark>]                member this.Bench175 () = encode FloatArrBench.Array175 |> ignore


[<Config(typeof<MyConfig>)>]
type FloatListBench () =
  static member val private List000 = [] : int list  with get
  static member val private List087 = [1. ..87]  with get
  static member val private List175 = [1. ..175] with get
  [<Benchmark(Baseline=true)>] member this.Bench000 () = encode FloatListBench.List000 |> ignore
  [<Benchmark>]                member this.Bench087 () = encode FloatListBench.List087 |> ignore
  [<Benchmark>]                member this.Bench175 () = encode FloatListBench.List175 |> ignore


[<Config(typeof<MyConfig>)>]
type FloatSeqBench () =
  static member val private Seq000 = Seq.empty : float seq with get
  static member val private Seq087 = seq {1. ..87} with get
  static member val private Seq175 = seq {1. ..175} with get
  
  [<Benchmark(Baseline=true)>] member this.Bench000 () = encode FloatSeqBench.Seq000 |> ignore
  [<Benchmark>]                member this.Bench087 () = encode FloatSeqBench.Seq087 |> ignore
  [<Benchmark>]                member this.Bench175 () = encode FloatSeqBench.Seq175 |> ignore


[<Config(typeof<MyConfig>)>]
type FloatSetBench () =
  static member val private Set000 = Set.empty : float Set   with get
  static member val private Set087 = [1. .. 87] |> Set.ofList with get
  static member val private Set175 = [1. ..175] |> Set.ofList with get
  [<Benchmark(Baseline=true)>] member this.Bench000 () = encode FloatSetBench.Set000 |> ignore
  [<Benchmark>]                member this.Bench087 () = encode FloatSetBench.Set087 |> ignore
  [<Benchmark>]                member this.Bench175 () = encode FloatSetBench.Set175 |> ignore


type Person =
  {
    Name     : string
    BirthDay : DateTime
    Phone    : string
    Siblings : int
    Spouse   : bool
    Money    : float
  }

[<Config(typeof<MyConfig>)>]
type PersonBench () =
  static member val Birth = DateTime.Now - TimeSpan.FromDays (365. * 10.)
  static member val LongName : string = String.init 700 (fun _ -> "A")
  static member val  MedName : string = String.init 350 (fun _ -> "A")
  static member val MedPhone : string = String.init 350 (fun _ -> "5")
  [<Benchmark(Baseline=true)>]
  member this.Person1 () =
    encode {
      Name     = ""
      BirthDay = PersonBench.Birth
      Phone    = ""
      Siblings = 0
      Spouse   = false
      Money    = 0.
    } |> ignore

  [<Benchmark>]
  member this.Person2 () =
    encode {
      Name     = PersonBench.MedName
      BirthDay = PersonBench.Birth
      Phone    = PersonBench.MedPhone
      Siblings = 50_000_555
      Spouse   = true
      Money    = -10_000_000.
    } |> ignore

  [<Benchmark>]
  member this.Person3 () =
    encode {
      Name     = PersonBench.LongName
      BirthDay = PersonBench.Birth
      Phone    = "555-555-5555"
      Siblings = 50_000_555
      Spouse   = true
      Money    = -10_000_000.
    } |> ignore
    
type WideRecord1 = { A : byte }
type WideRecord2 = { A : byte; B : byte; C : byte; D : byte; E : byte }
type WideRecord3 = { A : byte; B : byte; C : byte; D : byte; E : byte; F : byte; G : byte; H : byte; I : byte; J : byte }

[<Config(typeof<MyConfig>)>]
type WideRecordBench () =
  [<Benchmark(Baseline=true)>] member this.Record1 () = encode { A = 0uy } |> ignore
  [<Benchmark>]                member this.Record2 () = encode { A = 0uy; B = 0uy; C = 0uy; D = 0uy; E = 0uy } |> ignore
  [<Benchmark>]                member this.Record3 () = encode { A = 0uy; B = 0uy; C = 0uy; D = 0uy; E = 0uy; F = 0uy; G = 0uy; H = 0uy; I = 0uy; J = 0uy } |> ignore
    
type Nested =
  { UnWrap : Nested option }
  member this.Wrap = { UnWrap = Some this }
      
[<Config(typeof<MyConfig>)>]
type NestedBench () =
  static member val private NestedEx1 = { UnWrap = None }
  static member val private NestedEx2 = { UnWrap = None }.Wrap.Wrap.Wrap.Wrap.Wrap
  static member val private NestedEx3 = { UnWrap = None }.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap
  
  [<Benchmark(Baseline=true)>] member this.Nested1 () = encode NestedBench.NestedEx1 |> ignore
  [<Benchmark>] member this.Nested2 () = encode NestedBench.NestedEx2 |> ignore
  [<Benchmark>] member this.Nested3 () = encode NestedBench.NestedEx3 |> ignore
