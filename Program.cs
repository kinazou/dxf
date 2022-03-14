using System;

namespace dxf
{
    class Program
    {
        static void Main(string[] args)
        {
            // get input param.
            if (args.Length != 2)
            {
                Console.WriteLine("Wrong number of arguments. args.Length:{0}", args.Length);
                return;
            }
            string inputFilePath = args[0];
            string outputFilePath = args[1];
            var converter = new Converter(inputFilePath, outputFilePath);

            // check input param.
            if (converter.IsWrongParam())
            {
                return;
            }
            Console.WriteLine("InputFilePath:{0}, OutputFilePath:{1}", inputFilePath, outputFilePath);

            // read csv.
            if (!converter.ReadCSV())
            {
                Console.WriteLine("Wrong read CSV file.");
                return;
            }
            Console.WriteLine("Finished read CSV file. :{0}", inputFilePath);

            // convert csv to dxf.
            converter.ConvertCSVtoDXF();

            // print dxf entity.
            if (!converter.WriteDXF())
            {
                Console.WriteLine("Wrong convert DXF file.");
                return;
            }
            Console.WriteLine("Finished convert DXF file. :{0}", outputFilePath);
        }
    }
}
