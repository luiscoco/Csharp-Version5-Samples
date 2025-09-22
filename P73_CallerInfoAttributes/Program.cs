using System;
using System.Runtime.CompilerServices;

class Logger
{
    public static void Log(string message,
        [CallerMemberName] string member = null,
        [CallerFilePath] string file = null,
        [CallerLineNumber] int line = 0)
    {
        Console.WriteLine(message);
        Console.WriteLine("  at " + member + " (" + System.IO.Path.GetFileName(file) + ":" + line + ")");
    }
}

class Program
{
    static void A() { Logger.Log("Hello from A"); }
    static void B() { Logger.Log("Hello from B"); }

    static void Main()
    {
        A();
        B();
        Logger.Log("Top-level log");
    }
}
