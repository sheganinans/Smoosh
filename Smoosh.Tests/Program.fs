module Smoosh.Tests

open System

open System.Numerics
open Expecto

open Smoosh.Attributes
open Smoosh.Encoder
open Smoosh.Decoder

let encTest (x : 'A) =
  try
    let enc = mkEncoder<'A> ()
    let dec = mkDecoder<'A> ()
    let payload = enc x
    let decoded = dec (payload |> Array.ofSeq)
    match decoded with
    | Error _ -> false
    | Ok decoded -> LanguagePrimitives.GenericEqualityER x decoded
  with _ -> true

type Tree =
  | Leaf
  | Node of byte * Tree * Tree


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
    Money    : decimal
  }

type SmooshNums =
  {
     [<Smoosh>] A : uint16
     [<Smoosh>] B : int16
     [<Smoosh>] C : uint32
     [<Smoosh>] D : int32
     [<Smoosh>] E : uint64
     [<Smoosh>] F : int64
   }

let properties =
  testList "Encoder Tests" [
    testProperty "unit encode" <| fun () -> encTest ()
    testProperty "unit list encode" <| fun (xs : unit list) -> encTest xs
    testProperty "unit array encode" <| fun (xs : unit []) -> encTest xs
    testProperty "unit set encode" <| fun (xs : unit Set) -> encTest xs

    testProperty "bool encode" <| fun (b : bool) -> encTest b
    testProperty "bool list encode" <| fun (xs : bool list) -> encTest xs
    testProperty "bool array encode" <| fun (xs : bool []) -> encTest xs
    testProperty "bool set encode" <| fun (xs : bool Set) -> encTest xs

    testProperty "uint8 encode" <| fun (x : uint8) -> encTest x
    testProperty "uint8 list encode" <| fun (x : uint8 list) -> encTest x
    testProperty "uint8 array encode" <| fun (x : uint8 []) -> encTest x
    testProperty "uint8 set encode" <| fun (x : uint8 Set) -> encTest x

    testProperty "int8 encode" <| fun (x : int8) -> encTest x
    testProperty "int8 list encode" <| fun (x : int8 list) -> encTest x
    testProperty "int8 array encode" <| fun (x : int8 []) -> encTest x
    testProperty "int8 set encode" <| fun (x : int8 Set) -> encTest x

    testProperty "int16 encode" <| fun (x : int16) -> encTest x
    testProperty "int16 list encode" <| fun (x : int16 list) -> encTest x
    testProperty "int16 array encode" <| fun (x : int16 []) -> encTest x
    testProperty "int16 set encode" <| fun (x : int16 Set) -> encTest x

    testProperty "int32 encode" <| fun (x : int32) -> encTest x
    testProperty "int32 list encode" <| fun (x : int32 list) -> encTest x
    testProperty "int32 array encode" <| fun (x : int32 []) -> encTest x
    testProperty "int32 set encode" <| fun (x : int32 Set) -> encTest x

    testProperty "int64 encode" <| fun (x : int64) -> encTest x
    testProperty "int64 list encode" <| fun (x : int64 list) -> encTest x
    testProperty "int64 array encode" <| fun (x : int64 []) -> encTest x
    testProperty "int64 set encode" <| fun (x : int64 Set) -> encTest x

    testProperty "uint64 encode" <| fun (x : uint64) -> encTest x
    testProperty "uint64 list encode" <| fun (x : uint64 list) -> encTest x
    testProperty "uint64 array encode" <| fun (x : uint64 []) -> encTest x
    testProperty "uint64 set encode" <| fun (x : uint64 Set) -> encTest x

    testProperty "float32 encode" <| fun (x : float32) -> encTest x
    testProperty "float32 list encode" <| fun (x : float32 list) -> encTest x
    testProperty "float32 array encode" <| fun (x : float32 []) -> encTest x
    // https://github.com/dotnet/fsharp/issues/14507
    testProperty "float32 set encode" <| fun (x : float32 Set) -> encTest x

    testProperty "float encode" <| fun (x : float) -> encTest x
    testProperty "float list encode" <| fun (x : float list) -> encTest x
    testProperty "float array encode" <| fun (x : float []) -> encTest x
    // https://github.com/dotnet/fsharp/issues/14507
    testProperty "float set encode" <| fun (x : float Set) -> encTest x

    testProperty "decimal encode" <| fun (x : decimal) -> encTest x
    testProperty "decimal list encode" <| fun (x : decimal list) -> encTest x
    testProperty "decimal array encode" <| fun (x : decimal []) -> encTest x
    testProperty "decimal set encode" <| fun (x : decimal Set) -> encTest x

    testProperty "BigInt encode" <| fun (x : BigInteger) -> encTest x
    testProperty "BigInt list encode" <| fun (x : BigInteger list) -> encTest x
    testProperty "BigInt array encode" <| fun (x : BigInteger []) -> encTest x
    testProperty "BigInt set encode" <| fun (x : BigInteger Set) -> encTest x

    testProperty "string encode" <| fun (x : string) -> encTest x
    testProperty "string list encode" <| fun (x : string list) -> encTest x
    testProperty "string array encode" <| fun (x : string []) -> encTest x
    testProperty "string set encode" <| fun (x : string Set) -> encTest x

    testProperty "guid encode" <| fun (x : Guid) -> encTest x
    testProperty "guid list encode" <| fun (x : Guid list) -> encTest x
    testProperty "guid array encode" <| fun (x : Guid []) -> encTest x
    testProperty "guid set encode" <| fun (x : Guid Set) -> encTest x

    testProperty "DateTime encode" <| fun (x : DateTime) -> encTest x
    testProperty "DateTime list encode" <| fun (x : DateTime list) -> encTest x
    testProperty "DateTime array encode" <| fun (x : DateTime []) -> encTest x
    testProperty "DateTime set encode" <| fun (x : Guid Set) -> encTest x

    testProperty "TimeSpan encode" <| fun (x : TimeSpan) -> encTest x
    testProperty "TimeSpan list encode" <| fun (x : TimeSpan list) -> encTest x
    testProperty "TimeSpan array encode" <| fun (x : TimeSpan []) -> encTest x
    testProperty "TimeSpan set encode" <| fun (x : TimeSpan Set) -> encTest x

    testProperty "DateTimeOffset encode" <| fun (x : DateTimeOffset) -> encTest x
    testProperty "DateTimeOffset list encode" <| fun (x : DateTimeOffset list) -> encTest x
    testProperty "DateTimeOffset array encode" <| fun (x : DateTimeOffset []) -> encTest x
    testProperty "DateTimeOffset set encode" <| fun (x : DateTimeOffset Set) -> encTest x

    testProperty "guid option" <| fun (x : Guid option) -> encTest x
    testProperty "tuple 1" <| fun (x : string * int) -> encTest x
    testProperty "tuple 2" <| fun (x : Guid * DateTime []) -> encTest x

    testProperty "string opt arr" <| fun (x : string option []) -> encTest x
    testProperty "Nullable int" <| fun (x : int Nullable) -> encTest x
    testProperty "Tree" <| fun (x : Tree) -> encTest x
    testProperty "Person" <| fun (x : Person) -> encTest x
    testProperty "SmooshNums" <| fun (x : SmooshNums) -> encTest x

    testProperty "Hash failure check" <| fun (x : Person) ->
      try
        let enc = mkEncoder<Person> ()
        let dec = mkDecoder<Tree> ()
        let payload = enc x
        let decoded = dec (payload |> Array.ofSeq)
        match decoded with
        | Ok _ -> false
        | Error _ -> true
      with _ -> true

  ]

Tests.runTests defaultConfig properties |> ignore