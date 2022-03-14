using System;
using System.Collections.Generic;

namespace dxf
{
    /// <summary>
    /// Write object to the stream as DXF
    /// </summary>
    public interface IDXFData
    {
        /// <summary>
        /// Get a pair value
        /// </summary>
        IEnumerable<DXFBlock> GetBlock();
    }
}
