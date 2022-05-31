using BinarySearchTreeLib;
using FilterLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Services
{
    public class FilterService
    {
        private string pathToFile;
        private List<FilterCondition> filterConditions;
        public FilterService(string pathToFile)
        {
            this.pathToFile = pathToFile;
            try
            {
                var xmlreader = new XmlSerializer(typeof(List<FilterCondition>));
                using (FileStream fs = new FileStream(pathToFile, FileMode.Open))
                {
                    filterConditions = (List<FilterCondition>)xmlreader.Deserialize(fs);
                }
            }
            catch
            {
                filterConditions = new();
            }
        }
        public void AddCondition(string propertyName, FilterMethods filterMethod, string property1, string property2 = "")
        {
            filterConditions.Add(new FilterCondition(propertyName, filterMethod, property1, property2));
        }
        public Filter<StudentData> GetFilter()
        {
            Filter<StudentData> filter = new();
            foreach (var con in filterConditions)
            {
                Type typeOfStudentData = typeof(StudentData);
                Type typeOfProperty = typeOfStudentData.GetProperty(con.PropertyName).PropertyType;

                if (typeOfProperty == typeof(DateTime))
                {
                    filter.PropertyInRange<DateTime>(con.PropertyName, DateTime.Parse(con.Property1), DateTime.Parse(con.Property2));
                }
                if (typeOfProperty == typeof(int))
                {
                    filter.PropertyInRange<int>(con.PropertyName, int.Parse(con.Property1), int.Parse(con.Property2));
                }
                if (typeOfProperty == typeof(string))
                {
                    filter.PropertyEqualsToValue<string>(con.PropertyName, con.Property1);
                }
            }
            return filter;
        }
        public void ResetFilter()
        {
            filterConditions.Clear();
        }
        public void SaveFilter()
        {
            var xmlwriter = new XmlSerializer(typeof(List<FilterCondition>));
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                xmlwriter.Serialize(fs, filterConditions);
            }
        }
    }
}
