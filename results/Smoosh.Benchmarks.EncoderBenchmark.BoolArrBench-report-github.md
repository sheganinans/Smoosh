``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|     Method |        Mean |     Error |    StdDev |  Ratio | RatioSD |
|----------- |------------:|----------:|----------:|-------:|--------:|
| Bench00000 |    328.7 ns |   6.54 ns |  12.91 ns |   1.00 |    0.00 |
| Bench05500 | 28,027.8 ns | 558.05 ns | 548.08 ns |  85.56 |    4.91 |
| Bench11000 | 54,951.5 ns | 675.98 ns | 632.31 ns | 168.15 |    8.41 |
