using System;

namespace dxf
{
    /// <summary>
    /// Write object to the stream as DXF
    /// </summary>
    public interface IDXFWriter
    {
        /// <summary>
        /// Write a line
        /// </summary>
        /// <param name="record"></param>
        void WriteLine(DXFBlock block);
    }
}
