using CodeTranslatorLib;
using System;

namespace CodeTranslatorUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = "This is VB code";
            string languageVB = "VB";
            CodeTranslatorLib.IConvertible[] convertibles =
            {
                new ProgramConverter(),
                new ProgramHelper(),
                new ProgramHelper(),
            };
            foreach (var convertible in convertibles)
            {
                if(convertible is ICodeChecker)
                {
                    ICodeChecker codeChecker = (ICodeChecker)convertible;
                    if(codeChecker.CheckCodeSyntax(code, languageVB))
                    {
                        Console.WriteLine(convertible.ConvertToCSharp(code));
                    }
                    else
                    {
                        Console.WriteLine(convertible.ConvertToVB(code));
                    }
                }
                else
                {
                    Console.WriteLine(convertible.ConvertToCSharp(code));
                    Console.WriteLine(convertible.ConvertToVB(code));
                }
                Console.WriteLine();
            }
        }
    }
}
