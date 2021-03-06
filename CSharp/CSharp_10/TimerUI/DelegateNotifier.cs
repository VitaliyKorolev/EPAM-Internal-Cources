using System;
using TimerLib;

namespace TimerUI
{
    public class DelegateNotifier: ICountDownNotifier
    {
        private Timer Timer;
        private TimerLib.EventHandler<TimeEventArgs> startCountDownHandler;
        private TimerLib.EventHandler<TimeEventArgs> secondsRemainingHandler;
        private TimerLib.EventHandler<TimeEventArgs> finishCountDownHandler;
        TimeHasStarted TimeHasStarted { get; }
        Action<string> TimeHasFinished { get; }

        public DelegateNotifier(Timer timer, TimeHasStarted timeHasStarted, Action<string> timeHasFinished)
        {
            Timer = timer;
            TimeHasStarted = timeHasStarted;
            TimeHasFinished = timeHasFinished;
        }
        void ICountDownNotifier.Init()
        {
            startCountDownHandler = delegate (object source, TimeEventArgs e)
            {
                Timer t = source as Timer;
                if (t != null)
                {
                    TimeHasStarted?.Invoke(t.TimerName, e.SecondsRemaining);
                }
            };
            secondsRemainingHandler = delegate (object source, TimeEventArgs e)
            {
                Timer t = source as Timer;
                if (t != null)
                {
                    Console.WriteLine($"[{DateTime.Now.Second}]Timer: Task {t.TimerName}:  {e.SecondsRemaining} seconds remaining.");
                }
            };
            finishCountDownHandler = delegate (object source, TimeEventArgs e)
            {
                Timer t = source as Timer;
                if (t != null)
                {
                    TimeHasFinished?.Invoke(t.TimerName);
                }
            };
            Timer.StartCountdown += startCountDownHandler;
            Timer.SecondsRemaining += secondsRemainingHandler;
            Timer.FinishCountdown += finishCountDownHandler;
        }

        void ICountDownNotifier.Run()
        {
            Timer.Run();
        }

        void ICountDownNotifier.Unsubscribe()
        {
            Timer.StartCountdown -= startCountDownHandler;
            Timer.SecondsRemaining -= secondsRemainingHandler;
            Timer.FinishCountdown -= finishCountDownHandler;
        }
    }
}
