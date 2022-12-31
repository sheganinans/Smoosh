``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|   Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |------------:|----------:|----------:|------:|--------:|
| Bench000 |    399.7 ns |  17.86 ns |  51.82 ns |  1.00 |    0.00 |
| Bench175 |  8,834.0 ns | 174.87 ns | 454.51 ns | 22.22 |    2.91 |
| Bench350 | 16,306.8 ns | 323.98 ns | 454.18 ns | 40.76 |    5.63 |
