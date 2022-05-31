using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SharedResLib;

namespace DownloadManagerLib
{
    public delegate void DownloadStartedEventHandler(object sender, DownloadStartedEventArgs e);
    public delegate void DownloadProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
    public class DownloadManager
    {
        public event DownloadStartedEventHandler OnDownloadStarted;
        public event DownloadProgressChangedEventHandler OnDownloadProgressChanged;
        public event AsyncCompletedEventHandler OnDownloadCompleted;

        private const long defaultChunkSize = 512 * 1024;
        private string targetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

        private int parallelTasksNumber;

        public DownloadManager(int parallelTasksNumber)
        {
            this.parallelTasksNumber = parallelTasksNumber;
        }

        public string GetFileNameFromUrl(Uri uri)
        {
            return uri.LocalPath.Substring(uri.LocalPath.LastIndexOf('/') + 1);
        }
        public async Task DownloadFileAsync(Uri url, string fileName = "")
        {
            SemaphoreSlim semaphore = new SemaphoreSlim(parallelTasksNumber);
            if (fileName == "")
                fileName = GetFileNameFromUrl(url);

            if (File.Exists(Path.Combine(targetPath, fileName)))
            {
                OnDownloadStarted?.Invoke(this, new DownloadStartedEventArgs(fileName, 0, $"File already exists {fileName}"));
                return;
            }
            try
            {
                Progress progress = new Progress() { Percents = 0, TotalBytes = 0, BytesDowloaded = 0 };
                long size = await GetSizeAsync(url);
                progress.TotalBytes = size;
                OnDownloadStarted?.Invoke(this, new DownloadStartedEventArgs(fileName, size, "Download started succesfully"));

                (int chunkCount, long lastChunkSize, long chunkSize) = GetChunkCount(size);
                long offset = 0;

                using var targetStream = new FileStream(Path.Combine(targetPath, fileName), FileMode.Create, FileAccess.Write, FileShare.Write, 4096, true);
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < chunkCount; i++)
                {
                    if (i == chunkCount - 1)
                    {
                        chunkSize = lastChunkSize;
                    }

                    tasks.Add(ReadChunkAsync(offset, chunkSize, url, targetStream, progress, semaphore));
                    offset += chunkSize;
                }

                await Task.WhenAll(tasks);
                OnDownloadCompleted?.Invoke(this, new AsyncCompletedEventArgs(null, false, null));
            }
            catch(OperationCanceledException e)
            {
                OnDownloadCompleted?.Invoke(this, new AsyncCompletedEventArgs(e, true, null));
            }
            catch (Exception e)
            {
                OnDownloadCompleted?.Invoke(this,  new AsyncCompletedEventArgs(e, false, null));
            }
            finally
            {
                semaphore.Release(2);
                semaphore.Dispose();
            }
        }
        private async Task ReadChunkAsync(long offset, long chunkSize, Uri uri, Stream targetStream, Progress progress, SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.AddRange(offset, chunkSize + offset - 1);
            WebResponse response = await request.GetResponseAsync();
            using (var responseStream = response.GetResponseStream())
            {
                
                var bytesInStream = new byte[chunkSize];
                int read;
                int totalReadInTask = 0;
                do
                {
                    read = await responseStream.ReadAsync(bytesInStream, 0, (int)bytesInStream.Length);

                    if (read > 0)
                    {
                        //lock (targetStream)
                        //{
                        targetStream.Seek(offset + totalReadInTask, SeekOrigin.Begin);
                        await targetStream.WriteAsync(bytesInStream, 0, read);
                        //}
                        lock (progress)
                        {
                            progress.BytesDowloaded += read;
                            if (progress.BytesDowloaded >= progress.TotalBytes / 100 * (progress.Percents + 1) )
                            {
                                progress.Percents = (int)(100 * progress.BytesDowloaded / progress.TotalBytes);
                                OnDownloadProgressChanged?.Invoke(this, new ProgressChangedEventArgs(progress.Percents, null));
                            }
                        }
                        
                    }
                    totalReadInTask += read;
                }
                while (read > 0);
            }
            semaphore.Release();
        }
        private async Task<long> GetSizeAsync(Uri url)
        {
            WebRequest req = WebRequest.Create(url);
            req.Method = "HEAD";
            WebResponse resp = await req.GetResponseAsync();
            var res = resp.ContentLength;
            resp.Close();
            return res;
        }

        private (int chunkCount, long lastChunkSize, long chunkSize) GetChunkCount(long size)
        {
            long chunkSize = defaultChunkSize;
            int chunkCount = Convert.ToInt32(size / chunkSize);

            if (chunkCount > 1000)
            {
                chunkSize = defaultChunkSize * 2;
                chunkCount = Convert.ToInt32(size / chunkSize);
            }

            if (chunkCount > 1000)
            {
                chunkSize = defaultChunkSize * 4;
                chunkCount = Convert.ToInt32(size / chunkSize);
            }


            long lastChunkSize = Convert.ToInt32(size % chunkSize);
            if (lastChunkSize > 0) chunkCount++;

            return (chunkCount, lastChunkSize, chunkSize);
        }
    }
}
