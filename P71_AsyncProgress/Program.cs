using System;
using System.Threading.Tasks;

class Program
{
    static async Task DownloadAsync(IProgress<int> progress)
    {
        for (int i = 1; i <= 5; i++)
        {
            await Task.Delay(150);
            if (progress != null) progress.Report(i * 20);
        }
    }

    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async System.Threading.Tasks.Task MainAsync()
    {
        var progress = new Progress<int>(p => Console.WriteLine("Progress: " + p + "%"));
        await DownloadAsync(progress);
        Console.WriteLine("Done");
    }
}
