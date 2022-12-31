``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|   Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |-----------:|----------:|----------:|------:|--------:|
| Bench000 |   326.3 ns |   6.54 ns |  11.62 ns |  1.00 |    0.00 |
| Bench087 | 4,580.2 ns |  90.22 ns | 143.09 ns | 14.06 |    0.73 |
| Bench175 | 8,897.3 ns | 174.53 ns | 266.52 ns | 27.38 |    1.22 |
