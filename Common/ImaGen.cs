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
        public static int WriteDayNameToImage(Date d, string fileName)//(Date d)
        {
            // Draw vertical string test
            // Load image 
            Bitmap monday = new Bitmap(@"C:\Users\Matt\Desktop\Calender\bmps\BlankDayTemplate2.bmp");
            Graphics g = Graphics.FromImage(monday);
            // Create text
            String date = "MONDAY | 07/12/2021";
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
            monday.RotateFlip(RotateFlipType.Rotate180FlipNone);
            monday.Save(@"C:\Users\Matt\Desktop\Calender\bmps\" + fileName);//, ImageFormat.Png);
    
            return 1;
        }
        public static string GenerateWeekFromDateTime(DateTime dt)
        {
            return "";
        }

    }
}
