using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerLib
{
    public interface ICountDownNotifier
    {
        public void Init();
        public void Run();
        public void Unsubscribe();
    }
}
