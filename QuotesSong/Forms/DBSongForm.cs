using QuotesSong.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuotesSong.Forms
{
    public partial class DBSongForm : Form
    {
        #region members
        ToolTip toolTip1;
        string selectQuery = "select Id, Author as Виконавець, Name, Language as Мова, Country as Країна from Song ";
        int countSongs = 0;
        int countpages = 0;
        int count = 0;
        int scrollVall = 0;
        private DataSet ds;
        private SQLiteConnection con;
        private SQLiteDataAdapter adap;
        private SQLiteCommandBuilder cmdl;
        #endregion

        public DBSongForm()
        {
            InitializeComponent();
            LoadData();
            InitComboAndCounters();
            TopMost = Crud.ontop;
            toolTip1 = new ToolTip();
        }
        public DBSongForm(string emptyfilter)
        {
            InitializeComponent();
            TopMost = Crud.ontop;
            LoadData(emptyfilter);
            toolTip1 = new ToolTip();
            Text = "Заповніть для пісень їх мови та країни";
            selectorComboBox.Text = "Всі";
            panel2.Visible = false;
        }
        
        #region methods
        public void countPage(string selector)
        {
            selector = selector.Replace("По ", "");
            if (countSongs == 0)
            {
                countpages = 1;
            }
            else if (int.TryParse(selector, out count))
            {
                try
                {
                    countpages = countSongs / count;
                    countpages = countSongs % count == 0 ? countpages : countpages + 1;
                }
                catch { }
            }
            else countpages = 1;
            pageNumericCounter.Maximum = countpages;
            label2.Text = string.Format("з {0}", countpages);
        }
        void InitComboAndCounters()
        {
            selectorComboBox.SelectedItem = selectorComboBox.Items[0];
            pageNumericCounter.Value = 1;
        }
        public void LoadData(string selectquery = "")
        {
            selectQuery += selectquery;
            con = new SQLiteConnection();
            ds = new DataSet();
            con.ConnectionString = Settings.Default.SongDBConnectionString;
            con.Open();
            adap = new SQLiteDataAdapter(selectQuery, con);

            adap.Fill(ds, "Songs");
            mainGrid.DataSource = ds.Tables[0];
            mainGrid.Columns[0].Visible = false;
            mainGrid.Columns[2].HeaderText = "Назва пісні";
            mainGrid.Columns[1].FillWeight = 30;
            mainGrid.Columns[2].FillWeight = 40;
            mainGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            mainGrid.Columns[3].Visible = false;
            mainGrid.Columns[4].Visible = false;

            DataGridViewComboBoxColumn coutrycol = new DataGridViewComboBoxColumn();
            coutrycol.DataSource = new Crud().listCountry;
            coutrycol.DataPropertyName = "Країна";
            coutrycol.HeaderText = "Країна";
            coutrycol.FillWeight = 15;
            DataGridViewComboBoxColumn langcol = new DataGridViewComboBoxColumn();
            langcol.DataSource = new Crud().listLanguage;
            langcol.DataPropertyName = "Мова";
            langcol.HeaderText = "Мова";
            langcol.FillWeight = 15;
            langcol.SortMode = coutrycol.SortMode = DataGridViewColumnSortMode.Automatic;
            mainGrid.Columns.Add(langcol);
            mainGrid.Columns.Add(coutrycol);
            countSongs = ds.Tables[0].Rows.Count;
            ds.Tables[0].Columns["Виконавець"].AllowDBNull = false;
            ds.Tables[0].Columns["Name"].AllowDBNull = false;
            selectorComboBox.SelectedItem = selectorComboBox.Items[0];
            toolStripStatusLabel1.Text = string.Format("Знайдено пісень: {0}", countSongs);
        }        
        private void SetPaging()
        {
            int val = (int)pageNumericCounter.Value - 1;
            scrollVall = val * count;
            if (scrollVall > countSongs)
            {
                scrollVall = countSongs - count;
            }
            if (scrollVall <= 0)
                scrollVall = 0;
            ds.Clear();
            adap.Fill(ds, scrollVall, count, "Songs");
            mainGrid.Columns[0].Visible = false;
        }
        #endregion

        #region events
        private async void saveBth_Click(object sender, EventArgs e)
        {
            Timer tim = new Timer();
            tim.Interval = 1000;
            tim.Tick += Tim_Tick;
            tim.Start();
            cancelBtn.Enabled = saveBth.Enabled = filterTextBox.Enabled = mainGrid.Enabled = false;
            toolStripStatusLabel1.Text = "Зачекайте...";
            try
            {
                cmdl = new SQLiteCommandBuilder(adap);
                await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        adap.Update(ds, "Songs");
                    }

                    catch (SQLiteException)
                    {
                        throw new SQLiteException("Некоректні дані!");
                    }
                });

                Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message.Replace("unknown error", ""), "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tim.Stop();
            cancelBtn.Enabled = saveBth.Enabled = filterTextBox.Enabled = mainGrid.Enabled = true;
            toolStripStatusLabel1.Text = string.Format("Знайдено пісень: {0}", ds.Tables[0].Rows.Count);
        }
        private void Tim_Tick(object sender, EventArgs e)
        {
            int count = 0;
            foreach (char c in toolStripStatusLabel1.Text)
            {
                if (c == '.')
                {
                    count++;
                }
            }
            if (count == 3)
            {
                toolStripStatusLabel1.Text = toolStripStatusLabel1.Text.Replace("...", "");
            }
            toolStripStatusLabel1.Text += ".";
        }
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            pageNumericCounter.Value += 1;
        }
        private void selectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            countPage(selectorComboBox.Text);
            scrollVall = 0;
            SetPaging();
            pageNumericCounter.Value = 1;
            mainGrid.Columns[0].Visible = false;
        }
        private void pageNumericCounter_ValueChanged(object sender, EventArgs e)
        {
            SetPaging();
        }
        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = filterTextBox.Text;
            text = text.Replace("'", "''");
            string filter = selectQuery.Contains("where") ?
                string
                .Format
                (" and (upper(Name) LIKE upper('{0}*')" +
                " or upper(Name) LIKE upper('*{0}')" +
                " or upper(Name) LIKE upper('%{0}%')" +
                " or upper(Виконавець) LIKE upper('{0}*')" +
                " or upper(Виконавець) LIKE upper('*{0}')" +
                " or upper(Виконавець) LIKE upper('%{0}%'))", text) :
                string
                .Format
                (" where upper(Name) LIKE upper('{0}*')" +
                " or upper(Name) LIKE upper('*{0}')" +
                " or upper(Name) LIKE upper('%{0}%')" +
                " or upper(Виконавець) LIKE upper('{0}*')" +
                " or upper(Виконавець) LIKE upper('*{0}')" +
                " or upper(Виконавець) LIKE upper('%{0}%')" +
                " or upper(Країна) LIKE upper('{0}*')" +
                " or upper(Країна) LIKE upper('*{0}')" +
                " or upper(Країна) LIKE upper('%{0}%')" +
                " or upper(Мова) LIKE upper('{0}*')" +
                " or upper(Мова) LIKE upper('*{0}')" +
                " or upper(Мова) LIKE upper('%{0}%')", text);

            ds.Clear();
            con = new SQLiteConnection();
            con.ConnectionString = Settings.Default.SongDBConnectionString;
            con.Open();
            adap = new SQLiteDataAdapter(selectQuery + filter, con);
            adap.Fill(ds, "Songs");
            countSongs = ds.Tables[0].Rows.Count;
            countPage(selectorComboBox.Text);
            SetPaging();
            toolStripStatusLabel1.Text = string.Format("Знайдено пісень: {0}", countSongs);
        }
        private void filterTextBox_Click(object sender, EventArgs e)
        {
            if (filterTextBox.Text == "Пошук...")
            {
                filterTextBox.Text = string.Empty;
            }
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Перевірте дані в " + (e.RowIndex + 1) + " стрічці \nта в колонці " + mainGrid.Columns[e.ColumnIndex].HeaderText, "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Enabled = mainGrid.SelectedRows.Count > 0;
                langToolStripMenuItem.DropDownItems.Clear();
                countryToolStripMenuItem.DropDownItems.Clear();
                List<ToolStripItem> langs = new List<ToolStripItem>();
                foreach (var s in new Crud().listLanguage)
                {
                    ToolStripItem item = new ToolStripMenuItem();
                    item.Text = s;
                    item.Click += Item_Click;
                    langs.Add(item);
                }

                List<ToolStripItem> countries = new List<ToolStripItem>();
                foreach (var s in new Crud().listCountry)
                {
                    ToolStripItem item = new ToolStripMenuItem();
                    item.Text = s;
                    item.Click += Item_Click;
                    countries.Add(item);

                }

                langToolStripMenuItem.DropDownItems.AddRange(langs.ToArray());
                countryToolStripMenuItem.DropDownItems.AddRange(countries.ToArray());
                Rectangle rec = contextMenuStrip1.Bounds;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            foreach (DataGridViewRow row in mainGrid.SelectedRows)
            {
                row.Cells[item.OwnerItem.Text].Value = item.Text;

            }
            mainGrid.Refresh();
        }
        private void DBSongForm_Load(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(this.label3, "Для швидкого редагування мов/країн виберіть стрічку\n та нажміть пр. кнопку миші\n(підтримується мультивибір через Ctrl або Shift)");

        }
        private void mainGrid_SelectionChanged(object sender, EventArgs e)
        {
            selectedRowsLbl.Text = string.Format(" Виділено рядків: {0}", mainGrid.SelectedRows.Count);
        }
        #endregion
    }
}




