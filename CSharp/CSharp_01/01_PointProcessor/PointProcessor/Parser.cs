using System;
using System.Globalization;

namespace PointProcessor
{
    public static class Parser
    {
        private const int coordinatesNumber = 2;
        private const int xIndex = 0;
        private const int yIndex = 1;
        public static bool TryParsePoint(string line, out Point point)
        {
            var style = NumberStyles.Any;
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            point = null;
            if(line == null)
            {
                return false;
            }
            string[] coordinates = line.Split(',');
            if (coordinates.Length != coordinatesNumber)
            {
                return false;
            }
            if( !decimal.TryParse(coordinates[xIndex], style, culture, out decimal x) || !decimal.TryParse(coordinates[yIndex], style, culture, out decimal y))
            {
                return false;
            }
            point = new Point(x, y);
            return true;
        }
    }
}
