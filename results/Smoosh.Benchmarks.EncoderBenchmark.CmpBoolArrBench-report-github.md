``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|      Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------------ |---------:|---------:|---------:|------:|--------:|
| Bench05500L | 28.53 μs | 0.244 μs | 0.203 μs |  1.00 |    0.00 |
| Bench11000L | 56.79 μs | 0.583 μs | 0.487 μs |  1.99 |    0.02 |
| Bench05500S | 49.10 μs | 0.956 μs | 0.894 μs |  1.72 |    0.04 |
| Bench11000S | 94.65 μs | 1.029 μs | 0.859 μs |  3.32 |    0.05 |
