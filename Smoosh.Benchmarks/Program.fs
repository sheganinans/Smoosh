open System
open System.Reflection
open BenchmarkDotNet.Running

[<EntryPoint>]
let main args =
  let assembly = Assembly.GetExecutingAssembly()
  let switcher = BenchmarkSwitcher(assembly)
  switcher.Run args |> ignore
  0