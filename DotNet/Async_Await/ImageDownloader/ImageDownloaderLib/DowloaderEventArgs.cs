using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloaderLib
{
    public class DownloaderEventArgs: EventArgs
    {
        string ContentType { get; init; }
        long ContentLength { get; init; }

        public DownloaderEventArgs(string contentType, long contentLength) : base()
        {
            ContentType = contentType;
            ContentLength = contentLength;
        }
    }
}
