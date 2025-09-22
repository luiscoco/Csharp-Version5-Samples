using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task<int> CountAsync(int limit, CancellationToken ct)
    {
        for (int i = 1; i <= limit; i++)
        {
            ct.ThrowIfCancellationRequested();
            await Task.Delay(100, ct);
            Console.WriteLine("Tick " + i);
        }
        return limit;
    }

    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async System.Threading.Tasks.Task MainAsync()
    {
        var cts = new CancellationTokenSource();
        Task<int> t = CountAsync(20, cts.Token);
        Task cancel = Task.Run(async () => { await Task.Delay(550); cts.Cancel(); });

        try
        {
            int result = await t;
            Console.WriteLine("Completed with " + result);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Canceled!");
        }
    }
}
