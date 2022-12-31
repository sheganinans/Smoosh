``` ini

BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|  Method |     Mean |     Error |    StdDev |
|-------- |---------:|----------:|----------:|
| Person1 | 6.899 μs | 0.1122 μs | 0.0995 μs |
| Person2 | 6.701 μs | 0.1283 μs | 0.1200 μs |
| Person3 | 6.859 μs | 0.1366 μs | 0.1402 μs |
