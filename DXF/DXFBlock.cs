using System;

namespace dxf
{
    public class DXFBlock
    {
        public int _key { get; protected set; }

        public object _value { get; protected set; }

        public DXFBlock(int key, object value)
        {
            _key = key;
            _value = value;
        }
    }
}
