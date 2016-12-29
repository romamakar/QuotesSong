using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using QuotesSong.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace QuotesSong.Forms
{
    public partial class LoadFromSiteForm : Form
    {
        #region members
        private CancellationTokenSource cancelsource;
        CancellationToken token;
        IWebDriver webdrv;
        private Dictionary<string, string> radioWithSite = new Dictionary<string, string>();
        private string curStatus = string.Empty;
        #endregion

        #region methods
        public void LoadFromSite(KeyValuePair<string, string> radio, DateTime dt, CancellationToken token, string site, SynchronizationContext context)
        {
            string curdate = Crud.DateTimeSQLiteForRadioscope(dt).Replace("-", "/");
            webdrv.Navigate().GoToUrl(radio.Value + curdate + "/01");
            Crud.InsertIntoRadiostation(radio.Key);
            WebDriverWait ww = new WebDriverWait(webdrv, TimeSpan.FromSeconds(20));
            IWebElement listhour = ww.Until(ExpectedConditions.ElementIsVisible(By.TagName("li")));
            IJavaScriptExecutor ex = (IJavaScriptExecutor)webdrv;
            for (int i = 1; i <= 23; i += 2)
            {
                context.Send((progres) =>
                {
                    ToolStripProgressBar bar = progres as ToolStripProgressBar;
                    bar.Visible = true;
                    bar.Value = i * 4;
                }, toolStripProgressBar1);

                List<SongClass> songslist = new List<SongClass>();
                string iter = i < 10 ? "0" + i.ToString() : i.ToString();
                token.ThrowIfCancellationRequested();
                ex.ExecuteScript(String.Format(@"init.updateList('{0}/{1}')", curdate, iter));
                string[] durationstring;
                string[] songstring;
                string[] songtime;
                for (;;)
                {
                    try
                    {
                        ww.Until(ExpectedConditions.ElementIsVisible(By.ClassName("ellipsis")));
                        ww.Until(ExpectedConditions.ElementIsVisible(By.ClassName("col-xs-1")));
                        ww.Until(ExpectedConditions.ElementIsVisible(By.ClassName("col-xs-2")));
                        durationstring = webdrv.FindElements(By.ClassName("col-xs-1")).Where(x => !string.IsNullOrWhiteSpace(x.Text))
                            .Select((el, str) => { return el.Text; }).ToArray();
                        songstring = webdrv.FindElements(By.ClassName("ellipsis"))
                            .Select((el, str) => { return el.Text; }).ToArray();
                        songtime = webdrv.FindElements(By.ClassName("col-xs-2")).Where(x => !string.IsNullOrWhiteSpace(x.Text))
                            .Select((el, str) => { return el.Text; }).ToArray();
                        break;
                    }
                    catch (StaleElementReferenceException) { }
                }

                for (int k = songstring.Count() - 1; k >= 0; k--)
                {
                    SongClass songcl = new SongClass();
                    string songauthor = songstring[k].Replace(" - ", "|");
                    string[] nameAuthor = songauthor.Split('|');
                    songcl.Author = nameAuthor[0];
                    songcl.Name = "";
                    for (int j = 1; j < nameAuthor.Count(); j++)
                    {
                        songcl.Name += nameAuthor[j];
                        if (j < (nameAuthor.Count() - 1))
                        {
                            songcl.Name += " - ";
                        }
                    }
                    songcl.Duration = Crud.GetMinAndSecFromText(durationstring[k]);
                    songcl.Country = string.Empty;
                    songcl.Language = Crud.ReturnUkrLangForSong(songcl.Name);
                    songcl.dt = dt.AddHours(Convert.ToInt32((songtime[k].Replace(" - ", "|").Split('|')[0].Split(':')[0].Replace("[", "")))).AddMinutes(Convert.ToInt32((songtime[k].Replace(" - ", "|").Split('|')[0].Split(':')[1].Replace("]", ""))));
                    //TODO  all languages and countries
                    songslist.Add(songcl);
                }
                Crud.InsertIntoSongs(songslist);
                Crud.InsertIntoPlaylist(songslist, radio.Key, site);
            }

        }
        private void DisableEnableAllControls(bool cond)
        {
            okBtn.Enabled = siteCombo.Enabled = dateTimePickerFrom.Enabled = dateTimePickerTo.Enabled = radiosListBox.Enabled = cond;
        }
        private void InitPickers()
        {
            dateTimePickerTo.MaxDate = dateTimePickerFrom.MaxDate = DateTime.Today.AddDays(-1);
        }
        public void InitializeChecklistBox()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(item[]), new XmlRootAttribute() { ElementName = "items" });
            using (FileStream fs = new FileStream("radioscope.xml", FileMode.OpenOrCreate))
            {
                radioWithSite = ((item[])serializer.Deserialize(fs))
                .ToDictionary(i => i.id, i => i.value);
            }
            foreach (var rad in radioWithSite)
            {
                radiosListBox.Items.Add(rad.Key);
            }
        }
        #endregion

        public LoadFromSiteForm()
        {
            InitializeComponent();
            InitializeChecklistBox();
            InitPickers();
            siteCombo.SelectedItem = siteCombo.Items[0];
            radiosListBox_ItemCheck(null, null);
            TopMost = Crud.ontop;
        }

        #region events
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if (cancelsource != null && token != null)
            {
                toolStripStatusLabel1.Text = "Статус: Відмінено користувачем";
                cancelsource.Cancel();
                cancelsource = null;
            }
            else
            {
                Close();
            }
        }
        private async void okBtn_Click(object sender, EventArgs e)
        {
            if (Crud.CheckForInternetConnection())
            {
                DisableEnableAllControls(false);
                cancelsource = new CancellationTokenSource();
                token = cancelsource.Token;
                string site = siteCombo.Text;
                if (webdrv == null)
                {
                    DriverService cd = Settings.Default.BrowserName == "Chrome" ? ChromeDriverService.CreateDefaultService(Settings.Default.BrowserBit) as DriverService : FirefoxDriverService.CreateDefaultService(Settings.Default.BrowserBit) as DriverService;
                    cd.HideCommandPromptWindow = true;
                    toolStripStatusLabel1.Text = "Статус: Відкриття браузера";
                    var taskdrv = Task<IWebDriver>.Factory.StartNew(() =>
                    {
                        try
                        {
                            if (Settings.Default.BrowserName == "Chrome")
                                return new ChromeDriver((ChromeDriverService)cd);
                            else
                                return new FirefoxDriver((FirefoxDriverService)cd);
                        }
                        catch (Exception exec)
                        {
                            toolStripProgressBar1.Value = 0;
                            toolStripProgressBar1.Visible = false;
                            toolStripStatusLabel1.Text = "Статус: Помилка під час завантаження";
                            MessageBox.Show("Помилка. Деталі:\n" + exec.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }

                    });
                    using (webdrv = await taskdrv)
                    {
                        if (webdrv != null)
                        {
                            try
                            {
                                foreach (var t in radiosListBox.CheckedItems)
                                {
                                    var radio = radioWithSite.Where(x => x.Key == t.ToString()).FirstOrDefault();
                                    TimeSpan diff = dateTimePickerTo.Value - dateTimePickerFrom.Value;
                                    for (int i = 0; i <= diff.Days; i++)
                                    {
                                        toolStripStatusLabel1.Text = "Завантаження плейлисту " + radio.Key + " за " + dateTimePickerFrom.Value.AddDays(i).ToShortDateString();
                                        await Task.Factory.StartNew((context) => LoadFromSite(radio, dateTimePickerFrom.Value.AddDays(i), token, site, context as SynchronizationContext), SynchronizationContext.Current, token);
                                        toolStripStatusLabel1.Text = string.Empty;
                                    }
                                }
                                toolStripStatusLabel1.Text = "Статус: Готово";
                                toolStripProgressBar1.Visible = false;
                            }
                            catch (Exception ex)
                            {
                                toolStripProgressBar1.Value = 0;
                                toolStripProgressBar1.Visible = false;
                                toolStripStatusLabel1.Text = "Статус: Помилка під час завантаження";
                                MessageBox.Show("Помилка. Деталі:\n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                webdrv = null;
                DisableEnableAllControls(true);
                Crud.LoadEmptyLangWindow();
                cancelsource = null;
            }
            else
            {
                MessageBox.Show("Перевірте підключення до Інтернету!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerTo.MinDate = dateTimePickerFrom.Value;
        }
        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerFrom.MaxDate = dateTimePickerTo.Value;
        }
        private void radiosListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<string> checkedItems = new List<string>();
            foreach (var item in radiosListBox.CheckedItems)
                checkedItems.Add(item.ToString());
            if (e != null && e.NewValue == CheckState.Checked)
                checkedItems.Add(radiosListBox.Items[e.Index].ToString());
            if (e != null && e.NewValue == CheckState.Unchecked)
                checkedItems.Remove(radiosListBox.Items[e.Index].ToString());
            if (checkedItems.Count == 0)
                okBtn.Enabled = false;
            else
                okBtn.Enabled = true;
        }
        #endregion
    }
}
