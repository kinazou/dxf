using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace dxf
{
    /// <summary>
    /// generic CSV reader
    /// </summary>
    public class CSVReader : ICSVReader, IDisposable
    {
        private static readonly string END_OF_FILE = "\x1a";
        private static readonly string DELIMITER = "\"";
        private readonly StreamReader _reader;

        public CSVReader(Stream inputStream, Encoding encoding)
        {
            _reader = new StreamReader(inputStream, encoding);
        }

        public IEnumerable<string> ReadLine()
        {
            var line = ReadSemanticLine();
            if (line == null)
            {
                return null;
            }
            line = TrimRight(line);
            line += ",";
            var matches = Regex.Matches(line, "(\"[^\"]*(?:\"\"[^\"]*)*\"|[^,]*),");
            return matches.Cast<Match>().Select(x => Dequote(x));
        }

        public bool EndOfStream
        {
            get
            {
                return _reader.EndOfStream;
            }
        }

        private String TrimRight(string src)
        {
            return Regex.Replace(src, "(?:\x0D\x0A|[\x0D\x0A])?$", "", RegexOptions.Singleline);
        }

        private string Dequote(Match match)
        {
            var s = match.Groups[1].Value;
            var quoted = Regex.Match(s, "^\"(.*)\"$", RegexOptions.Singleline);
            if (quoted.Success)
            {
                return quoted.Groups[1].Value.Replace("\"\"", "\"");
            }
            else
            {
                return s;
            }
        }

        private string ReadSemanticLine()
        {
            if (_reader.EndOfStream)
            {
                return null;
            }
            var line = _reader.ReadLine();
            if (line == null | line == END_OF_FILE || line.Length == 0)
            {
                return null;
            }
            while (!HasEnoughQuote(line) && !_reader.EndOfStream)
            {
                // Complete missing line break.
                line += "\n" + _reader.ReadLine();
            }
            return line;
        }

        private bool HasEnoughQuote(string line)
        {
            return (Regex.Matches(line, DELIMITER).Count % 2) == 0;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_reader != null)
                    {
                        _reader.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CSVReader() {
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
