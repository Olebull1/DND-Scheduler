using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DND_Scheduler.Common
{
    class ImaGen
    {
        private ImaGen()
        {

        }
        public static string DateToCalImage(Date d)
        {
            Bitmap myBitmap = new Bitmap(@"E:\calendar\calendar-blank.bmp");
            Graphics g = Graphics.FromImage(myBitmap);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString("My\nText",
                         new Font("Tahoma", 20),
                         Brushes.White,
                         new PointF(0, 0));

            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            g.DrawString("Fat\n Cocks\n Cocks",
                         new Font("Tahoma", 20), Brushes.Red,
                         new RectangleF(0, 0, 500, 500),
                         strFormat);
            myBitmap.Save(@"E:\calendar\calendar-edit.bmp");
            return "";
        }
        public static string GenerateWeekFromDateTime(DateTime dt)
        {
            return "";
        }

    }
}
