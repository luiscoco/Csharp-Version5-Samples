using System;
using System.Threading.Tasks;

class Button
{
    public event EventHandler Click;
    public void RaiseClick() { if (Click != null) Click(this, EventArgs.Empty); }
}

class Program
{
    // In C# 5, 'async void' is intended for event handlers
    static async void OnClick(object sender, EventArgs e)
    {
        await Task.Delay(200);
        Console.WriteLine("Button clicked (handled asynchronously).");
    }

    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async System.Threading.Tasks.Task MainAsync()
    {
        Button b = new Button();
        b.Click += OnClick;
        b.RaiseClick();
        await Task.Delay(300); // keep app alive so handler can finish
        Console.WriteLine("Main done.");
    }
}
