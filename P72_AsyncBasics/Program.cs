using System;
using System.Threading.Tasks;

class Program
{
    static async Task<int> SlowAddAsync(int a, int b)
    {
        await Task.Delay(300); // simulate async work
        return a + b;
    }

    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async System.Threading.Tasks.Task MainAsync()
    {
        Console.WriteLine("Starting...");
        int sum = await SlowAddAsync(3, 4);
        Console.WriteLine("3 + 4 = " + sum);
        Console.WriteLine("Done.");
    }
}
