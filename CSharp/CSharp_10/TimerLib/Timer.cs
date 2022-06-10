using System.Threading;

namespace TimerLib
{
    public class Timer
    {
        public event EventHandler<TimeEventArgs> StartCountdown;
        public event EventHandler<TimeEventArgs> SecondsRemaining;
        public event EventHandler<TimeEventArgs> FinishCountdown;
        public string TimerName { get; }
        public int Time { get; }

        public Timer(string timerName, int time)
        {
            TimerName = timerName;
            Time = time;
        }

        public void Run()
        {
            if (StartCountdown == null)
                return;

            OnStartCountdown(new TimeEventArgs(Time));
            for (int i = Time; i >= 0; i--)
            {
                OnSecondsRemaining(new TimeEventArgs(i));
                if (i == 0) { break; }
                Thread.Sleep(1000);
            }
            //OnSecondsRemaining(new TimeEventArgs(0));
            OnFinishCountdown(new TimeEventArgs(0));
        }

        protected virtual void OnStartCountdown(TimeEventArgs e)
        {
            StartCountdown?.Invoke(this, e);
        }

        protected virtual void OnSecondsRemaining(TimeEventArgs e)
        {
            SecondsRemaining?.Invoke(this, e);
        }

        protected virtual void OnFinishCountdown(TimeEventArgs e)
        {
            FinishCountdown?.Invoke(this, e);
        }
    }
}
