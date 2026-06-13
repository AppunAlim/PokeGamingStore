using System;
using System.Windows.Forms;
using PokeGamingStore.GUI;

namespace PokeGamingStore
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Langsung jalankan wadah utama GUI, tidak memakai CMD lagi
            Application.Run(new MainForm());
        }
    }
}