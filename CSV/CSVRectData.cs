using System;
using System.Linq;
using System.Collections.Generic;

namespace dxf
{
    public class CSVRectData : CSVData
    {
        public double _width { get; private set; }
        public double _hegiht { get; private set; }

        public CSVRectData(IEnumerable<string> csvBeans)
        {
            _layer = csvBeans.ElementAt(0);
            _coordX = double.Parse(csvBeans.ElementAt(1));
            _coordY = double.Parse(csvBeans.ElementAt(2));
            _angle = double.Parse(csvBeans.ElementAt(3));
            _shape = csvBeans.ElementAt(4);
            _width = double.Parse(csvBeans.ElementAt(5));
            _hegiht = double.Parse(csvBeans.ElementAt(6));
        }

        public override string Print()
        {
            return _layer;
        }
    }
}
