``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|   Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|--------- |----------:|----------:|----------:|----------:|------:|--------:|
| Person0L |  1.304 μs | 0.0245 μs | 0.0537 μs |  1.297 μs |  1.00 |    0.00 |
| Person1L |  3.173 μs | 0.1186 μs | 0.3478 μs |  3.062 μs |  2.42 |    0.26 |
| Person0S |  9.013 μs | 0.1538 μs | 0.1200 μs |  9.034 μs |  7.12 |    0.31 |
| Person1S | 13.040 μs | 0.2563 μs | 0.3837 μs | 13.081 μs | 10.04 |    0.53 |
