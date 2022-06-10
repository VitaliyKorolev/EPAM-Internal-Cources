using System;
using System.Collections;
using System.IO;
using System.Text;

namespace FileProcessorLib
{
    public class FileProcessor : IDisposable, IEnumerable
    {
        private byte[] buffer;
        private FileStream fstream;
        private StreamReader reader;
        private StreamWriter writer;
        private readonly Encoding ANSI;
        private readonly Encoding Unicode = Encoding.Unicode;
        private bool isDisposed = false;
        public int Length { get; private set; }

        private FileProcessor(string path, int fileLength)
        {
            if (fileLength < 0)
            {
                throw new ArgumentOutOfRangeException("File length must be positive");
            }
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ANSI = Encoding.GetEncoding(1251);
            OpenStream(path, fileLength);

            Length = fileLength;
            buffer = new byte[fileLength];
            writer.Write(new string(' ', fileLength));
            writer.Flush();
            fstream.Read(buffer, 0, fileLength);
        }

        private FileProcessor(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ANSI = Encoding.GetEncoding(1251);
            OpenStream(path);

            Length = (int)fstream.Length;
            buffer = new byte[Length];
            fstream.Read(buffer, 0, Length);
        }

        public static FileProcessor Create(string path, int fileLength)
        {
            return new FileProcessor(path, fileLength);
        }

        public static FileProcessor Read(string path)
        {
            return new FileProcessor(path);
        }

        public char this[int index]
        {
            get
            {
                if (index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }
                if (isDisposed)
                {
                    throw new ObjectDisposedException(this.ToString());
                }
                fstream.Seek(index, SeekOrigin.Begin);
                char result = (char)reader.Read();
                return result;
            }
            set
            {
                if (index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }
                if (isDisposed)
                {
                    throw new ObjectDisposedException(this.ToString());
                }

                char[] unicodeChar = { value };
                byte[] unicodeByte = Unicode.GetBytes(unicodeChar);
                byte[] ansiByte = Encoding.Convert(Unicode, ANSI, unicodeByte);
                buffer[index] = ansiByte[0];

                fstream.Seek(index, SeekOrigin.Begin);
                writer.Write(value);
                writer.Flush();
            }
        }

        public void Write(string text)
        {
            if (text.Length > Length)
                throw new ArgumentOutOfRangeException("Lenght of text greater than array length");
            for(int i = 0; i < text.Length; i++)
            {
                this[i] = text[i];
            }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                reader?.Dispose();
                writer?.Dispose();
                fstream?.Dispose();
                isDisposed = true;
            }
        }

        private void OpenStream(string path, int fileLength = -1)
        {
            fstream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            reader = new StreamReader(fstream, ANSI, false, fileLength, true);
            writer = new StreamWriter(fstream, ANSI, fileLength, true);
        }

        public IEnumerator GetEnumerator()
        {
            byte[] ansiByte;
            byte[] unicodeByte;
            for (int i = 0; i < Length; i++)
            {
                ansiByte = new byte[] { buffer[i] };
                unicodeByte = Encoding.Convert(ANSI, Unicode, ansiByte);
                yield return Unicode.GetString(unicodeByte)[0];
            }
        }
    }
}