using BinarySearchTreeLib;
using FilterLib;
using Services;
using System;
using System.Collections.Generic;

namespace Managers
{
    public class FilterManager
    {
        private FilterService filterService;
        public FilterManager(FilterService filterService)
        {
            this.filterService = filterService;
        }

        public Filter<StudentData> GetFilter()
        {
            return filterService.GetFilter();
        }
        public void ResetFilter()
        {
            filterService.ResetFilter();
        }
        public void SaveFilter()
        {
            filterService.SaveFilter();
        }
        public void AddCondition(string propertyName, FilterMethods filterMethod, string property1, string property2 = "")
        {
            filterService.AddCondition(propertyName, filterMethod, property1, property2);
        }
    }
}
