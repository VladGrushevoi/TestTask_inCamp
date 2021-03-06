﻿using System;
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

        SortedSet<string> uniquePerson = new SortedSet<string>();
        SortedSet<DateTime> dates = new SortedSet<DateTime>();
        List<Person> tempPerson = new List<Person>();

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
        }

        public void WritingToCsv()
        {
            using (var writer = new StreamWriter("result.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                GetUniqueDate();
                WriteHeader(csv);
                WritePerson(csv);
            }
        }

        public void WriteHeader(CsvWriter csv)
        {
            csv.WriteField("Date / Name");

            foreach (var d in dates)
            {
                csv.WriteField(d.ToString("yyyy-MM-dd"));
            }
            csv.NextRecord();
        }

        public void WritePerson(CsvWriter csv)
        {
            foreach (var p in personsData)
            {
                if (!uniquePerson.Contains(p.Name))
                {
                    uniquePerson.Add(p.Name);
                    tempPerson = personsData.FindAll(tp => tp.Name.Equals(p.Name)); 
                    csv.WriteField(tempPerson[0].Name);
                    FormHours(tempPerson, csv);
                    tempPerson.Clear();
                    csv.NextRecord();
                }
            }
        }

        public void FormHours(List<Person> per, CsvWriter csv)
        {
            bool flag = true;
            foreach (var d in dates)
            {
                foreach (var p in per)
                {
                    if (d == p.Date)
                    {
                        csv.WriteField(p.WorkHours);
                        flag = false;
                        break;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    csv.WriteField("0");
                }
            }
        }

        public void GetUniqueDate()
        {
            personsData.ForEach(p => dates.Add(p.Date));
        }

    }
}
