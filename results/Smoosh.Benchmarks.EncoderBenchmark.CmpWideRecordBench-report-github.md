``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|   Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |-----------:|----------:|----------:|------:|--------:|
| Record1L |   254.7 ns |   5.12 ns |   9.35 ns |  1.00 |    0.00 |
| Record2L |   452.7 ns |   8.68 ns |  20.46 ns |  1.77 |    0.09 |
| Record3L |   691.3 ns |  13.74 ns |  18.81 ns |  2.75 |    0.14 |
| Record1S | 1,992.9 ns |  21.56 ns |  19.11 ns |  7.80 |    0.26 |
| Record2S | 4,064.7 ns |  78.90 ns |  99.79 ns | 16.18 |    0.84 |
| Record3S | 6,547.0 ns | 125.43 ns | 144.44 ns | 26.08 |    1.20 |
