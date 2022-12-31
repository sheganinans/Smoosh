``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|   Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |-----------:|----------:|----------:|------:|--------:|
| Bench000 |   282.8 ns |   5.64 ns |  13.07 ns |  1.00 |    0.00 |
| Bench087 | 4,793.5 ns |  94.84 ns | 158.45 ns | 16.79 |    0.82 |
| Bench175 | 9,069.8 ns | 179.89 ns | 300.55 ns | 31.79 |    1.85 |
