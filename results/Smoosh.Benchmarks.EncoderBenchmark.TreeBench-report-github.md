``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|      Method |        Mean |       Error |    StdDev |  Ratio | RatioSD |
|------------ |------------:|------------:|----------:|-------:|--------:|
|   LeafBench |    177.8 ns |     3.54 ns |   6.00 ns |   1.00 |    0.00 |
| Tree01Bench |    265.4 ns |     5.24 ns |   8.89 ns |   1.49 |    0.07 |
| Tree05Bench |  2,647.4 ns |    52.79 ns |  54.21 ns |  15.05 |    0.80 |
| Tree10Bench | 75,021.9 ns | 1,084.52 ns | 905.63 ns | 427.07 |   19.57 |
