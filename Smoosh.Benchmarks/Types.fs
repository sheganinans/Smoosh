module Smoosh.Benchmarks.Types

open System


open Smoosh.Benchmarks.EncoderBenchmark

module Poco =
    [<CLIMutable>]
    type T = { A : int; B : string; C : bool; D : byte []; F : DateTimeOffset; G : TimeSpan; H : Guid }

    let value = { A = 42; B = "lorem ipsum"; C = true; D = [|1uy .. 20uy|]
                  F = DateTimeOffset(2018,1,1,23,11,12,TimeSpan.Zero)
                  G = TimeSpan.FromDays 30.
                  H = Guid.Empty }

    //type PocoRoundtrip() = inherit RoundtripBenchmark<T>(value)

module FloatArray =
    type T = float[]

    let value =
        [| for i in 1 .. 100 -> float i / float 100.
           yield Double.Epsilon
           yield Double.PositiveInfinity
           yield Double.NaN
           yield Double.NegativeInfinity |]

    //type FloatArrayRoundtrip() = inherit RoundtripBenchmark<T>(value)

module Array3D =
    type T = float [,,]

    let value : T = Array3D.init 20 20 20 (fun i j k -> 0.1 * float i + float j + 10. * float k)

    //type Array3DRoundtrip() = inherit RoundtripBenchmark<T>(value)

module LargeTuple =
    type T = string * int * int * int * bool * string * (float * int list) option * int * int * int * string
    let value : T = ("lorem ipsum dolor", 1, 2, 3, true, "", Some(3.14, [2]), 3, 2, 1, "lorem")

    //type LargeTupleRoundtrip() = inherit RoundtripBenchmark<T>(value)

module FSharpList =
    type T = int list
    let value : T = [1..1000]

    //type FSharpListRoundtrip() = inherit RoundtripBenchmark<T>(value)

module Dictionary =
    open System.Collections.Generic
    open Poco

    type T = Dictionary<string, Poco.T>

    let mkDict size =
        let d = T ()
        for i = 1 to size do
            let key = $"key-%d{i}"
            let value = {
                A = i ;
                B = $"value-%d{i}" ;
                C = i % 2 = 0 ;
                D = [|byte i|]
                F = DateTimeOffset(2018,1,1,23,11,12,TimeSpan.Zero) ;
                G = TimeSpan.FromDays 30.
                H = Guid.Empty
            }

            d.Add (key, value)
        d

    let value = mkDict 20

    //type DictionaryRoundtrip () = inherit RoundtripBenchmark<T>(value)


module FSharpBinTree =
    type Tree = Node | Leaf of int * Tree * Tree

    let rec mkTree d = if d = 0 then Node else Leaf (d, mkTree(d-1), mkTree(d-1))

    let value = mkTree 10

    //type FSharpBinaryRoundtrip () =  inherit RoundtripBenchmark<Tree>(value)

module FSharpSet =
    let value = set [1 .. 40]

    //type FSharpSetRoundtrip () = inherit RoundtripBenchmark<Set<int>>(value)


module FSharpMap =
    let value = [1 .. 20] |> Seq.map (fun i -> string i, i) |> Map.ofSeq

    //type FSharpMapRoundtrip () = inherit RoundtripBenchmark<Map<string, int>>(value)
