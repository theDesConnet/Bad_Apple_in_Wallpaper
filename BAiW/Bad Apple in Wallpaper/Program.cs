using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Bad_Apple_in_Wallpaper
{
    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;

        private static void DisplayPicture(string file_name)
        {
            uint flags = 0;
            if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, file_name, flags))
            {
                Console.WriteLine("Error");
            }
        }
        public class MyStringComparer : Comparer<string>, IComparer<string>
        {
            public override int Compare(string x, string y)
            {
                decimal dx = Convert.ToDecimal(Regex.Replace(x, @"[^\d]", ""));
                decimal dy = Convert.ToDecimal(Regex.Replace(y, @"[^\d]", ""));
                return Convert.ToInt32(dx - dy);
            }
        }
        static void Main(string[] args)
        {

            List<String> DirSearch(string sDir)
            {
                List<String> files = new List<String>();
                try
                {
                    foreach (string f in Directory.GetFiles(sDir))
                    {
                        files.Add(f);
                    }
                    foreach (string d in Directory.GetDirectories(sDir))
                    {
                        files.AddRange(DirSearch(d));
                    }
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }

                return files;
            }

            List<string> Files = DirSearch("Frames");
            Files.Sort(new MyStringComparer());

            Console.WriteLine("'Bad Apple in Wallpaper' Started\nc0d9d by DesConnet");

            foreach (string file in Files)
            {
                DisplayPicture(file);
                Thread.Sleep(1000 / 60);
            }
        }
    }
}
