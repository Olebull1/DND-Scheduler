using System;
using System.Collections.Generic;


namespace DND_Scheduler.Common                                                                                 
{                                                                                                              
    class Date                                                                                                 
    {                                                                                                          
        public int Month { get; set; }                                                                         
        public int Day { get; set; }                                                                           
        public int Year {get; set;}
        public bool[] Minutes { get; set; }

        public Date()                                                                                          
        {
            Minutes = new bool[1440];                                                                          
        }                                                                                                      
        public int BlockMinutes(int startIndex, int endIndex)                                                  
        {                                                                                                
            if (startIndex < 0 || endIndex > 1439)                                                       
                return -1;                                                                               
            for (int i = startIndex; i <= endIndex; i++)                                                 
            {
                Minutes[i] = true;                                                                       
            }                                                                                            
            return 1;                                                                                    
        }                                                                                                
                                                                                                         
        //private string getMinutesString()
        //{
        //    string minutearr = "";
        //    foreach (bool minute in minutes)
        //    {
        //        minutearr += minute.ToString();
        //    }
        //    Console.WriteLine(minutearr);
        //    return minutearr;
        //}
    }
}
