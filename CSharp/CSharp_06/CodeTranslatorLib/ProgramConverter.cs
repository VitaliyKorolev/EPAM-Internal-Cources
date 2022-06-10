using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslatorLib
{
    public class ProgramConverter : IConvertible
    {
        public string ConvertToCSharp(string code)
        {
            return "Code has been converted to CSharp";
        }
        public string ConvertToVB(string code)
        {
            return "Code has been converted to VB";
        }
    }
}
