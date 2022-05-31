using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ImageDownloaderLib
{
    public delegate void DownloadStartedEventHandler(object sender, DownloaderEventArgs e);
    public delegate void DownloadProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
   
    public class Downloader
    {
        public event DownloadStartedEventHandler OnDownloadStarted;
        public event DownloadProgressChangedEventHandler OnDownloadProgressChanged;
        public event AsyncCompletedEventHandler OnDownloadImageCompleted;
        private Logger logger;
        public Downloader(Logger logger)
        {
            this.logger = logger;
        }

        public async Task<Bitmap> DownloadImageTaskAsync(string url, CancellationToken cancellationToken)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.UserAgent, "Hello world");
            Bitmap image = null;
            Stream stream = null;
            try
            {
                stream = await wc.OpenReadTaskAsync(url);
                int sizeInBytes = Convert.ToInt32(wc.ResponseHeaders["Content-Length"]);
                string type = wc.ResponseHeaders["Content-Type"];
                if (!(type == "image/png" || type == "image/jpeg" || type == "image/gif"))
                {
                    throw new ArgumentException("There is no image in this URL");
                }

                logger.Info($"[{Thread.CurrentThread.ManagedThreadId}] Download starts");
                OnDownloadStarted?.Invoke(this, new DownloaderEventArgs(type, sizeInBytes));

                var bufferSize = 2048*2;
                var buffer = new byte[bufferSize];
                var result = new List<byte>(sizeInBytes);

                var readBytes = 0;
                int byteCount = 0;
                int percentCount = 0;
                while ((readBytes = await stream.ReadAsync(buffer, 0, bufferSize, cancellationToken)) != 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    byteCount += readBytes;
                    if (sizeInBytes / 100 * percentCount < byteCount)
                    {
                        OnDownloadProgressChanged?.Invoke(this, new ProgressChangedEventArgs(percentCount++, null));
                    }

                    for (int i = 0; i < readBytes; i++)
                    {
                        result.Add(buffer[i]);
                    }
                }
                image = new Bitmap(new MemoryStream(result.ToArray()));
                logger.Info($"[{Thread.CurrentThread.ManagedThreadId}] Download ends");
                OnDownloadImageCompleted?.Invoke(this, new AsyncCompletedEventArgs(null, false, null));
                return image;

            }
            catch (OperationCanceledException e)
            {
                logger.Info($"[{Thread.CurrentThread.ManagedThreadId}] Download was canceled");
                OnDownloadImageCompleted?.Invoke(this, new AsyncCompletedEventArgs(e, true, null));
            }
            catch (IOException e)
            {
                logger.Error($"[{Thread.CurrentThread.ManagedThreadId}] IOException");
                OnDownloadImageCompleted?.Invoke(this, new AsyncCompletedEventArgs(e, true, null));
            }
            catch (WebException e)
            {
                logger.Error($"[{Thread.CurrentThread.ManagedThreadId}] Wrong URL");
                throw new ArgumentException("Wrong URL", e);
            }
            finally
            {
                stream?.Close();
                wc?.Dispose();
            }
            return image;
        }
    }
}
