using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace dxf
{
    class Converter
    {
        private string _inputFilePath;
        private string _outputFilePath;
        private List<CSVData> _csvDatas;
        private DXFData _dxfData;

        public Converter(string inputFilePath, string outputFilePath)
        {
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
            _csvDatas = new List<CSVData>();
            _dxfData = new DXFData();
        }

        public bool IsWrongParam()
        {
            bool isWrong = true;

            if (!File.Exists(_inputFilePath))
            {
                Console.WriteLine("Wrong input file path.:{0}", _inputFilePath);
                return isWrong;
            }
            string outputDir = Path.GetDirectoryName(_outputFilePath);
            if (!Directory.Exists(outputDir))
            {
                Console.WriteLine("Wrong output directory path. :{0}", outputDir);
                return isWrong;
            }

            isWrong = false;
            return isWrong;
        }

        public bool ReadCSV()
        {
            bool isSuccess = false;
            try
            {
                using (var stream = new FileStream(_inputFilePath, FileMode.Open, FileAccess.Read))
                using (var reader = new CSVReader(stream, Encoding.UTF8))
                {
                    while (!reader.EndOfStream)
                    {
                        var data = CSVData.Create(reader.ReadLine());
                        if (data != null) _csvDatas.Add(data);
                    }
                }
                isSuccess = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("exception. :{0}", ex);
            }
            return isSuccess;
        }

        public bool ConvertCSVtoDXF()
        {
            bool isSuccess = true;
            foreach (var csvData in _csvDatas)
            {
                var dxfEntity = DXFEntity.ConvertFrom(csvData);
                if (dxfEntity == null)
                {
                    isSuccess = false;
                    Console.WriteLine("Wrong convert CSV to DXF data. :{0}", csvData.Print());
                }
                _dxfData.Add(dxfEntity);
            }
            return isSuccess;
        }

        public bool WriteDXF()
        {
            bool isSuccess = false;
            try
            {
                SafeCreateDirectory(_outputFilePath);

                using (var stream = File.Create(_outputFilePath))
                using (var writer = new DXFWriter(stream, Encoding.ASCII))
                {
                    foreach (var block in _dxfData.GetBlock())
                    {
                        writer.WriteLine(block);
                    }
                    writer.Close();
                }
                isSuccess = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("exception. :{0}", ex);
            }
            return isSuccess;
        }

        private DirectoryInfo SafeCreateDirectory(string path)
        {
            string dirPath = Path.GetDirectoryName(path);
            if (Directory.Exists(dirPath))
            {
                return null;
            }
            return Directory.CreateDirectory(dirPath);
        }
    }
}
