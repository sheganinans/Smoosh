``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|       Method |         Mean |      Error |     StdDev |  Ratio | RatioSD |
|------------- |-------------:|-----------:|-----------:|-------:|--------:|
| Tree05BenchL |     9.940 μs |  0.1856 μs |  0.1645 μs |   1.00 |    0.00 |
| Tree10BenchL |   350.989 μs |  5.6621 μs |  5.2963 μs |  35.30 |    0.73 |
| Tree05BenchS |    60.323 μs |  0.9711 μs |  0.9084 μs |   6.07 |    0.17 |
| Tree10BenchS | 2,194.997 μs | 41.3432 μs | 44.2368 μs | 220.47 |    5.77 |
