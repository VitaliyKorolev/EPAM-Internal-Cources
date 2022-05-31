using System;
using TimerLib;

namespace TimerUI
{
    public class MethodNotifier : ICountDownNotifier
    {
        private Timer Timer;
        TimeHasStarted TimeHasStarted { get; }
        Action<string> TimeHasFinished { get; }

        public MethodNotifier(Timer timer, TimeHasStarted timeHasStarted, Action<string> timeHasFinished)
        {
            Timer = timer;
            this.TimeHasStarted = timeHasStarted;
            this.TimeHasFinished = timeHasFinished;
        }

        void ICountDownNotifier.Init()
        {
            Timer.StartCountdown += OnStartCountdown;
            Timer.SecondsRemaining += OnSecondsRemaining;
            Timer.FinishCountdown += OnFinishCountdown;
        }

        void ICountDownNotifier.Run()
        {
            Timer.Run();
        }

        void ICountDownNotifier.Unsubscribe()
        {
            Timer.StartCountdown -= OnStartCountdown;
            Timer.SecondsRemaining -= OnSecondsRemaining;
            Timer.FinishCountdown -= OnFinishCountdown;
        }

        public void OnStartCountdown(object source, TimeEventArgs e)
        {
            Timer t = source as Timer;
            if (t != null)
            {
                TimeHasStarted?.Invoke(t.TimerName, e.SecondsRemaining);
            }
        }

        public void OnSecondsRemaining(object source, TimeEventArgs e)
        {
            Timer t = source as Timer;
            if (t != null)
            {
                Console.WriteLine($"[{DateTime.Now.Second}]Timer: Task {t.TimerName}:  {e.SecondsRemaining} seconds remaining.");
            }
        }

        public void OnFinishCountdown(object source, TimeEventArgs e)
        {
            Timer t = source as Timer;
            if (t != null)
            {
                TimeHasFinished?.Invoke(t.TimerName);
            }
        }
    }
}
