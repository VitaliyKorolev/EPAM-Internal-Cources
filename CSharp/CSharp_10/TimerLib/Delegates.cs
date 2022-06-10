using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerLib
{
    public delegate void TimeHasStarted(string taskName, int executionTime);
    public delegate void EventHandler<TimeEventArgs>(object source, TimeEventArgs e);
}
