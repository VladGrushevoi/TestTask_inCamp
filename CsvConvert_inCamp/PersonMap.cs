using System;
using System.Collections.Generic;
using System.Text;

namespace CsvConvert_inCamp
{
    class PersonMap: CsvHelper.Configuration.ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.Name).Name("Employee Name");
            Map(m => m.Date).Name("Date");
            Map(m => m.WorkHours).Name("Work Hours");
        }
    }
}
