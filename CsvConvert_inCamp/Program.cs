using System;

namespace CsvConvert_inCamp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertCsv c = new ConvertCsv("acme_worksheet.csv");
            c.ReadingCsv();
            c.WritingToCsv();
        }
    }
}
