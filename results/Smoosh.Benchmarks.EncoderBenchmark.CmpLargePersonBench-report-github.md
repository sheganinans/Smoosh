``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
| Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------- |---------:|---------:|---------:|------:|--------:|
|    P0L | 24.15 μs | 0.251 μs | 0.223 μs |  1.00 |    0.00 |
|    P1L | 24.72 μs | 0.489 μs | 0.503 μs |  1.02 |    0.02 |
|    P2L | 26.48 μs | 0.524 μs | 1.226 μs |  1.10 |    0.04 |
|    P0S | 41.88 μs | 0.831 μs | 2.289 μs |  1.76 |    0.14 |
|    P1S | 39.84 μs | 0.768 μs | 1.241 μs |  1.66 |    0.06 |
|    P2S | 38.69 μs | 0.764 μs | 0.750 μs |  1.60 |    0.04 |
