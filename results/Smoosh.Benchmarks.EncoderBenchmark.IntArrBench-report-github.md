``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|   Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |------------:|----------:|----------:|------:|--------:|
| Bench000 |    323.5 ns |   6.44 ns |  11.61 ns |  1.00 |    0.00 |
| Bench175 |  5,993.4 ns | 118.63 ns | 255.37 ns | 18.61 |    0.88 |
| Bench350 | 11,006.6 ns | 205.16 ns | 325.40 ns | 33.99 |    1.63 |
