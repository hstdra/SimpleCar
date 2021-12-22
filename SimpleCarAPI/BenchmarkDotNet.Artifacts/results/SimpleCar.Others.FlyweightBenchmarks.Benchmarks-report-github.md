``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1348 (21H1/May2021Update)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.100
  [Host] : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Dry    : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

Job=Dry  IterationCount=1  LaunchCount=1  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  

```
|          Method |     Mean | Error | Allocated |
|---------------- |---------:|------:|----------:|
| TestNoFlyWeight | 15.65 ms |    NA |  1,588 KB |
|   TestFlyWeight | 11.70 ms |    NA |    903 KB |
