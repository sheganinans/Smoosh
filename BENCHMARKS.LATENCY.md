# `Smoosh.Latency` Benchmarks

``` ini
BenchmarkDotNet=v0.13.2, OS=debian 11
AMD Ryzen 5 3550H with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.403
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2
```

# `bool []`

|     Method |        Mean |     Error |    StdDev |  Ratio | RatioSD |
|----------- |------------:|----------:|----------:|-------:|--------:|
| Bench00000 |    328.7 ns |   6.54 ns |  12.91 ns |   1.00 |    0.00 |
| Bench05500 | 28,027.8 ns | 558.05 ns | 548.08 ns |  85.56 |    4.91 |
| Bench11000 | 54,951.5 ns | 675.98 ns | 632.31 ns | 168.15 |    8.41 |

|                                                                                                          |                                                                                                                 |
|----------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.BoolArrBench-boxplot.png)                       |                                                                                                                 |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.BoolArrBench-Bench00000-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.BoolArrBench-Bench00000-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.BoolArrBench-Bench05500-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.BoolArrBench-Bench05500-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.BoolArrBench-Bench11000-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.BoolArrBench-Bench11000-DefaultJob-timelineSmooth.png) |

---

# `int []`

|   Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |------------:|----------:|----------:|------:|--------:|
| Bench000 |    323.5 ns |   6.44 ns |  11.61 ns |  1.00 |    0.00 |
| Bench175 |  5,993.4 ns | 118.63 ns | 255.37 ns | 18.61 |    0.88 |
| Bench350 | 11,006.6 ns | 205.16 ns | 325.40 ns | 33.99 |    1.63 |


|                                                                                                       |                                                                                                              |
|-------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntArrBench-boxplot.png)                     |                                                                                                              |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntArrBench-Bench000-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntArrBench-Bench000-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntArrBench-Bench175-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntArrBench-Bench175-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntArrBench-Bench350-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntArrBench-Bench350-DefaultJob-timelineSmooth.png) |

---

# `int list`

|   Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |------------:|----------:|----------:|------:|--------:|
| Bench000 |    311.5 ns |  13.26 ns |  38.27 ns |  1.00 |    0.00 |
| Bench175 |  6,960.9 ns | 141.14 ns | 413.93 ns | 22.68 |    3.17 |
| Bench350 | 12,965.0 ns | 257.98 ns | 752.53 ns | 42.24 |    5.41 |

|                                                                                                        |                                                                                                               |
|--------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntListBench-boxplot.png)                     |                                                                                                               |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntListBench-Bench000-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntListBench-Bench000-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntListBench-Bench175-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntListBench-Bench175-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntListBench-Bench350-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntListBench-Bench350-DefaultJob-timelineSmooth.png) |

---

# `int set`

|   Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |------------:|----------:|----------:|------:|--------:|
| Bench000 |    399.7 ns |  17.86 ns |  51.82 ns |  1.00 |    0.00 |
| Bench175 |  8,834.0 ns | 174.87 ns | 454.51 ns | 22.22 |    2.91 |
| Bench350 | 16,306.8 ns | 323.98 ns | 454.18 ns | 40.76 |    5.63 |

|                                                                                                       |                                                                                                              |
|-------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntSetBench-boxplot.png)                     |                                                                                                              |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntSetBench-Bench000-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntSetBench-Bench000-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntSetBench-Bench175-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntSetBench-Bench175-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntSetBench-Bench350-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.IntSetBench-Bench350-DefaultJob-timelineSmooth.png) |

---

# `float []`

|   Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |-----------:|----------:|----------:|------:|--------:|
| Bench000 |   326.3 ns |   6.54 ns |  11.62 ns |  1.00 |    0.00 |
| Bench087 | 4,580.2 ns |  90.22 ns | 143.09 ns | 14.06 |    0.73 |
| Bench175 | 8,897.3 ns | 174.53 ns | 266.52 ns | 27.38 |    1.22 |

|                                                                                                         |                                                                                                                |
|---------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatArrBench-boxplot.png)                     |                                                                                                                |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatArrBench-Bench000-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatArrBench-Bench000-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatArrBench-Bench087-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatArrBench-Bench087-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatArrBench-Bench175-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatArrBench-Bench175-DefaultJob-timelineSmooth.png) |

---

# `float list`

|   Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |-----------:|----------:|----------:|------:|--------:|
| Bench000 |   282.8 ns |   5.64 ns |  13.07 ns |  1.00 |    0.00 |
| Bench087 | 4,793.5 ns |  94.84 ns | 158.45 ns | 16.79 |    0.82 |
| Bench175 | 9,069.8 ns | 179.89 ns | 300.55 ns | 31.79 |    1.85 |


|                                                                                                          |                                                                                                                 |
|----------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatListBench-boxplot.png)                     |                                                                                                                 |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatListBench-Bench000-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatListBench-Bench000-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatListBench-Bench087-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatListBench-Bench087-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatListBench-Bench175-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatListBench-Bench175-DefaultJob-timelineSmooth.png) |

---

# `float set`

|   Method |        Mean |     Error |    StdDev | Ratio | RatioSD |
|--------- |------------:|----------:|----------:|------:|--------:|
| Bench000 |    340.0 ns |   6.75 ns |  10.31 ns |  1.00 |    0.00 |
| Bench087 |  5,965.6 ns | 113.27 ns | 105.95 ns | 17.67 |    0.67 |
| Bench175 | 11,425.4 ns | 194.56 ns | 216.26 ns | 33.80 |    1.25 |

|                                                                                                         |                                                                                                                |
|---------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatSetBench-boxplot.png)                     |                                                                                                                |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatSetBench-Bench000-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatSetBench-Bench000-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatSetBench-Bench087-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatSetBench-Bench087-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatSetBench-Bench175-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.FloatSetBench-Bench175-DefaultJob-timelineSmooth.png) |

---

# `SmallPerson`

|  Method |     Mean |    Error |   StdDev | Ratio |
|-------- |---------:|---------:|---------:|------:|
| Person1 | 661.1 ns | 13.09 ns | 17.92 ns |  1.00 |

|                                                                                                           |                                                                                                                  |
|-----------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.SmallPersonBench-boxplot.png)                    |                                                                                                                  |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.SmallPersonBench-Person1-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.SmallPersonBench-Person1-DefaultJob-timelineSmooth.png) |

---

# `LargePerson`

|  Method |     Mean |     Error |    StdDev |
|-------- |---------:|----------:|----------:|
| Person1 | 6.899 μs | 0.1122 μs | 0.0995 μs |
| Person2 | 6.701 μs | 0.1283 μs | 0.1200 μs |
| Person3 | 6.859 μs | 0.1366 μs | 0.1402 μs |

|                                                                                                           |                                                                                                                  |
|-----------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.LargePersonBench-boxplot.png)                    |                                                                                                                  |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.LargePersonBench-Person1-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.LargePersonBench-Person1-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.LargePersonBench-Person2-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.LargePersonBench-Person2-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.LargePersonBench-Person3-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.LargePersonBench-Person3-DefaultJob-timelineSmooth.png) |

---

# `WideRecord`

|  Method |     Mean |   Error |   StdDev | Ratio | RatioSD |
|-------- |---------:|--------:|---------:|------:|--------:|
| Record1 | 278.9 ns | 5.54 ns | 10.26 ns |  1.00 |    0.00 |
| Record2 | 359.9 ns | 7.21 ns |  8.58 ns |  1.29 |    0.06 |
| Record3 | 498.8 ns | 9.89 ns | 14.81 ns |  1.78 |    0.09 |

|                                                                                                          |                                                                                                                 |
|----------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.WideRecordBench-boxplot.png)                    |                                                                                                                 |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.WideRecordBench-Record1-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.WideRecordBench-Record1-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.WideRecordBench-Record2-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.WideRecordBench-Record2-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.WideRecordBench-Record3-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.WideRecordBench-Record3-DefaultJob-timelineSmooth.png) |

---

# `NestedRecord`

|   Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|--------- |---------:|---------:|---------:|------:|--------:|
| Nested01 | 319.6 ns |  6.35 ns | 10.43 ns |  1.00 |    0.00 |
| Nested05 | 599.5 ns | 12.02 ns | 26.14 ns |  1.87 |    0.09 |
| Nested10 | 854.4 ns | 16.36 ns | 19.48 ns |  2.68 |    0.11 |

|                                                                                               |                                                                                                      |
|-----------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.NestedBench-boxplot.png)                     |                                                                                                      |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.NestedBench-Nested01-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.NestedBench-Nested01-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.NestedBench-Nested05-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.NestedBench-Nested05-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.NestedBench-Nested10-DefaultJob-density.png)  | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.NestedBench-Nested10-DefaultJob-timelineSmooth.png) |

# `Tree`

|      Method |        Mean |       Error |      StdDev |  Ratio | RatioSD |
|------------ |------------:|------------:|------------:|-------:|--------:|
|   LeafBench |    269.4 ns |     5.40 ns |    10.01 ns |   1.00 |    0.00 |
| Tree01Bench |    364.7 ns |     6.88 ns |     6.76 ns |   1.36 |    0.06 |
| Tree05Bench |  2,709.5 ns |    43.04 ns |    38.16 ns |  10.08 |    0.37 |
| Tree10Bench | 82,310.7 ns | 1,421.88 ns | 2,039.22 ns | 304.50 |   15.52 |

|                                                                                                        |                                                                                                               |
|--------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeBench-boxplot.png)                        |                                                                                                               |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeBench-LeafBench-DefaultJob-density.png)   | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeBench-LeafBench-DefaultJob-timelineSmooth.png)   |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeBench-Tree01Bench-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeBench-Tree01Bench-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeBench-Tree05Bench-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeBench-Tree05Bench-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeBench-Tree10Bench-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeBench-Tree10Bench-DefaultJob-timelineSmooth.png) |

# `Tree` Enc & Dec

|      Method |           Mean |        Error |       StdDev |    Ratio | RatioSD |
|------------ |---------------:|-------------:|-------------:|---------:|--------:|
|   LeafBench |       652.5 ns |     13.02 ns |     14.99 ns |     1.00 |    0.00 |
| Tree01Bench |     1,700.7 ns |     15.91 ns |     14.11 ns |     2.62 |    0.07 |
| Tree05Bench |    33,872.2 ns |    644.16 ns |    571.03 ns |    52.19 |    1.33 |
| Tree10Bench | 1,111,835.8 ns | 21,284.58 ns | 23,657.75 ns | 1,706.59 |   51.90 |

|                                                                                                        |                                                                                                               |
|--------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------|
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeEncDecBench-boxplot.png)                        |                                                                                                               |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeEncDecBench-LeafBench-DefaultJob-density.png)   | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeEncDecBench-LeafBench-DefaultJob-timelineSmooth.png)   |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeEncDecBench-Tree01Bench-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeEncDecBench-Tree01Bench-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeEncDecBench-Tree05Bench-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeEncDecBench-Tree05Bench-DefaultJob-timelineSmooth.png) |
| ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeEncDecBench-Tree10Bench-DefaultJob-density.png) | ![](./results/Smoosh.Benchmarks.EncoderBenchmark.TreeEncDecBench-Tree10Bench-DefaultJob-timelineSmooth.png) |
