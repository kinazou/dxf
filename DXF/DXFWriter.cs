using System;
using System.IO;
using System.Text;

namespace dxf
{
    public class DXFWriter : IDXFWriter, IDisposable
    {
        private readonly StreamWriter _writer;

        public DXFWriter(Stream stream, Encoding encoding)
        {
            _writer = new StreamWriter(stream, encoding);
        }

        public void WriteLine(DXFBlock record)
        {
            _writer.WriteLine(Format(record));
        }

        public void Flush()
        {
            _writer.Flush();
        }

        public void Close()
        {
            _writer.Close();
        }

        private string Format(DXFBlock src)
        {
            string ssrc = "";
            ssrc += src._key.ToString().PadLeft(3, ' ');
            ssrc += Environment.NewLine;
            ssrc += src._value.ToString();
            return ssrc;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _writer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CSVWriter() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
