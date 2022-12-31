``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|   Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------- |---------:|---------:|---------:|------:|--------:|
| Nested01 | 319.6 ns |  6.35 ns | 10.43 ns |  1.00 |    0.00 |
| Nested05 | 599.5 ns | 12.02 ns | 26.14 ns |  1.87 |    0.09 |
| Nested10 | 854.4 ns | 16.36 ns | 19.48 ns |  2.68 |    0.11 |
