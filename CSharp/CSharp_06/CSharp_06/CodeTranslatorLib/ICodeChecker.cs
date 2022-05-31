using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslatorLib
{
    public interface ICodeChecker
    {
        bool CheckCodeSyntax(string code, string language);
    }
}
