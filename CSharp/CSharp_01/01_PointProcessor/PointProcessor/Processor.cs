using System;
using System.IO;

namespace PointProcessor
{
	public class Processor
    {
        public static void ProcessFiles(string[] filenames)
        {
            foreach (string file in filenames)
            {
                using (TextReader tr = new StreamReader(file))
                {
                    Reader(tr);
                }
            }
        }
        public static void ProcessConsole()
        {
            using (TextReader tr = Console.In)
            {
                Reader(tr);
            }
        }
        private static void Reader(TextReader textReader)
        {
            string input;
            while ((input = textReader.ReadLine()) != null)
            {
                Console.WriteLine(ProcessLine(input));
            }
        }
        public static string ProcessLine(string line)
        {
            Point point;
            Parser.TryParsePoint(line, out point);
            string result = Formatter.Format(point);
            return result;
        }
    }
}
