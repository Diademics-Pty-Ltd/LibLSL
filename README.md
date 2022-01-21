# LibLSL
Modern C# wrapper for the **LabStreamingLayer** (LSL): https://github.com/sccn/labstreaminglayer/. There exist at least two previous C# wrappers, [liblsl-Csharp](https://github.com/labstreaminglayer/liblsl-Csharp/tree/c74a4d4b0a7049cbc59fa98995706de8fa26b395) and [LSL4Unity](https://github.com/labstreaminglayer/LSL4Unity). This wrapper attempts to improve on those with the following features:
* Modern C# 10/.NET 5.0 code style adhering to [Microsoft Coding Conventions] (https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
* Better use of language features such as properties, generics, and overloads
* Use of `List<T>` and `List<List<T>>` containers for receiving data and timestamps
* Improved namespaces (no more `LSL.LSL.method`)
* Improved data receiver examples showing using threads and delegates
* Growing list of utility programs (see TODO list) for testing and network diagnostics
  
# TODO:
  * In code comments on all methods and classes
  * NuGet packaging
  * Further utility programs:
    - Stream Diagnostic
    - ???
  * Async patterns for pull and resolve functions (Note: at the moment this can be 'faked' with `Task.Delay()` but future plans for improving LSL include implementing callback functions which will allow truly asynchronous---not just parallel---programming patterns
  * Explanation in this README for how to include LibLSL.dll and lsl.dll in a C# project.
  
# Acknowledgements:
  * The whole LSL development community esp. [tstenner](https://github.com/tstenner), [cboulay](https://github.com/cboulay/), and [chkothe](https://github.com/chkothe)
  * Big ups to [markheath](https://github.com/naudio/NAudio/commits?author=markheath) whose approach to [type punning arrays of different types](https://github.com/naudio/NAudio/blob/master/NAudio.Core/Wave/WaveOutputs/WaveBuffer.cs) I completely stole for [`GenericSampleBuffer`](https://github.com/Diademics-Pty-Ltd/LibLSL/blob/main/src/lib/Internal/GenericDataBuffer.cs)
  
  # Using
  In order to use `LibLSL` in your C# project, you must include a reference to the wrapper (LibLSL.dll/dylib/so) _and_ lsl itself (lsl.dll/dylib/so). 
  
  
