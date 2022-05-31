using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManagerLib
{
    public class DownloadStartedEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string Message { get; set; }
        public DownloadStartedEventArgs(string fileName, long fileSize, string message) : base()
        {
            FileName = fileName;
            FileSize = fileSize;
            Message = message;
        }
    }
}
