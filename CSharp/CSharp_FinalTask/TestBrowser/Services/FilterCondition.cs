using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [Serializable]
    public struct FilterCondition
    {
        public string Property1;
        public string Property2;
        public string PropertyName;
        public FilterMethods FilterMethod;
        public FilterCondition(string propertyName, FilterMethods filterMethod, string property1, string property2)
        {
            PropertyName = propertyName;
            FilterMethod = filterMethod;
            Property1 = property1;
            Property2 = property2;
        }
    }
}
