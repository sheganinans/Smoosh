``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|      Method |         Mean |       Error |      StdDev |       Median |    Ratio | RatioSD |
|------------ |-------------:|------------:|------------:|-------------:|---------:|--------:|
|   LeafBench |     273.5 ns |     5.39 ns |     9.01 ns |     276.6 ns |     1.00 |    0.00 |
| Tree01Bench |     629.5 ns |    12.60 ns |    14.00 ns |     633.2 ns |     2.32 |    0.09 |
| Tree05Bench |  11,132.1 ns |   269.35 ns |   789.94 ns |  10,800.3 ns |    40.93 |    3.26 |
| Tree10Bench | 340,358.3 ns | 6,529.61 ns | 8,490.34 ns | 337,617.1 ns | 1,250.11 |   55.08 |
