``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|      Method |        Mean |       Error |      StdDev |  Ratio | RatioSD |
|------------ |------------:|------------:|------------:|-------:|--------:|
|   LeafBench |    269.4 ns |     5.40 ns |    10.01 ns |   1.00 |    0.00 |
| Tree01Bench |    364.7 ns |     6.88 ns |     6.76 ns |   1.36 |    0.06 |
| Tree05Bench |  2,709.5 ns |    43.04 ns |    38.16 ns |  10.08 |    0.37 |
| Tree10Bench | 82,310.7 ns | 1,421.88 ns | 2,039.22 ns | 304.50 |   15.52 |
