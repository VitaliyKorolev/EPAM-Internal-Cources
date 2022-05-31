using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResLib
{
    public class SharedRes : IDisposable
    {
        private ReaderWriterLockSlim stringLock = new ReaderWriterLockSlim();
        private Random random = new Random();
        private string sharedString;
        private Logger logger;
        public string SharedString
        {
            get
            {
                logger.Log("Thread waits block for reading");
                stringLock.EnterReadLock();
                logger.Log($"Thread gets block for reading");
                try
                {
                    Thread.Sleep(random.Next(100, 500));
                    logger.Log($"Thread read the value");
                    return sharedString;
                }
                finally
                {
                    stringLock.ExitReadLock();
                    logger.Log($"Thread realizes block for reading");
                }
            }
            set
            {
                stringLock.EnterUpgradeableReadLock();
                logger.Log($"Thread waits block for writing");
                stringLock.EnterWriteLock();
                logger.Log($"Thread gets block for writing");
                try
                {
                    Thread.Sleep(random.Next(500, 1000));
                    sharedString = value;
                    logger.Log($"Thread wrote the value");
                }
                finally
                {
                    stringLock.ExitWriteLock();
                    logger.Log($"Thread realizes block for writing");
                    stringLock.ExitUpgradeableReadLock();
                }
            }
        } 

        public SharedRes(string sharedString, Logger logger)
        {
            this.sharedString = sharedString;
            this.logger = logger;
            logger.Log($"Constructor of {this}");
        }
        public void Dispose()
        {
            logger.Log($"Dispose {this}");
            stringLock?.Dispose();
            logger?.Close();
        }
    }
}
