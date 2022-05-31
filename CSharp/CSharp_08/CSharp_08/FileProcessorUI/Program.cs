using System;
using FileProcessorLib;

namespace FileProcessorUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\Netlab_Vitalii\CSharp\CSharp_08\CSharp_08\text.txt";
            FileProcessor fr = null;
            try
            {
                fr = FileProcessor.Create(path, 16);
                fr.Write("[01] Привет мир!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(fr != null)
                {
                    fr.Dispose();
                }
            }

            Write(path);

            using (FileProcessor fr1 = FileProcessor.Read(path))
            {
                fr1[2] = '2';
            }

            Write(path);
        }
        private static void Write(string path)
        {
            using (FileProcessor fr1 = FileProcessor.Read(path))
            {
                foreach (var letter in fr1)
                {
                    Console.Write(letter);
                }
            }
        }
    }
}
