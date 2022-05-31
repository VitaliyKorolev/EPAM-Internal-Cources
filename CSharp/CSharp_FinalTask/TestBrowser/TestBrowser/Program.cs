using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services;

namespace TestResultsBrowser
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Application.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string path1 = @"E:\Netlab_Vitalii\CSharp\CSharp_FinalTask\TestBrowser\StudentDatas.xml";
            string path2 = @"E:\Netlab_Vitalii\CSharp\CSharp_FinalTask\TestBrowser\Filter.xml";
            StudentDataService studentDataService = new StudentDataService(path1);
            FilterService filterService = new FilterService(path2);

            Application.Run(new MainForm(studentDataService, filterService));
        }
    }
}
