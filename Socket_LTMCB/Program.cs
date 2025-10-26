using System;
using System.Windows.Forms;
using Socket_LTMCB.Client;

namespace Socket_LTMCB
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ✅ HIỂN thị Dashboard để chọn Server hoặc Client
            Application.Run(new Dashboard());
        }
    }
}