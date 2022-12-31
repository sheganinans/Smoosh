``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|   Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |------------:|----------:|----------:|------:|--------:|
| Bench000 |    311.5 ns |  13.26 ns |  38.27 ns |  1.00 |    0.00 |
| Bench175 |  6,960.9 ns | 141.14 ns | 413.93 ns | 22.68 |    3.17 |
| Bench350 | 12,965.0 ns | 257.98 ns | 752.53 ns | 42.24 |    5.41 |
