using QuotesSong.Forms;
using System;
using System.Windows.Forms;

namespace QuotesSong
{
    static class Program
    {
        /// <summary>
        /// Main entry
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();            
            Application.SetCompatibleTextRenderingDefault(false);
            bool runsetform = false;
            DialogResult dr = DialogResult.Cancel;
            if (Properties.Settings.Default.isFirstRun)
            {
                SetForm  form = new SetForm(Properties.Settings.Default.isFirstRun);
                dr = form.ShowDialog();
                runsetform = true;                        
            }

            if (!runsetform || dr != DialogResult.Cancel)
            {
                Application.Run(new MainForm());
            }            
        }
    }
}
