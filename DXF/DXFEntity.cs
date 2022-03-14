using System;
using System.Collections.Generic;

namespace dxf
{
    public abstract class DXFEntity : IDXFData
    {
        public string _layer { get; protected set; }

        protected DXFEntity() {}

        public abstract IEnumerable<DXFBlock> GetBlock();

        public static DXFEntity ConvertFrom(CSVData csvData)
        {
            if (String.Compare(csvData._shape, "circle") == 0)
            {
                return new DXFCircleEntity((CSVCircleData)csvData);
            }
            else if (String.Compare(csvData._shape, "rect") == 0)
            {
                return new DXFPolylineEntity((CSVRectData)csvData);
            }
            Console.WriteLine("Wrong input csvData. :{0}", csvData.Print());
            return null;
        }
    }
}
