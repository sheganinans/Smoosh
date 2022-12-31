``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|       Method |        Mean |     Error |     StdDev | Ratio | RatioSD |
|------------- |------------:|----------:|-----------:|------:|--------:|
| Tree05BenchL |    37.65 μs |  0.751 μs |   1.482 μs |  1.00 |    0.00 |
| Tree10BenchL | 1,270.21 μs | 25.968 μs |  75.750 μs | 33.53 |    2.62 |
| Tree05BenchS |    93.75 μs |  1.848 μs |   3.857 μs |  2.50 |    0.12 |
| Tree10BenchS | 3,406.15 μs | 66.905 μs | 109.926 μs | 91.04 |    4.25 |
