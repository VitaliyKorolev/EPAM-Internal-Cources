using System;
using TimerLib;
using System.Collections.Generic;

namespace TimerUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer t1 = new Timer("Reading the task", 1);
            Timer t2 = new Timer("Сompleting the task", 2);
            Timer t3 = new Timer("Checking the task before sending", 5);
            ICountDownNotifier[] countDownNotifiers =
            {
                 new MethodNotifier(t1, OnTimeHasStarted, OnTimeHasFinished),
                 new DelegateNotifier(t2, OnTimeHasStarted, OnTimeHasFinished),
                 new LambdaNotifier(t3, OnTimeHasStarted, OnTimeHasFinished),
            };
            foreach(var countDownNotifier in countDownNotifiers)
            {
                countDownNotifier.Init();
                countDownNotifier.Run();
                countDownNotifier.Unsubscribe();
                Console.WriteLine();
            }
        }

        static void OnTimeHasStarted(string taskName, int executionTime)
        {
            Console.WriteLine($"[{DateTime.Now.Second}]Timer: Task {taskName}: has started for {executionTime} seconds.");
        }

        static void OnTimeHasFinished(string taskName)
        {
            Console.WriteLine($"[{DateTime.Now.Second}]Timer: Task {taskName}: has finished");
        }
    }
}
