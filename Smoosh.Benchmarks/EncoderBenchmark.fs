module Smoosh.Benchmarks.EncoderBenchmark

open Microsoft.FSharp.Core
open System

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Exporters

open Smoosh.Latency
open Smoosh.Attributes


type MyConfig () as this =
  inherit ManualConfig ()
  do this.AddExporter [| RPlotExporter.Default; MarkdownExporter.GitHub |] |> ignore

module Seq = let rec cycle xs = seq { yield! xs; yield! cycle xs }


[<Config(typeof<MyConfig>)>]
type BoolArrBench () =
  static member val private Array00000 = [||] : bool [] with get
  static member val private Array05500 = (Seq.cycle [|true;false|] |> Seq.take  5_500 |> Array.ofSeq) with get
  static member val private Array11000 = (Seq.cycle [|true;false|] |> Seq.take 11_000 |> Array.ofSeq) with get
  static member val private Enc = Encoder.mkEncoder<bool []> ()
  [<Benchmark(Baseline=true)>] member this.Bench00000 () = BoolArrBench.Enc BoolArrBench.Array00000 |> ignore
  [<Benchmark>]                member this.Bench05500 () = BoolArrBench.Enc BoolArrBench.Array05500 |> ignore
  [<Benchmark>]                member this.Bench11000 () = BoolArrBench.Enc BoolArrBench.Array11000 |> ignore


[<Config(typeof<MyConfig>)>]
type IntArrBench () =
  static member val private Array000 = [||] : int [] with get
  static member val private Array175 = [|1..175|] with get
  static member val private Array350 = [|1..350|] with get
  static member val private Enc = Encoder.mkEncoder<int []> ()
  [<Benchmark(Baseline=true)>] member this.Bench000 () = IntArrBench.Enc IntArrBench.Array000 |> ignore
  [<Benchmark>]                member this.Bench175 () = IntArrBench.Enc IntArrBench.Array175 |> ignore
  [<Benchmark>]                member this.Bench350 () = IntArrBench.Enc IntArrBench.Array350 |> ignore


[<Config(typeof<MyConfig>)>]
type IntListBench () =
  static member val private List000 = [] : int list with get
  static member val private List175 = [1..175] with get
  static member val private List350 = [1..350] with get
  static member val private Enc = Encoder.mkEncoder<int list> ()
  [<Benchmark(Baseline=true)>] member this.Bench000 () = IntListBench.Enc IntListBench.List000 |> ignore
  [<Benchmark>]                member this.Bench175 () = IntListBench.Enc IntListBench.List175 |> ignore
  [<Benchmark>]                member this.Bench350 () = IntListBench.Enc IntListBench.List350 |> ignore


[<Config(typeof<MyConfig>)>]
type IntSetBench () =
  static member val private Set000 = Set.empty : int Set    with get
  static member val private Set175 = [1..175] |> Set.ofList with get
  static member val private Set350 = [1..350] |> Set.ofList with get
  static member val private Enc = Encoder.mkEncoder<int Set> ()
  [<Benchmark(Baseline=true)>] member this.Bench000 () = IntSetBench.Enc IntSetBench.Set000 |> ignore
  [<Benchmark>]                member this.Bench175 () = IntSetBench.Enc IntSetBench.Set175 |> ignore
  [<Benchmark>]                member this.Bench350 () = IntSetBench.Enc IntSetBench.Set350 |> ignore


[<Config(typeof<MyConfig>)>]
type FloatArrBench () =
  static member val private Array000 = [||] : float [] with get
  static member val private Array087 = [|1. ..87|]  with get
  static member val private Array175 = [|1. ..175|] with get
  static member val private Enc = Encoder.mkEncoder<float []> ()
  [<Benchmark(Baseline=true)>] member this.Bench000 () = FloatArrBench.Enc FloatArrBench.Array000 |> ignore
  [<Benchmark>]                member this.Bench087 () = FloatArrBench.Enc FloatArrBench.Array087 |> ignore
  [<Benchmark>]                member this.Bench175 () = FloatArrBench.Enc FloatArrBench.Array175 |> ignore


[<Config(typeof<MyConfig>)>]
type FloatListBench () =
  static member val private List000 = [] : float list  with get
  static member val private List087 = [1. ..87]  with get
  static member val private List175 = [1. ..175] with get
  static member val private Enc = Encoder.mkEncoder<float list> ()
  [<Benchmark(Baseline=true)>] member this.Bench000 () = FloatListBench.Enc FloatListBench.List000 |> ignore
  [<Benchmark>]                member this.Bench087 () = FloatListBench.Enc FloatListBench.List087 |> ignore
  [<Benchmark>]                member this.Bench175 () = FloatListBench.Enc FloatListBench.List175 |> ignore


[<Config(typeof<MyConfig>)>]
type FloatSetBench () =
  static member val private Set000 = Set.empty : float Set   with get
  static member val private Set087 = [1. .. 87] |> Set.ofList with get
  static member val private Set175 = [1. ..175] |> Set.ofList with get
  static member val private Enc = Encoder.mkEncoder<float Set> ()
  [<Benchmark(Baseline=true)>] member this.Bench000 () = FloatSetBench.Enc FloatSetBench.Set000 |> ignore
  [<Benchmark>]                member this.Bench087 () = FloatSetBench.Enc FloatSetBench.Set087 |> ignore
  [<Benchmark>]                member this.Bench175 () = FloatSetBench.Enc FloatSetBench.Set175 |> ignore


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


[<Config(typeof<MyConfig>)>]
type SmallPersonBench () =
  static member val Birth = DateTime.Now - TimeSpan.FromDays (365. * 10.)
  static member val private Enc = Encoder.mkEncoder<Person> ()
  
  [<Benchmark(Baseline=true)>]
  member this.Person1 () =
    SmallPersonBench.Enc {
      Name     = ""
      BirthDay = SmallPersonBench.Birth
      Phone    = ""
      Siblings = 0
      Spouse   = false
      Money    = 0.
    } |> ignore

  member this.Person2 () =
    SmallPersonBench.Enc {
      Name     = "Ace The Place"
      BirthDay = SmallPersonBench.Birth
      Phone    = "+555-555-5555"
      Siblings = 2
      Spouse   = false
      Money    = 55_555.55
    } |> ignore


[<Config(typeof<MyConfig>)>]
type LargePersonBench () =
  static member val Birth = DateTime.Now - TimeSpan.FromDays (365. * 10.)
  static member val   MedName : string = String.init 350 (fun _ -> "A")
  static member val  LongName : string = String.init 700 (fun _ -> "A")
  static member val  MedPhone : string = String.init 350 (fun _ -> "5")
  static member val LongPhone : string = String.init 700 (fun _ -> "5")
  static member val private Enc = Encoder.mkEncoder<Person> ()

  [<Benchmark>]
  member this.Person1 () =
    LargePersonBench.Enc {
      Name     = LargePersonBench.MedName
      BirthDay = LargePersonBench.Birth
      Phone    = LargePersonBench.MedPhone
      Siblings = 50_000_555
      Spouse   = true
      Money    = -10_000_000.
    } |> ignore

  [<Benchmark>]
  member this.Person2 () =
    LargePersonBench.Enc {
      Name     = LargePersonBench.LongName
      BirthDay = LargePersonBench.Birth
      Phone    = "555-555-5555"
      Siblings = 50_000_555
      Spouse   = true
      Money    = -10_000_000.
    } |> ignore

  [<Benchmark>]
  member this.Person3 () =
    LargePersonBench.Enc {
      Name     = "Med Name"
      BirthDay = LargePersonBench.Birth
      Phone    = LargePersonBench.LongName
      Siblings = 50_000_555
      Spouse   = true
      Money    = -10_000_000.
    } |> ignore
    
    
type WideRecord1 = { A : byte }
type WideRecord2 = { A : byte; B : byte; C : byte; D : byte; E : byte }
type WideRecord3 = { A : byte; B : byte; C : byte; D : byte; E : byte; F : byte; G : byte; H : byte; I : byte; J : byte }

[<Config(typeof<MyConfig>)>]
type WideRecordBench () =
  static member val private Enc1 = Encoder.mkEncoder<WideRecord1> ()
  static member val private Enc2 = Encoder.mkEncoder<WideRecord2> ()
  static member val private Enc3 = Encoder.mkEncoder<WideRecord3> ()
  [<Benchmark(Baseline=true)>] member this.Record1 () = WideRecordBench.Enc1 { A = 0uy } |> ignore
  [<Benchmark>]                member this.Record2 () = WideRecordBench.Enc2 { A = 0uy; B = 0uy; C = 0uy; D = 0uy; E = 0uy } |> ignore
  [<Benchmark>]                member this.Record3 () = WideRecordBench.Enc3 { A = 0uy; B = 0uy; C = 0uy; D = 0uy; E = 0uy; F = 0uy; G = 0uy; H = 0uy; I = 0uy; J = 0uy } |> ignore
    
    
type Nested =
  { UnWrap : Nested option }
  member this.Wrap = { UnWrap = Some this }
      
[<Config(typeof<MyConfig>)>]
type NestedBench () =
  static member val private NestedEx01 = { UnWrap = None }
  static member val private NestedEx05 = { UnWrap = None }.Wrap.Wrap.Wrap.Wrap.Wrap
  static member val private NestedEx10 = { UnWrap = None }.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap
  static member val private Enc = Encoder.mkEncoder<Nested> ()
  
  [<Benchmark(Baseline=true)>] member this.Nested01 () = NestedBench.Enc NestedBench.NestedEx01 |> ignore
  [<Benchmark>]                member this.Nested05 () = NestedBench.Enc NestedBench.NestedEx05 |> ignore
  [<Benchmark>]                member this.Nested10 () = NestedBench.Enc NestedBench.NestedEx10 |> ignore


type Tree =
  | Leaf
  | Node of byte * Tree * Tree

let rec mkTree d =
  if d = 0
  then Leaf
  else Node (0x0fuy, mkTree (d-1), mkTree (d-1))

[<Config(typeof<MyConfig>)>]
type TreeBench () =
  static member val private LeafV  = Leaf with get
  static member val private Tree1 = mkTree 1 with get
  static member val private Tree5 = mkTree 5 with get
  static member val private Tree10 = mkTree 10 with get
  static member val private Enc = Encoder.mkEncoder<Tree> ()

  [<Benchmark(Baseline=true)>] member this.LeafBench () = TreeBench.Enc TreeBench.LeafV |> ignore
  [<Benchmark>]                member this.Tree01Bench () = TreeBench.Enc TreeBench.Tree1 |> ignore
  [<Benchmark>]                member this.Tree05Bench () = TreeBench.Enc TreeBench.Tree5 |> ignore
  [<Benchmark>]                member this.Tree10Bench () = TreeBench.Enc TreeBench.Tree10 |> ignore


[<Config(typeof<MyConfig>)>]
type TreeEncDecBench () =
  static member val private LeafV  = Leaf with get
  static member val private Tree1 = mkTree 1 with get
  static member val private Tree5 = mkTree 5 with get
  static member val private Tree10 = mkTree 10 with get
  static member val private Enc = Encoder.mkEncoder<Tree> ()
  static member val private Dec = Decoder.mkDecoder<Tree> ()

  [<Benchmark(Baseline=true)>] member this.LeafBench () = TreeEncDecBench.Enc TreeEncDecBench.LeafV |> TreeEncDecBench.Dec |> ignore
  [<Benchmark>]                member this.Tree01Bench () = TreeEncDecBench.Enc TreeEncDecBench.Tree1 |> TreeEncDecBench.Dec |> ignore
  [<Benchmark>]                member this.Tree05Bench () = TreeEncDecBench.Enc TreeEncDecBench.Tree5 |> TreeEncDecBench.Dec |> ignore
  [<Benchmark>]                member this.Tree10Bench () = TreeEncDecBench.Enc TreeEncDecBench.Tree10 |> TreeEncDecBench.Dec |> ignore


[<Config(typeof<MyConfig>)>]
type CmpBoolArrBench () =
  static member val private Array05500 = (Seq.cycle [|true;false|] |> Seq.take 5_500 |> Array.ofSeq) with get
  static member val private Array11000 = (Seq.cycle [|true;false|] |> Seq.take 11_000 |> Array.ofSeq) with get
  static member val private SEnc = Smoosh.Encoder.mkEncoder<bool []> ()
  static member val private LEnc = Encoder.mkEncoder<bool []> ()
  [<Benchmark(Baseline=true)>] member this.Bench05500L () = CmpBoolArrBench.LEnc CmpBoolArrBench.Array05500 |> ignore
  [<Benchmark>]                member this.Bench11000L () = CmpBoolArrBench.LEnc CmpBoolArrBench.Array11000 |> ignore
  [<Benchmark>]                member this.Bench05500S () = CmpBoolArrBench.SEnc CmpBoolArrBench.Array05500 |> ignore
  [<Benchmark>]                member this.Bench11000S () = CmpBoolArrBench.SEnc CmpBoolArrBench.Array11000 |> ignore


[<Config(typeof<MyConfig>)>]
type CmpIntArrBench () =
  static member val private Array175 = [|1..175|] with get
  static member val private Array350 = [|1..350|] with get
  static member val private SEnc = Smoosh.Encoder.mkEncoder<int []> ()
  static member val private LEnc = Encoder.mkEncoder<int []> ()
  [<Benchmark(Baseline=true)>] member this.Bench175L () = CmpIntArrBench.LEnc CmpIntArrBench.Array175 |> ignore
  [<Benchmark>]                member this.Bench350L () = CmpIntArrBench.LEnc CmpIntArrBench.Array350 |> ignore
  [<Benchmark>]                member this.Bench175S () = CmpIntArrBench.SEnc CmpIntArrBench.Array175 |> ignore
  [<Benchmark>]                member this.Bench350S () = CmpIntArrBench.SEnc CmpIntArrBench.Array350 |> ignore


[<Config(typeof<MyConfig>)>]
type CmpTreeEncDecBench () =
  static member val private Tree05 = mkTree  5 with get
  static member val private Tree10 = mkTree 10 with get
  static member val private SEnc = Smoosh.Encoder.mkEncoder<Tree> ()
  static member val private SDec = Smoosh.Decoder.mkDecoder<Tree> ()
  static member val private LEnc = Encoder.mkEncoder<Tree> ()
  static member val private LDec = Decoder.mkDecoder<Tree> ()

  [<Benchmark(Baseline=true)>] member this.Tree05BenchL () = CmpTreeEncDecBench.LEnc CmpTreeEncDecBench.Tree05 |> CmpTreeEncDecBench.LDec |> ignore
  [<Benchmark>]                member this.Tree10BenchL () = CmpTreeEncDecBench.LEnc CmpTreeEncDecBench.Tree10 |> CmpTreeEncDecBench.SDec |> ignore
  [<Benchmark>]                member this.Tree05BenchS () = CmpTreeEncDecBench.SEnc CmpTreeEncDecBench.Tree05 |> Array.ofSeq |> CmpTreeEncDecBench.SDec |> ignore
  [<Benchmark>]                member this.Tree10BenchS () = CmpTreeEncDecBench.SEnc CmpTreeEncDecBench.Tree10 |> Array.ofSeq |> CmpTreeEncDecBench.SDec |> ignore


[<Config(typeof<MyConfig>)>]
type CmpSmallPersonBench () =
  static member val Birth = DateTime.Now - TimeSpan.FromDays (365. * 10.)
  static member val private SEnc = Smoosh.Encoder.mkEncoder<Person> ()
  static member val private SDec = Smoosh.Decoder.mkDecoder<Person> ()
  static member val private LEnc = Encoder.mkEncoder<Person> ()
  static member val private LDec = Decoder.mkDecoder<Person> ()
  static member val private P0 = {
      Name     = ""
      BirthDay = SmallPersonBench.Birth
      Phone    = ""
      Siblings = 0
      Spouse   = false
      Money    = 0.
    }
  static member val private P1 = {
      Name     = "Ace The Place"
      BirthDay = SmallPersonBench.Birth
      Phone    = "+555-555-5555"
      Siblings = 2
      Spouse   = false
      Money    = 55_555.55
    }

  [<Benchmark(Baseline=true)>] member this.Person0L () = CmpSmallPersonBench.LEnc CmpSmallPersonBench.P0 |> CmpSmallPersonBench.LDec |> ignore
  [<Benchmark>]                member this.Person1L () = CmpSmallPersonBench.LEnc CmpSmallPersonBench.P1 |> CmpSmallPersonBench.LDec |> ignore
  [<Benchmark>]                member this.Person0S () = CmpSmallPersonBench.SEnc CmpSmallPersonBench.P0 |> Array.ofSeq |> CmpSmallPersonBench.SDec |> ignore
  [<Benchmark>]                member this.Person1S () = CmpSmallPersonBench.SEnc CmpSmallPersonBench.P1 |> Array.ofSeq |> CmpSmallPersonBench.SDec |> ignore


[<Config(typeof<MyConfig>)>]
type CmpLargePersonBench () =
  static member val Birth = DateTime.Now - TimeSpan.FromDays (365. * 10.)
  static member val   MedName : string = String.init 350 (fun _ -> "A")
  static member val  LongName : string = String.init 700 (fun _ -> "A")
  static member val  MedPhone : string = String.init 350 (fun _ -> "5")
  static member val LongPhone : string = String.init 700 (fun _ -> "5")
  static member val private SEnc = Smoosh.Encoder.mkEncoder<Person> ()
  static member val private SDec = Smoosh.Decoder.mkDecoder<Person> ()
  static member val private LEnc = Encoder.mkEncoder<Person> ()
  static member val private LDec = Decoder.mkDecoder<Person> ()
  static member val private P0 = {
      Name     = LargePersonBench.MedName
      BirthDay = LargePersonBench.Birth
      Phone    = LargePersonBench.MedPhone
      Siblings = 50_000_555
      Spouse   = true
      Money    = -10_000_000.
    }
  static member val private P1 = {
      Name     = LargePersonBench.LongName
      BirthDay = LargePersonBench.Birth
      Phone    = "555-555-5555"
      Siblings = 50_000_555
      Spouse   = true
      Money    = -10_000_000.
    }
  static member val private P2 = {
      Name     = "Med Name"
      BirthDay = LargePersonBench.Birth
      Phone    = LargePersonBench.LongName
      Siblings = 50_000_555
      Spouse   = true
      Money    = -10_000_000.
    }

  [<Benchmark(Baseline=true)>] member this.P0L () = CmpLargePersonBench.LEnc CmpLargePersonBench.P0 |> CmpLargePersonBench.LDec |> ignore
  [<Benchmark>]                member this.P1L () = CmpLargePersonBench.LEnc CmpLargePersonBench.P1 |> CmpLargePersonBench.LDec |> ignore
  [<Benchmark>]                member this.P2L () = CmpLargePersonBench.LEnc CmpLargePersonBench.P2 |> CmpLargePersonBench.LDec |> ignore
  [<Benchmark>]                member this.P0S () = CmpLargePersonBench.SEnc CmpLargePersonBench.P0 |> Array.ofSeq |> CmpLargePersonBench.SDec |> ignore
  [<Benchmark>]                member this.P1S () = CmpLargePersonBench.SEnc CmpLargePersonBench.P1 |> Array.ofSeq |> CmpLargePersonBench.SDec |> ignore
  [<Benchmark>]                member this.P2S () = CmpLargePersonBench.SEnc CmpLargePersonBench.P2 |> Array.ofSeq |> CmpLargePersonBench.SDec |> ignore


[<Config(typeof<MyConfig>)>]
type CmpWideRecordBench () =
  static member val private SEnc1 = Smoosh.Encoder.mkEncoder<WideRecord1> ()
  static member val private SDec1 = Smoosh.Decoder.mkDecoder<WideRecord1> ()
  static member val private LEnc1 = Encoder.mkEncoder<WideRecord1> ()
  static member val private LDec1 = Decoder.mkDecoder<WideRecord1> ()
  static member val private SEnc2 = Smoosh.Encoder.mkEncoder<WideRecord2> ()
  static member val private SDec2 = Smoosh.Decoder.mkDecoder<WideRecord2> ()
  static member val private LEnc2 = Encoder.mkEncoder<WideRecord2> ()
  static member val private LDec2 = Decoder.mkDecoder<WideRecord2> ()
  static member val private SEnc3 = Smoosh.Encoder.mkEncoder<WideRecord3> ()
  static member val private SDec3 = Smoosh.Decoder.mkDecoder<WideRecord3> ()
  static member val private LEnc3 = Encoder.mkEncoder<WideRecord3> ()
  static member val private LDec3 = Decoder.mkDecoder<WideRecord3> ()
  static member val private R1 = { A = 0uy }
  static member val private R2 = { A = 0uy; B = 0uy; C = 0uy; D = 0uy; E = 0uy }
  static member val private R3 = { A = 0uy; B = 0uy; C = 0uy; D = 0uy; E = 0uy; F = 0uy; G = 0uy; H = 0uy; I = 0uy; J = 0uy }

  [<Benchmark(Baseline=true)>] member this.Record1L () = CmpWideRecordBench.LEnc1 CmpWideRecordBench.R1 |> CmpWideRecordBench.LDec1 |> ignore
  [<Benchmark>]                member this.Record2L () = CmpWideRecordBench.LEnc2 CmpWideRecordBench.R2 |> CmpWideRecordBench.LDec2 |> ignore
  [<Benchmark>]                member this.Record3L () = CmpWideRecordBench.LEnc3 CmpWideRecordBench.R3 |> CmpWideRecordBench.LDec3 |> ignore
  [<Benchmark>]                member this.Record1S () = CmpWideRecordBench.SEnc1 CmpWideRecordBench.R1 |> Array.ofSeq |> CmpWideRecordBench.SDec1 |> ignore
  [<Benchmark>]                member this.Record2S () = CmpWideRecordBench.SEnc2 CmpWideRecordBench.R2 |> Array.ofSeq |> CmpWideRecordBench.SDec2 |> ignore
  [<Benchmark>]                member this.Record3S () = CmpWideRecordBench.SEnc3 CmpWideRecordBench.R3 |> Array.ofSeq |> CmpWideRecordBench.SDec3 |> ignore


[<Config(typeof<MyConfig>)>]
type CmpNestedBench () =
  static member val private NestedEx1 = { UnWrap = None }
  static member val private NestedEx5 = { UnWrap = None }.Wrap.Wrap.Wrap.Wrap.Wrap
  static member val private NestedEx10 = { UnWrap = None }.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap.Wrap
  static member val private SEnc = Smoosh.Encoder.mkEncoder<Nested> ()
  static member val private SDec = Smoosh.Decoder.mkDecoder<Nested> ()
  static member val private LEnc = Encoder.mkEncoder<Nested> ()
  static member val private LDec = Decoder.mkDecoder<Nested> ()

  [<Benchmark(Baseline=true)>] member this.Nested01L () = CmpNestedBench.LEnc CmpNestedBench.NestedEx1 |> CmpNestedBench.LDec |> ignore
  [<Benchmark>]                member this.Nested05L () = CmpNestedBench.LEnc CmpNestedBench.NestedEx5 |> CmpNestedBench.LDec |> ignore
  [<Benchmark>]                member this.Nested10L () = CmpNestedBench.LEnc CmpNestedBench.NestedEx10 |> CmpNestedBench.LDec |> ignore
  [<Benchmark>]                member this.Nested1S () = CmpNestedBench.SEnc CmpNestedBench.NestedEx1 |> Array.ofSeq |> CmpNestedBench.SDec |> ignore
  [<Benchmark>]                member this.Nested2S () = CmpNestedBench.SEnc CmpNestedBench.NestedEx5 |> Array.ofSeq |> CmpNestedBench.SDec |> ignore
  [<Benchmark>]                member this.Nested3S () = CmpNestedBench.SEnc CmpNestedBench.NestedEx10 |> Array.ofSeq |> CmpNestedBench.SDec |> ignore
