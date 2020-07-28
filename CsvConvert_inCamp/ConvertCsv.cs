using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace CsvConvert_inCamp
{
    class ConvertCsv
    {
        string NameCsv = null;
        List<Person> personsData = new List<Person>();

        SortedSet<DateTime> dates = new SortedSet<DateTime>();

        public ConvertCsv(string nameCsv)
        {
            this.NameCsv = nameCsv;
        }

        public void ReadingCsv()
        {
            using (var reader = new StreamReader(NameCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Person>();
                csv.Configuration.RegisterClassMap<PersonMap>();
                personsData = records.OrderBy(n => n.Name).ToList();
            }
            GetDate();
        }

        public void WritingToCsv()
        {
            using (var writer = new StreamWriter("result.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                WriteHeader(csv);
            }
        }

        public void WriteHeader(CsvWriter csv)
        {
            csv.WriteField("Date / Name");

            foreach (var d in dates)
            {
                csv.WriteField(d.ToString("yyyy-MM-dd"));
            }
        }

        public void GetDate()
        {
            personsData.ForEach(p => dates.Add(p.Date));
        }

    }
}
