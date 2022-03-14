using System;
using System.Collections.Generic;

namespace dxf
{
    public class DXFData : IDXFData
    {
        private List<IDXFData> _entities;

        public DXFData()
        {
            _entities = new List<IDXFData>();
        }

        public void Add(IDXFData entity)
        {
            _entities.Add(entity);
        }

        public virtual IEnumerable<DXFBlock> GetBlock()
        {
            yield return new DXFBlock(0, "SECTION");
            yield return new DXFBlock(2, "ENTITIES");
            foreach (var entity in _entities)
            {
                foreach (var block in entity.GetBlock())
                {
                    yield return block;
                }
            }
            yield return new DXFBlock(0, "ENDSEC");
            yield return new DXFBlock(0, "EOF");
        }
    }
}
