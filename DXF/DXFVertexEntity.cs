using System;
using System.Collections.Generic;

namespace dxf
{
    public class DXFVertexEntity : DXFEntity
    {
        public double _coordX { get; protected set; }
        public double _coordY { get; protected set; }
        public double _coordZ { get; protected set; }
        public double _vertexFlag { get; protected set; }

        public DXFVertexEntity(string layer, double coordX, double coordY)
        {
            _layer = layer;
            _coordX = coordX;
            _coordY = coordY;
            _coordZ = 0;
            _vertexFlag = 32;
        }

        public void Rotate2D(double angle, double centerX, double centerY)
        {
            double a = Math.PI * angle / 180.0;
            double posX = (_coordX - centerX) * Math.Cos(a) - (_coordY - centerY) * Math.Sin(a);
            double posY = (_coordX - centerX) * Math.Sin(a) + (_coordY - centerY) * Math.Cos(a);
            _coordX = posX + centerX;
            _coordY = posY + centerY;
        }

        public override IEnumerable<DXFBlock> GetBlock()
        {
            yield return new DXFBlock( 0, "VERTEX");
            yield return new DXFBlock( 8, _layer);
            yield return new DXFBlock(10, _coordX);
            yield return new DXFBlock(20, _coordY);
            yield return new DXFBlock(30, _coordZ);
            yield return new DXFBlock(70, _vertexFlag);
        }
    }
}
