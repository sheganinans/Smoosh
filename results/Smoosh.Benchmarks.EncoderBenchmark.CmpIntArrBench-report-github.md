``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|    Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|---------- |-----------:|----------:|----------:|------:|--------:|
| Bench175L |   5.584 μs | 0.1110 μs | 0.1321 μs |  1.00 |    0.00 |
| Bench350L |  11.400 μs | 0.2242 μs | 0.4477 μs |  2.02 |    0.08 |
| Bench175S |  68.140 μs | 1.3447 μs | 1.4946 μs | 12.22 |    0.45 |
| Bench350S | 139.318 μs | 2.7098 μs | 3.7093 μs | 25.01 |    0.82 |
