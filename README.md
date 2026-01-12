# C# 5.0 Features – Sample Projects

This solution demonstrates the major features added in **C# 5.0** (Visual Studio 2012 / .NET 4.5).  
Each project is a minimal console app focused on a single feature.

---

## Projects in this repo

- `P72_AsyncBasics` — `async`/`await` basics with `Task` and `Task<T>`  
- `P73_AsyncComposition` — Task composition with `Task.WhenAll` and `Task.WhenAny`  
- `P74_AsyncCancellation` — Cooperative cancellation via `CancellationToken`  
- `P75_AsyncProgress` — Progress reporting with `IProgress<T>` / `Progress<T>`  
- `P76_AsyncVoidEvent` — `async void` for **event handlers** (only)  
- `P77_CallerInfoAttributes` — Caller info attributes: `[CallerMemberName]`, `[CallerFilePath]`, `[CallerLineNumber]`  

> C# 5.0 is primarily about **asynchronous programming** (`async`/`await`) and **caller info attributes**.
---

## 🚀 Feature Walkthrough & Tiny Examples

### P72_AsyncBasics - async/await
- **What’s new**: Mark methods `async` and use `await` on `Task`/`Task<T>` to write non‑blocking code that *reads like synchronous code*. The compiler rewrites it into a state machine.
- **Sketch**:
  ```csharp
  static async Task<string> DownloadAsync(string url)
  {
      using var client = new HttpClient();
      return await client.GetStringAsync(url); // suspension point
  }

  // Usage
  string html = await DownloadAsync("https://example.com");
  ```
---

### P73_AsyncComposition - Compose tasks
- **What’s new**: Combine multiple asynchronous operations with `Task.WhenAll` / `Task.WhenAny`.
- **Sketch**:
  ```csharp
  var urls = new[] { "https://a", "https://b" };
  var tasks = urls.Select(u => client.GetStringAsync(u));
  string[] pages = await Task.WhenAll(tasks);  // run concurrently
  var first = await Task.WhenAny(tasks);       // first to complete
  ```
---

### P74_AsyncCancellation - Cancellation
- **What’s new**: Cooperative cancellation pattern with `CancellationToken` and `CancellationTokenSource`.
- **Sketch**:
  ```csharp
  var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
  await WorkAsync(cts.Token);

  static async Task WorkAsync(CancellationToken ct)
  {
      while (!ct.IsCancellationRequested)
      {
          await Task.Delay(250, ct);
      }
  }
  ```
---

### P75_AsyncProgress - Progress reporting
- **What’s new**: Report progress from async code via `IProgress<T>` / `Progress<T>`.
- **Sketch**:
  ```csharp
  var progress = new Progress<int>(p => Console.WriteLine($"Progress {p}%"));
  await DownloadWithProgressAsync(progress);

  static async Task DownloadWithProgressAsync(IProgress<int> progress)
  {
      for (int i = 0; i <= 100; i += 10)
      {
          progress.Report(i);
          await Task.Delay(100);
      }
  }
  ```
---

### P76_AsyncVoidEvent - async void, event handlers
- **What’s new**: Event handlers can be `async void`, because the event signature requires `void`. For all other async methods, prefer `Task`/`Task<T>`.
- **Sketch**:
  ```csharp
  static event EventHandler? Clicked;

  static void Main()
  {
      Clicked += async (_, __) => { await Task.Delay(100); Console.WriteLine("done"); };
      Clicked?.Invoke(null, EventArgs.Empty);
  }
  ```

> Avoid `async void` in regular methods (hard to catch exceptions / compose). Use it **only** for event handlers.

---

### P77_CallerInfoAttributes - Caller info attributes
- **What’s new**: Attributes from `System.Runtime.CompilerServices` let you capture the **caller’s** member name, file path, and line number *automatically*.
- **Sketch**:
  ```csharp
  using System.Runtime.CompilerServices;

  static void Log(string message,
                  [CallerMemberName] string? member = null,
                  [CallerFilePath] string? file = null,
                  [CallerLineNumber] int line = 0)
      => Console.WriteLine($"{file}:{line} [{member}] {message}");

  // Usage:
  Log("starting...");
  ```
---

## Build & Run

From the repo root:
```bash
dotnet restore
dotnet build
dotnet run --project P68_AsyncBasics
# …or any other project (P69_…, P70_…, … P73_…)
```

---

## ✅ Tips & Best Practices

- Prefer `Task`/`Task<T>` return types for async APIs; keep `async void` for *event handlers* only.  
- Use `ConfigureAwait(false)` in library code if you don’t require the caller’s synchronization context.  
- Always pass a `CancellationToken` for long‑running operations.  
- Caller info attributes are great for logging, tracing, and implementing `INotifyPropertyChanged` helpers.
