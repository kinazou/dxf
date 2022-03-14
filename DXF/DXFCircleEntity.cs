using System;
using System.Collections.Generic;

namespace dxf
{
    public class DXFCircleEntity : DXFEntity
    {
        public double _coordX { get; protected set; }
        public double _coordY { get; protected set; }
        public double _coordZ { get; protected set; }
        public double _radius { get; private set; }

        public DXFCircleEntity(CSVCircleData csvData)
        {
            _layer = csvData._layer;
            _coordX = csvData._coordX;
            _coordY = csvData._coordY;
            _coordZ = 0;
            _radius = csvData._diameter / 2;
        }

        public override IEnumerable<DXFBlock> GetBlock()
        {
            yield return new DXFBlock( 0, "CIRCLE");
            yield return new DXFBlock( 8, _layer);
            yield return new DXFBlock(10, _coordX);
            yield return new DXFBlock(20, _coordY);
            yield return new DXFBlock(30, _coordZ);
            yield return new DXFBlock(40, _radius);
        }
    }
}
