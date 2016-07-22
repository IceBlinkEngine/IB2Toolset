using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IB2Toolset
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ParentForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("fail: " + ex.ToString());
            }
        }
    }
}
