``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|  Method |     Mean |   Error |   StdDev | Ratio | RatioSD |
|-------- |---------:|--------:|---------:|------:|--------:|
| Record1 | 278.9 ns | 5.54 ns | 10.26 ns |  1.00 |    0.00 |
| Record2 | 359.9 ns | 7.21 ns |  8.58 ns |  1.29 |    0.06 |
| Record3 | 498.8 ns | 9.89 ns | 14.81 ns |  1.78 |    0.09 |
