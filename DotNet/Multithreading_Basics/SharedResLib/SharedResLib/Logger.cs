using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResLib
{
    public class Logger
    {
        private TraceSource traceSource = new TraceSource("SharedRes");
        private const string timeFormat = "yy/MM/dd HH:mm:ss.fff";
        public Logger(string path)
        {
            traceSource.Switch = new SourceSwitch("sourceSwitch", "Information");
            traceSource.Listeners.Remove("Default");
            TextWriterTraceListener textListener = new TextWriterTraceListener(path);
            textListener.Filter = new EventTypeFilter(SourceLevels.Information);
            traceSource.Listeners.Add(textListener);
            //traceSource.Switch.Level = SourceLevels.Information;
            Log($"Constructor of {this}");
        }

        public void Log(string message)
        {
            string str = $"[{DateTime.Now.ToString(timeFormat)}] [{Thread.CurrentThread.ManagedThreadId}]";
            traceSource.TraceInformation(str + message);
        }
        public void Close()
        {
            Log($"Dispose {this}");
            traceSource.Close();
        }
    }
}
