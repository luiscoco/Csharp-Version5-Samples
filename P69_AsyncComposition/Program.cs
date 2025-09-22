using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task<int> WorkAsync(int id, int ms)
    {
        await Task.Delay(ms);
        Console.WriteLine("Task " + id + " finished after " + ms + "ms");
        return id;
    }

    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async System.Threading.Tasks.Task MainAsync()
    {
        // WhenAll: run in parallel and await them all
        Task<int>[] tasks = new [] { WorkAsync(1, 200), WorkAsync(2, 400), WorkAsync(3, 100) };
        int[] results = await Task.WhenAll(tasks);
        Console.WriteLine("WhenAll results: " + string.Join(",", results));

        // WhenAny: get first to complete, then await remaining
        Task<int> first = await Task.WhenAny(tasks);
        Console.WriteLine("WhenAny winner: Task " + first.Result);
        await Task.WhenAll(tasks); // ensure others completed
    }
}
