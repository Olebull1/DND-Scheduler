using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DND_Scheduler.Common
{
    class FileMgr
    {
        static public void DateToJson(Date d)
        {
            string filename = "./Dates/" + d.Month.ToString() + d.Day.ToString() + d.Year.ToString() + ".json";
            string jsonString = JsonSerializer.Serialize(d);
            File.WriteAllText(filename, jsonString);
        }
        static public Date JsonToDate(string filename)
        {
            string fileName = filename;
            string jsonString = File.ReadAllText(fileName);
            Date jsonDate= JsonSerializer.Deserialize<Date>(jsonString);
            return jsonDate;
        }
    }
}
