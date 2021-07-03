using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;



namespace DND_Scheduler.Common
{
    class Calendar
    {
        private const int EOD = 1439;
        private const int BOD = 0;
        private string Dir = "./Dates/";
        List<string> dates = new List<string> { };
        private static Calendar instance = null;
        public enum befafter
        {
            BEFORE,
            AFTER
        }
        public static Calendar Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Calendar();
                }
                return instance;
            }
        }
        private Calendar()
        {
            dates = GetDatesFromDir();
        }

        private List<string> GetDatesFromDir()
        {
            string[] fileEntries = Directory.GetFiles(Dir);
            List<string> import = new List<string>();
             foreach(string file in fileEntries)
            {
                Console.WriteLine(file);
                string fuck = file.Split('/')[2];
                import.Add(fuck.Split('.')[0]);
            }
            Console.WriteLine(import.ToString());
            return import;
        }

        public bool DoesDateExist(string d) 
        {
            bool retval = false;
            Console.WriteLine(d);
            foreach(string date in dates)
            {
                if (date == d)
                {
                    retval = true;
                    break;
                }
            }
            return retval;
        }

        public int BlockDay(string day)
        {
            //DayOfWeek? dayOfWeek = GetEnumFromString(day);
            //Console.WriteLine(dayOfWeek);
            //if (dayOfWeek == null)
            //    return -1;

            //DateTime dateOfDay = GetNextWeekday((DayOfWeek)dayOfWeek);
            DateTime? dateOfDay = GetDateTimeFromDay(day);
            Console.WriteLine(dateOfDay);
            //string dString = dateOfDay.ToString("MMddyyyy");
            Date dayToBlock = new Date();
            dayToBlock.Day = dateOfDay.Value.Day;
            dayToBlock.Month = dateOfDay.Value.Month;
            dayToBlock.Year = dateOfDay.Value.Year;
            dayToBlock.BlockMinutes(0, EOD);
            FileMgr.DateToJson(dayToBlock);
            return 0;
        }
        public int BlockBeforeAfter(string date, string time, befafter beaf)
        {
            Console.WriteLine("top of function");
            Regex rx = new Regex(@"^[0-9]*$", RegexOptions.Compiled);
            // Find matches.
            DateTime? dateTime;
            string dateTimeString = "";
            if (!rx.IsMatch(date))
            {
                Console.WriteLine("received a day " + date);
                dateTime = GetDateTimeFromDay(date);
                if (dateTime != null)
                {
                    dateTimeString = GetStringDateFromDateTime(dateTime);
                }
                else
                {
                    Console.WriteLine("dateString null");
                    return -1;
                }
                //nope
            }
            else
            {
                if (ValidateDateString(date))
                {
                    dateTimeString = date;
                    dateTime = GetDateTimeFromString(date);
                }
                else
                {
                    Console.WriteLine("invalid date format");
                    return -1;
                }

            }
            Console.WriteLine("middle of function");
            //TODO BRANDON
            int minute = ParseMinuteFromTime(time);
            Date importedDate;
            Console.WriteLine("middle 2");
            if (DoesDateExist(dateTimeString)) //this checks to see if the string date exists in the list from the file dir
            {
                importedDate = FileMgr.JsonToDate(dateTimeString);
            }
            else
            {
                importedDate = new Date();
                importedDate.Month = dateTime.Value.Month;
                importedDate.Day = dateTime.Value.Day;
                importedDate.Year = dateTime.Value.Year;
            }
            if(beaf == befafter.BEFORE)
            {
                importedDate.BlockMinutes(BOD, minute);
            }
            else
            {
                importedDate.BlockMinutes(minute, EOD);
            }
            
            FileMgr.DateToJson(importedDate);
            Console.WriteLine("blockafter end");
            return 1;
        }

        private int ParseMinuteFromTime(string time)
        {
            return 720;
        }
        public string GetStringDateFromDateTime(DateTime? dt)
        {
            string dateString = dt.Value.ToString("MMddyyyy");
            return dateString;
        }
        public static DateTime? GetDateTimeFromString(string date)
        {
            DateTime? dt;
            try {
                dt = DateTime.ParseExact(date, "MMddyyyy", null);
            } catch (Exception e)
            {
                Console.WriteLine("Yooooooo dis bitch empty YEEEET (but actually its not empty it just didnt parse)" + e.ToString());
                dt = null;
            }
            return dt;
        }

        private bool ValidateDateString(string date)
        {
            int month, day, year;
            bool rv = int.TryParse(date.Substring(0,2), out month);
            if(rv)
            {
                rv = int.TryParse(date.Substring(2, 2), out day);
                if (rv)
                {
                    rv = int.TryParse(date.Substring(4, 4), out year);
                    if(rv)
                    {
                        if ((month < 0 || month > 12) || (day < 0 || day > 31)  || (year < DateTime.Now.Year))
                        {
                            rv = false;
                        }
                    }
                }
            }
            return rv;

        }
        private DateTime? GetDateTimeFromDay(string day)
        {

            DayOfWeek? dayOfWeek = GetEnumFromString(day);
            if (dayOfWeek == null)
                return null;

            DateTime dateOfDay = GetNextWeekday((DayOfWeek)dayOfWeek);
            Console.WriteLine(dateOfDay);         
            return dateOfDay;
        }
        public static DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine(start);
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public DayOfWeek? GetEnumFromString(string day)
        {
            DayOfWeek? d;
            switch (day.ToLower())
            {
                case "monday":
                    d = DayOfWeek.Monday;
                    break;
                case "tuesday":
                    d = DayOfWeek.Tuesday;
                    break;
                case "wednesday":
                    d = DayOfWeek.Wednesday;
                    break;
                case "thursday":
                    d =  DayOfWeek.Thursday;
                    break;
                case "friday":
                    d = DayOfWeek.Friday;
                    break;
                case "saturday":
                    d = DayOfWeek.Monday;
                    break;
                case "sunday":
                    d = DayOfWeek.Monday;
                    break;
                default:
                    d = null;
                    break;
            }
            return d;

        }
    }
}
