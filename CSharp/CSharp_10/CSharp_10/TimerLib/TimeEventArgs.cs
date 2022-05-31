using System;

namespace TimerLib
{
    public class TimeEventArgs : EventArgs
    {
        public int SecondsRemaining { get; }

        public TimeEventArgs(int secondsRemaining)
        {
            SecondsRemaining = secondsRemaining;
        }
    }
}
