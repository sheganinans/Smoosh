``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|   Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |------------:|----------:|----------:|------:|--------:|
| Bench000 |    340.0 ns |   6.75 ns |  10.31 ns |  1.00 |    0.00 |
| Bench087 |  5,965.6 ns | 113.27 ns | 105.95 ns | 17.67 |    0.67 |
| Bench175 | 11,425.4 ns | 194.56 ns | 216.26 ns | 33.80 |    1.25 |
