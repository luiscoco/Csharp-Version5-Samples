# C# 5.0 Features (Visual Studio 2012, .NET 4.5)

This solution demonstrates the **major features added in C# 5.0**.

## Projects
- **P68_AsyncBasics** — `async`/`await` basics with `Task` and `Task<T>`.
- **P69_AsyncComposition** — `Task.WhenAll` and `Task.WhenAny` composition.
- **P70_AsyncCancellation** — Cooperative cancellation via `CancellationToken`.
- **P71_AsyncProgress** — Progress reporting with `IProgress<T>` / `Progress<T>`.
- **P72_AsyncVoidEvent** — `async void` for event handlers (when appropriate).
- **P73_CallerInfoAttributes** — `[CallerMemberName]`, `[CallerFilePath]`, `[CallerLineNumber]`.

> Note: C# 5.0 is mostly about `async`/`await` (compiler transforms to state machines) plus **caller info attributes**.

## Build & Run
```bash
dotnet restore
dotnet build
dotnet run --project P68_AsyncBasics
```
Targets `.NET 10.0` with `<LangVersion>5</LangVersion>` to enforce C# 5.0 syntax.
