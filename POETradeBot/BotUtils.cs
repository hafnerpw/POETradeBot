using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AForge.Imaging;
using Image = System.Drawing.Image;

namespace POETradeBot
{
    public class BotUtils
    {

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        
        
        public static void ClickAt(int x, int y)
        {
            var oldX = Cursor.Position.X;
            var oldY = Cursor.Position.Y;
            MouseOperations.SetCursorPosition(x, y);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            System.Threading.Thread.Sleep(80);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            MouseOperations.SetCursorPosition(oldX, oldY);
        }
        private static void BringPoeToFront()
        {
            var poeProcess = Process.GetProcessesByName("PathOfExileSteam").FirstOrDefault();
            if (poeProcess == null) return;
            
            var h = poeProcess.MainWindowHandle;
            SetForegroundWindow(h);
        }


        public static void SendKeysToPoe(string paste = "")
        {
            BringPoeToFront();
            var oldClipboard = Clipboard.GetText();
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait("^A");
            SendKeys.SendWait("{DEL}");
            Clipboard.SetText(paste);
            SendKeys.SendWait("^V");
            SendKeys.SendWait("{ENTER}");
            Clipboard.SetText(oldClipboard);
        }
        public static Bitmap ConvertToFormat(Image image, PixelFormat format = PixelFormat.Format24bppRgb)
        {
            var copy = new Bitmap(image.Width, image.Height, format);
            var gr = Graphics.FromImage(copy);
            gr.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
            gr.Dispose();
            return copy;
        }
        public static bool InPicture(Bitmap mainImage, Bitmap pattern)
        {
            var tm = new ExhaustiveTemplateMatching(0.91f);

            var matches = tm.ProcessImage(mainImage, pattern);
            return matches.Length > 0;
        }
        
        public static Bitmap CaptureScreen(int x0, int y0, int width, int height)
        {
            var image = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var gfx = Graphics.FromImage(image);
            gfx.CopyFromScreen(new Point(x0, y0), new Point(0, 0), new Size(width, height));
            gfx.Dispose();
            return image;
        }
    }
}