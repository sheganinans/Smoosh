# Comparison Benchmarks

``` ini
BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2
```

# `bool []`

|      Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------------ |---------:|---------:|---------:|------:|--------:|
| Bench05500L | 28.53 μs | 0.244 μs | 0.203 μs |  1.00 |    0.00 |
| Bench11000L | 56.79 μs | 0.583 μs | 0.487 μs |  1.99 |    0.02 |
| Bench05500S | 49.10 μs | 0.956 μs | 0.894 μs |  1.72 |    0.04 |
| Bench11000S | 94.65 μs | 1.029 μs | 0.859 μs |  3.32 |    0.05 |

|                                                                                                      |                                                                                                             |
|------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpBoolArrBench-boxplot.png)                       |                                                                                                             |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpBoolArrBench-Bench05500L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpBoolArrBench-Bench05500L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpBoolArrBench-Bench11000L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpBoolArrBench-Bench11000L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpBoolArrBench-Bench05500S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpBoolArrBench-Bench05500S-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpBoolArrBench-Bench11000S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpBoolArrBench-Bench11000S-DefaultJob-timelineSmooth.png) |


# `int []`

|    Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|---------- |-----------:|----------:|----------:|------:|--------:|
| Bench175L |   5.584 μs | 0.1110 μs | 0.1321 μs |  1.00 |    0.00 |
| Bench350L |  11.400 μs | 0.2242 μs | 0.4477 μs |  2.02 |    0.08 |
| Bench175S |  68.140 μs | 1.3447 μs | 1.4946 μs | 12.22 |    0.45 |
| Bench350S | 139.318 μs | 2.7098 μs | 3.7093 μs | 25.01 |    0.82 |

|                                                                                                      |                                                                                                             |
|------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpIntArrBench-boxplot.png)                       |                                                                                                             |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpIntArrBench-Bench175L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpIntArrBench-Bench175L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpIntArrBench-Bench350L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpIntArrBench-Bench350L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpIntArrBench-Bench175S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpIntArrBench-Bench175S-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpIntArrBench-Bench350S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpIntArrBench-Bench350S-DefaultJob-timelineSmooth.png) |


# Small `Person`

|   Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|--------- |----------:|----------:|----------:|----------:|------:|--------:|
| Person0L |  1.304 μs | 0.0245 μs | 0.0537 μs |  1.297 μs |  1.00 |    0.00 |
| Person1L |  3.173 μs | 0.1186 μs | 0.3478 μs |  3.062 μs |  2.42 |    0.26 |
| Person0S |  9.013 μs | 0.1538 μs | 0.1200 μs |  9.034 μs |  7.12 |    0.31 |
| Person1S | 13.040 μs | 0.2563 μs | 0.3837 μs | 13.081 μs | 10.04 |    0.53 |


|                                                                                                      |                                                                                                             |
|------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpSmallPersonBench-boxplot.png)                       |                                                                                                             |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpSmallPersonBench-Person0L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpSmallPersonBench-Person0L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpSmallPersonBench-Person1L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpSmallPersonBench-Person1L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpSmallPersonBench-Person0S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpSmallPersonBench-Person0S-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpSmallPersonBench-Person1S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpSmallPersonBench-Person1S-DefaultJob-timelineSmooth.png) |


# Large `Person`

| Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------- |---------:|---------:|---------:|------:|--------:|
|    P0L | 24.15 μs | 0.251 μs | 0.223 μs |  1.00 |    0.00 |
|    P1L | 24.72 μs | 0.489 μs | 0.503 μs |  1.02 |    0.02 |
|    P2L | 26.48 μs | 0.524 μs | 1.226 μs |  1.10 |    0.04 |
|    P0S | 41.88 μs | 0.831 μs | 2.289 μs |  1.76 |    0.14 |
|    P1S | 39.84 μs | 0.768 μs | 1.241 μs |  1.66 |    0.06 |
|    P2S | 38.69 μs | 0.764 μs | 0.750 μs |  1.60 |    0.04 |

|                                                                                                      |                                                                                                             |
|------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-boxplot.png)                       |                                                                                                             |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P0L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P0L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P1L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P1L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P2L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P2L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P0S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P0S-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P1S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P1S-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P2S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpLargePersonBench-P1S-DefaultJob-timelineSmooth.png) |


# `Nested`

|    Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|---------- |------------:|----------:|----------:|------:|--------:|
| Nested01L |    289.4 ns |   6.02 ns |  17.28 ns |  1.00 |    0.00 |
| Nested05L |    868.2 ns |  16.87 ns |  20.08 ns |  2.91 |    0.14 |
| Nested10L |  1,530.3 ns |  30.52 ns |  51.82 ns |  5.29 |    0.41 |
|  Nested1S |  1,990.3 ns |  30.30 ns |  25.30 ns |  6.68 |    0.28 |
|  Nested2S |  7,777.2 ns | 155.31 ns | 207.34 ns | 26.18 |    1.32 |
|  Nested3S | 13,944.9 ns | 267.33 ns | 307.86 ns | 46.83 |    2.07 |

|                                                                                                   |                                                                                                          |
|---------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-boxplot.png)                      |                                                                                                          |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested01L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested01L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested05L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested05L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested10L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested10L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested01S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested01S-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested05S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested05S-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested10S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpNestedBench-Nested10S-DefaultJob-timelineSmooth.png) |


# `Tree`

|       Method |         Mean |      Error |     StdDev |  Ratio | RatioSD |
|------------- |-------------:|-----------:|-----------:|-------:|--------:|
| Tree05BenchL |     9.940 μs |  0.1856 μs |  0.1645 μs |   1.00 |    0.00 |
| Tree10BenchL |   350.989 μs |  5.6621 μs |  5.2963 μs |  35.30 |    0.73 |
| Tree05BenchS |    60.323 μs |  0.9711 μs |  0.9084 μs |   6.07 |    0.17 |
| Tree10BenchS | 2,194.997 μs | 41.3432 μs | 44.2368 μs | 220.47 |    5.77 |

|                                                                                                      |                                                                                                             |
|------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpTreeEncDecBench-boxplot.png)                       |                                                                                                             |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpTreeEncDecBench-Tree05BenchL-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpTreeEncDecBench-Tree05BenchL-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpTreeEncDecBench-Tree10BenchL-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpTreeEncDecBench-Tree10BenchL-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpTreeEncDecBench-Tree05BenchS-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpTreeEncDecBench-Tree05BenchS-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpTreeEncDecBench-Tree10BenchS-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpTreeEncDecBench-Tree10BenchS-DefaultJob-timelineSmooth.png) |


# `WideRecord`

|   Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |-----------:|----------:|----------:|------:|--------:|
| Record1L |   254.7 ns |   5.12 ns |   9.35 ns |  1.00 |    0.00 |
| Record2L |   452.7 ns |   8.68 ns |  20.46 ns |  1.77 |    0.09 |
| Record3L |   691.3 ns |  13.74 ns |  18.81 ns |  2.75 |    0.14 |
| Record1S | 1,992.9 ns |  21.56 ns |  19.11 ns |  7.80 |    0.26 |
| Record2S | 4,064.7 ns |  78.90 ns |  99.79 ns | 16.18 |    0.84 |
| Record3S | 6,547.0 ns | 125.43 ns | 144.44 ns | 26.08 |    1.20 |

|                                                                                                      |                                                                                                             |
|------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-boxplot.png)                       |                                                                                                             |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record1L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record1L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record2L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record2L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record3L-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record3L-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record1S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record1S-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record2S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record2S-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record3S-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.CmpWideRecordBench-Record3S-DefaultJob-timelineSmooth.png) |
