using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KOMAKRO
{
    internal static class Program
    {


        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        
        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        [STAThread]
        static void Main()
        {
            // Konsol penceresini oluştur
            AllocConsole();
            // Form açılışını başlat
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new KOMacro());
            FreeConsole();
        }
    }
}
