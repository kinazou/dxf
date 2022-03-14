using System;
using System.Collections.Generic;

namespace dxf
{
    public class DXFPolylineEntity : DXFEntity
    {
        public double _polylineFlag { get; private set; }
        public List<DXFVertexEntity> _vertexs { get; private set; }

        public DXFPolylineEntity(CSVRectData csvData)
        {
            _layer = csvData._layer;
            _polylineFlag = 1;
            _vertexs = new List<DXFVertexEntity>();
            // buttom left
            _vertexs.Add(new DXFVertexEntity(_layer, 
                                             csvData._coordX - (csvData._width / 2),
                                             csvData._coordY - (csvData._hegiht / 2)));
            // buttom right
            _vertexs.Add(new DXFVertexEntity(_layer, 
                                             csvData._coordX + (csvData._width / 2),
                                             csvData._coordY - (csvData._hegiht / 2)));
            // top right
            _vertexs.Add(new DXFVertexEntity(_layer, 
                                             csvData._coordX + (csvData._width / 2),
                                             csvData._coordY + (csvData._hegiht / 2)));
            // top left
            _vertexs.Add(new DXFVertexEntity(_layer, 
                                             csvData._coordX - (csvData._width / 2),
                                             csvData._coordY + (csvData._hegiht / 2)));
            // rotate 2D
            foreach (var vertex in _vertexs)
            {
                vertex.Rotate2D(csvData._angle, csvData._coordX, csvData._coordY);
            }
        }

        public override IEnumerable<DXFBlock> GetBlock()
        {
            yield return new DXFBlock( 0, "POLYLINE");
            yield return new DXFBlock( 8, _layer);
            yield return new DXFBlock(66, 1);
            yield return new DXFBlock(70, _polylineFlag);
            foreach (var vertex in _vertexs)
            {
                foreach (var block in vertex.GetBlock())
                {
                    yield return block;
                }
            }
            yield return new DXFBlock( 0, "SEQEND");
            yield return new DXFBlock( 8, _layer);
        }
    }
}
