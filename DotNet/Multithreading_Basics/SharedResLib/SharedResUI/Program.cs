using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SharedResLib;

namespace SharedResUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings["loggerPath"];
            Logger logger = new Logger(path);
            logger.Log($"Program starts version {Assembly.GetExecutingAssembly().GetName().Version}");
            SharedRes res = new SharedRes("", logger);
            List <Thread> threads = new List<Thread>();

            for(int i = 0; i < 5; i++)
            {
                Thread t = new Thread(() =>
                {
                    Read(res, logger);
                });
                t.Start();
                threads.Add(t);
            }

            var t1 = new Thread(() =>
            {
                string str = $"[{DateTime.Now:yy/MM/dd HH:mm:ss.fff}][{Thread.CurrentThread.ManagedThreadId}]Task ";
                Write(res, logger, str);
            });
            t1.Start();
            threads.Add(t1);

            for (int i = 0; i < 3; i++)
            {
                Thread t = new Thread(() =>
                {
                    Read(res, logger);
                });
                t.Start();
                threads.Add(t);
            }

            var t2 = new Thread(() =>
            {
                string str = $"[{DateTime.Now:yy/MM/dd HH:mm:ss.fff}][{Thread.CurrentThread.ManagedThreadId}]Task ";
                Write(res, logger, str);
            });
            t2.Start();
            threads.Add(t2);

            for (int i = 0; i < 2; i++)
            {
                Thread t = new Thread(() =>
                {
                    Read(res, logger);
                });
                t.Start();
                threads.Add(t);
            }

            foreach(Thread thread in threads)
            {
                thread.Join();
            }

            logger.Log($"Program ends");
            res.Dispose();
        }
        static void Read(SharedRes res, Logger logger)
        {
            logger.Log($"Thread starts");
            logger.Log($"Read method starts");
            string value = res.SharedString;
            logger.Log($"Read method ends");
        }

        static void Write(SharedRes res, Logger logger, string str)
        {
            logger.Log($"Thread starts");
            logger.Log($"Write method starts");
            res.SharedString = str;
            logger.Log($"Write method ends");
        }
    }
}
