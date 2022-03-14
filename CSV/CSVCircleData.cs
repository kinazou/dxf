using System;
using System.Linq;
using System.Collections.Generic;

namespace dxf
{
    public class CSVCircleData : CSVData
    {
        public double _diameter { get; private set; }

        public CSVCircleData(IEnumerable<string> csvBeans)
        {
            _layer = csvBeans.ElementAt(0);
            _coordX = double.Parse(csvBeans.ElementAt(1));
            _coordY = double.Parse(csvBeans.ElementAt(2));
            _angle = double.Parse(csvBeans.ElementAt(3));
            _shape = csvBeans.ElementAt(4);
            _diameter = double.Parse(csvBeans.ElementAt(5));
        }

        public override string Print()
        {
            return _layer;
        }
    }
}
