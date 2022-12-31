``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|      Method |           Mean |        Error |       StdDev |    Ratio | RatioSD |
|------------ |---------------:|-------------:|-------------:|---------:|--------:|
|   LeafBench |       652.5 ns |     13.02 ns |     14.99 ns |     1.00 |    0.00 |
| Tree01Bench |     1,700.7 ns |     15.91 ns |     14.11 ns |     2.62 |    0.07 |
| Tree05Bench |    33,872.2 ns |    644.16 ns |    571.03 ns |    52.19 |    1.33 |
| Tree10Bench | 1,111,835.8 ns | 21,284.58 ns | 23,657.75 ns | 1,706.59 |   51.90 |
