using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
//using System.Text;

namespace DND_Scheduler.Common
{
    class ImaGen
    {
        private ImaGen()
        {

        }
        public static int DateToCalImg(Date d, string filename)
        {            
            int rv = WriteDayNameToImage(d, filename);           
            if (rv > 0)
                rv = BlockMinutesImgGen(d, filename);
            if (rv > 0)
                rv = ConvertBmpToPng(filename);
            return rv;
        }
        public static int ConvertBmpToPng(string filename)
        {
            Bitmap sDate = new Bitmap("./Generated_bmps/" + filename + "Block.bmp");
            sDate.Save("./Generated_bmps/" + filename + ".png", ImageFormat.Png);
            return 1;
        }
        private static Rectangle rectFromBlock(int start, int end)
        {
            Console.WriteLine("start: " + start + " end: "+ end);
            int sHour = start / 60;
            int sMin = start % 60;
            int eHour = end / 60;
            int eMin = end % 60;
            float percentage1 = (float)sMin / 60;
            float mins1 = percentage1 * 25;
            Console.WriteLine("percentage1: " + percentage1 + " sMin " + sMin + " mins1: " + mins1);
            int heightTop = 25 * sHour + (int)mins1;
            float percentage2 = (float)eMin / 60;
            float mins2 = percentage2 * 25;
            Console.WriteLine("sMin " + eMin + " mins2: " + mins2);
            int heightBotton = 25 * eHour + (int)(mins2);
            int totalHeight = heightBotton - heightTop;
            Console.WriteLine("sHour: " + sHour + " sMin: " + sMin + " eHour: " + eHour + " eMin: " + eMin + " heightTop: " + heightTop + " heightBottom: " + heightBotton);
            return new Rectangle(0, heightTop, 250, totalHeight);
        }
        private static List<Rectangle> minutesToRectList(bool[] min)
        {
            Console.WriteLine("minutesToRectList");
            //Each row is 25 px tall
            //Each Col is 25 px wide
            List<Rectangle> blocks = new List<Rectangle>();
            bool availBlockFound = false;
            int start = -1;
            int end = -1;
            for (int i = 0; i < min.Length; i++)
            {
                Console.WriteLine(min[i]);
                if(min[i] == false)
                {
                    if(!availBlockFound)
                    {
                        Console.WriteLine("Starting a block");
                        start = i;
                        availBlockFound = true;
                    }
                    if(i == min.Length -1)
                    {
                        Console.WriteLine("Adding one");
                        end = i;
                        blocks.Add(rectFromBlock(start, end));
                    }
                }
                else
                {
                    if(availBlockFound)
                    {
                        Console.WriteLine("Adding one");
                        end = i-1;
                        blocks.Add(rectFromBlock(start, end));
                        //create rect
                        //add to list
                        //clear vars
                        availBlockFound = false;
                        start = -1;
                        end = -1;
                    }
                }
            }
            return blocks;
        }
        public static int BlockMinutesImgGen(Date d, string filename)
        {
            Bitmap sDate = new Bitmap("./Generated_bmps/" + filename + ".bmp");


            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);
            List<Rectangle> blocks = minutesToRectList(d.Minutes);
            // Create rectangle.
            Graphics g = Graphics.FromImage(sDate);
            // Draw rectangle to screen.
            Console.WriteLine("There are " + blocks.Count + "Rectangles in the list.");
            foreach (Rectangle block in blocks)
            {
                g.DrawRectangle(blackPen, block);
            }
            
            sDate.Save("./Generated_bmps/" + filename + "Block.bmp");
            return 1;
        }
        public static int WriteDayNameToImage(Date d, string fileName)//(Date d)
        {
            DateTime dateValue = new DateTime(d.Year, d.Month, d.Day);           
            Console.WriteLine(dateValue.ToString("dddd").ToUpper());
            // Draw vertical string test
            // Load image 
            Bitmap sDate = new Bitmap("./Template_bmps/BlankDayTemplate2.bmp");
            Graphics g = Graphics.FromImage(sDate);
            // Create text
            String date = dateValue.ToString("dddd").ToUpper() + " | " + d.Month + "/" + d.Day + "/" + d.Year;
            // Create font and brush
            Font drawFont = new Font("Bebas Neue", 14);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            // Create points for draw locations
            Rectangle rect2 = new Rectangle(0,10,80,500);
            // Set format
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            drawFormat.Alignment = StringAlignment.Near;
            drawFormat.LineAlignment = StringAlignment.Near;

            // Draw to image
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.DrawString(date, drawFont, drawBrush, rect2, drawFormat);
            // Rotate the image
            // Save new image
            sDate.RotateFlip(RotateFlipType.Rotate180FlipNone);
            sDate.Save("./Generated_bmps/" + fileName + ".bmp");//, ImageFormat.Png);
    
            return 1;
        }
        public static string GenerateWeekFromDateTime(DateTime dt)
        {
            return "";
        }

    }
}
