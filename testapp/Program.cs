using System.Timers;
using InfiniteAxisUtility;
using InfiniteAxisUtility.Examples;
using Timer = System.Timers.Timer;

namespace testapp;

public class Program
{
    private static Timer? _thinkTimer;
    private static BehaviorProcessor? _processor;

    static void Main(string[] args)
    {
        _processor = new BehaviorProcessor(new List<IBehavior>()
        {
            new ExampleBehavior()
        });

        _thinkTimer = new Timer(1000);
        _thinkTimer.Elapsed += Think;
        _thinkTimer.AutoReset = true;
        _thinkTimer.Enabled = true;
        Console.ReadLine();
    }

    private static void Think(object? sender, ElapsedEventArgs e)
    {
        if (_processor == null)
        {
            return;
        }

        var behavior = _processor.FindNextBehavior();
        behavior.Run();
    }
}