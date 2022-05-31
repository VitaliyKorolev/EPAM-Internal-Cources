using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageDownloaderUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info($"[{Thread.CurrentThread.ManagedThreadId}] Program starts");
            Application.Run(new MainForm(logger));
            logger.Info($"[{Thread.CurrentThread.ManagedThreadId}] Program ends");
        }
    }
}
