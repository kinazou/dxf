using System;
using System.Linq;
using System.Collections.Generic;

namespace dxf
{
    public abstract class CSVData
    {
        public string _layer { get; protected set; }
        public double _coordX { get; protected set; }
        public double _coordY { get; protected set; }
        public double _angle { get; protected set; }
        public string _shape { get; protected set; }

        protected CSVData() {}

        public static CSVData Create(IEnumerable<string> csvBeans)
        {
            string shape = csvBeans.ElementAt(4);
            if ((csvBeans.Count() == 6) && (String.Compare(shape, "circle", true) == 0))
            {
                return new CSVCircleData(csvBeans);
            }
            else if ((csvBeans.Count() == 7) && (String.Compare(shape, "rect", true) == 0))
            {
                return new CSVRectData(csvBeans);
            }
            string log = "";
            bool isTopColumn = true;
            foreach (var column in csvBeans)
            {
                if (!isTopColumn) log += ", ";
                isTopColumn = false;
                log += column;
            }
            Console.WriteLine("Wrong csvData row. :{0}", log);
            return null;
        }

        public abstract string Print();
    }
}
