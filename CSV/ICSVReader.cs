using System.Collections.Generic;

namespace dxf
{
    /// <summary>
    /// Read object to the stream as CSV
    /// </summary>
    public interface ICSVReader
    {
        /// <summary>
        /// Read a line
        /// </summary>
        IEnumerable<string> ReadLine();

        bool EndOfStream { get; }
    }
}
