using QuotesSong.Properties;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuotesSong.Forms
{
    public partial class SetForm : Form
    {
        #region members
        bool firstrun = false;
        #endregion
        public SetForm()
        {
            InitializeComponent();
            InitControls();
            TopMost = Crud.ontop;
        }
        public SetForm(bool firstrun) : this()
        {
            this.firstrun = firstrun;
        }

        #region methods
        public void InitControls()
        {
            x64radio.Enabled = Environment.Is64BitOperatingSystem;
            x86radio.Checked = Settings.Default.BrowserBit == "./WebDrivers/x86";
            x64radio.Checked = Settings.Default.BrowserBit == "./WebDrivers/x64";
            radioChrome.Checked = Settings.Default.BrowserName == "Chrome";
            radioMozilla.Checked = Settings.Default.BrowserName == "Firefox";
            listLangs.Lines = Settings.Default.listLang.Cast<string>().ToArray();
            listConts.Lines = Settings.Default.listCountries.Cast<string>().ToArray();
            deletePlaylist.Checked = Settings.Default.ClearWhereClosing;
            quoteUkrLang.Value = Settings.Default.quotesUkrLang;
            quoteUkrCountrs.Value = Settings.Default.quotesUkrCountry;
        }
        #endregion

        #region events
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Settings.Default.Reload();
            Settings.Default.Reset();

            listLangs.Lines = listLangs.Lines.Distinct().ToArray();
            listConts.Lines = listConts.Lines.Distinct().ToArray();
            if ((listLangs.Lines.Where(x => x == "Українська").Count() != 1) || (listConts.Lines.Where(x => x == "Україна").Count() != 1))
            {
                MessageBox.Show("Українська мова та Україна повинні бути в списках!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string st = Settings.Default.SongDBConnectionString;
            Settings.Default.listCountries.Clear();
            Settings.Default.listCountries.AddRange(listConts.Lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());
            Settings.Default.listLang.Clear();
            Settings.Default.listLang.AddRange(listLangs.Lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());

            Settings.Default.ClearWhereClosing = deletePlaylist.Checked;
            Settings.Default.isFirstRun = false;
            Settings.Default.quotesUkrCountry = (int)quoteUkrCountrs.Value;
            Settings.Default.quotesUkrLang = (int)quoteUkrLang.Value;

            Settings.Default.BrowserBit = x86radio.Checked ? "./WebDrivers/x86" : "./WebDrivers/x64";
            Settings.Default.BrowserName = radioChrome.Checked ? "Chrome" : "Firefox";
            Settings.Default.Save();
            
            DialogResult = DialogResult.OK;
            Close();
        }
        private void radioChrome_CheckedChanged(object sender, EventArgs e)
        {
            if (radioChrome.Checked)
            {
                x86radio.Checked = true;
                x64radio.Enabled = false;
            }
            else
            {
                x64radio.Enabled = Environment.Is64BitOperatingSystem;
            }
        }

        #endregion
    }
}
