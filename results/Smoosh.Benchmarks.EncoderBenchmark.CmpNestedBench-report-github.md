``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|    Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|---------- |------------:|----------:|----------:|------:|--------:|
| Nested01L |    289.4 ns |   6.02 ns |  17.28 ns |  1.00 |    0.00 |
| Nested05L |    868.2 ns |  16.87 ns |  20.08 ns |  2.91 |    0.14 |
| Nested10L |  1,530.3 ns |  30.52 ns |  51.82 ns |  5.29 |    0.41 |
|  Nested1S |  1,990.3 ns |  30.30 ns |  25.30 ns |  6.68 |    0.28 |
|  Nested2S |  7,777.2 ns | 155.31 ns | 207.34 ns | 26.18 |    1.32 |
|  Nested3S | 13,944.9 ns | 267.33 ns | 307.86 ns | 46.83 |    2.07 |
