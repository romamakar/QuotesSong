using QuotesSong.Forms;
using QuotesSong.Properties;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuotesSong
{
    public partial class MainForm : Form
    {
        #region consts
        private const int c_legendItemHeight = 20;
        private const string c_checkCustomPropertyName = "CHECK";
        private const string c_checkedString = "✔";
        private const string c_uncheckedString = "✘";

        #endregion

        #region members
        private DataSet ds;
        private DataSet ds2;
        private DataSet ds3;
        #endregion
        public MainForm()
        {
            try
            {
                InitializeComponent();
                InitControls();
                LoadData();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        #region methods
        public void InitControls()
        {

            DateTime dt = DateTime.MinValue;
            cryteryCheckBoxTable.SelectedItem = cryteryCheckBoxTable.Items[0];
            cryteryComboChart.SelectedItem = cryteryComboChart.Items[0];
            radioComboTable.Items.Clear();
            radioComboTable.Items.Add("Всі");
            radioComboTable.Items.AddRange(Crud.GetAllRadiostations().ToArray());
            stationComboChart.Items.Clear();
            stationComboChart.Items.AddRange(Crud.GetAllRadiostations().ToArray());
            if (stationComboChart.Items.Count > 0)
                stationComboChart.SelectedItem = stationComboChart.Items[0];
            radioComboTable.SelectedItem = radioComboTable.Items[0];
            dt = Crud.GetMinDate();
            if (dt == DateTime.MinValue)
            {
                dateTimePickerFromTable.Enabled = dateTimePickerToTable.Enabled = timePickerFromTable.Enabled = timePickerToTable.Enabled = setTimeCheckBox.Enabled = false;
            }
            else
            {
                dateTimePickerFromTable.Enabled = dateTimePickerToTable.Enabled = timePickerFromTable.Enabled = timePickerToTable.Enabled = setTimeCheckBox.Enabled = true;
                dateTimePickerFromTable.MinDate = dateTimePickerToTable.MinDate = Crud.GetMinDate();
                dateTimePickerFromTable.MaxDate = dateTimePickerToTable.MaxDate = Crud.GetMaxDate();
            }

            CheckboxVisiblePickers();
        }
        public void CheckboxVisiblePickers()
        {
            DateTime dt = DateTime.MinValue;
            dt = Crud.GetMinDate();
            if (dt != DateTime.MinValue)
            {


                if (setTimeCheckBox.Checked)
                {
                    dateTimePickerToTable.Enabled = false;
                    timePickerFromTable.Enabled = true;
                    timePickerToTable.Enabled = true;
                }
                else
                {
                    dateTimePickerToTable.Enabled = true; ;
                    timePickerFromTable.Enabled = false;
                    timePickerToTable.Enabled = false;
                }
            }
        }
        private string FilterBuilder(string textRadio, string type)
        {
            textRadio = textRadio.Replace("'", "''");
            string nametype = type == "Країна" ? "Україна" : "Українська";
            string res = "";
            string radio = textRadio == "Всі" ? string.Empty : textRadio;
            if (!string.IsNullOrEmpty(radio))
            {
                res += string.Format(" Name='{0}'", radio);
            }

            return res;
        }
        public void LoadData()
        {
            DateTime dtfrom = dateTimePickerFromTable.Value.Date;
            DateTime dtto = dateTimePickerToTable.Value.Date.AddHours(23).AddMinutes(59);
            if (!setTimeCheckBox.Checked)
            {
                dtfrom = dateTimePickerFromTable.Value.Date;
                dtto = dateTimePickerToTable.Value.Date.AddHours(23).AddMinutes(59);
            }
            else
            {
                dtfrom = dateTimePickerFromTable.Value.Date.AddHours(timePickerFromTable.Value.Hour).AddMinutes(timePickerFromTable.Value.Minute);
                dtto = dateTimePickerFromTable.Value.Date.AddHours(timePickerToTable.Value.Hour).AddMinutes(timePickerToTable.Value.Minute);
            }
            using (var con = new SQLiteConnection())
            {
                ds = new DataSet();
                con.ConnectionString = Settings.Default.SongDBConnectionString;
                con.Open();
                using (var adap = new SQLiteDataAdapter(Crud.BuildSelectForMain(cryteryCheckBoxTable.Text == "Мова" ? "Language" : "Country", dtfrom, dtto), con))
                {
                    adap.Fill(ds, "Radiostations");
                }
            }
            mainGrid.Columns.Clear();
            ds.Tables[0].DefaultView.RowFilter = FilterBuilder(radioComboTable.Text, cryteryCheckBoxTable.Text);
            mainGrid.DataSource = ds.Tables[0];
            mainGrid.Columns[0].HeaderText = "Радіостанція";
            for (int i = 1; i < mainGrid.Columns.Count - 1; i++)
            {
                mainGrid.Columns[i].HeaderText += " (в %)";
            }

            mainGrid.Columns[mainGrid.Columns.Count - 1].HeaderText = "Кількість невизначених пісень";
            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[mainGrid.DataSource];
            currencyManager1.SuspendBinding();
            foreach (DataGridViewRow data in mainGrid.Rows)
            {
                for (int i = 1; i < data.DataGridView.Columns.Count; i++)
                {
                    float percent;
                    if (float.TryParse(data.Cells[i].Value.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out percent))
                    {
                        data.Visible = false;
                        if (percent != 0.00 || percent != 0)
                        {
                            data.Visible = true;
                            break;
                        }
                    }
                }

            }
            mainGrid.Refresh();
            currencyManager1.ResumeBinding();

            toolStripStatusLabel1.Text = string.Format("Кількість записів: {0}", mainGrid.Rows.Cast<DataGridViewRow>().Where(y => y.Visible == true).Count());
        }
        private void DrawChart()
        {
            mainChart.Series.Clear();
            mainChart.ChartAreas.Clear();
            mainChart.ChartAreas.Add("ChartArea1");
            ds3 = new DataSet();
            using (var con = new SQLiteConnection())
            {
                ds2 = new DataSet();
                con.ConnectionString = Settings.Default.SongDBConnectionString;
                con.Open();
                if (dateTimePickerFromChart.Value.Date == dateTimePickerToChart.Value.Date)
                {
                    for (int i = 1; i <= 24; i++)
                    {
                        using (var adap = new SQLiteDataAdapter(Crud.BuildSelectForChartOneDay(cryteryComboChart.Text == "Мова" ? "Language" : "Country", dateTimePickerFromChart.Value.Date.AddHours(i - 1), dateTimePickerFromChart.Value.Date.AddHours(i), stationComboChart.Text), con))
                        {
                            adap.Fill(ds2, "Radiostations");
                        }

                        ds3 = ds2.Clone();
                        foreach (DataRow row in ds2.Tables[0].Rows)
                        {
                            ds3.Tables[0].Rows.Add(row.ItemArray);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= (dateTimePickerToChart.Value.Date - dateTimePickerFromChart.Value.Date).Days; i++)
                    {
                        using (var adap = new SQLiteDataAdapter(Crud.BuildSelectForChart(cryteryComboChart.Text == "Мова" ? "Language" : "Country", dateTimePickerFromChart.Value.Date.AddDays(i), dateTimePickerFromChart.Value.Date.AddDays(i + 1).AddSeconds(-1), stationComboChart.Text), con))
                        {
                            adap.Fill(ds2, "Radiostations");
                        }
                        ds3 = ds2.Clone();
                        foreach (DataRow row in ds2.Tables[0].Rows)
                        {
                            ds3.Tables[0].Rows.Add(row.ItemArray);
                        }
                    }
                }
            }
            if (dateTimePickerFromChart.Value.Date == dateTimePickerToChart.Value.Date)
            {
                mainChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                mainChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                mainChart.ChartAreas[0].AxisX.Minimum = 0;
                mainChart.ChartAreas[0].AxisX.Maximum = 23;
                mainChart.ChartAreas[0].AxisX.Interval = 1;
                mainChart.ChartAreas[0].AxisX.Title = "Години дня";
                mainChart.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12);
                mainChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
                mainChart.ChartAreas[0].AxisY.Title = "%";
                mainChart.ChartAreas[0].AxisY.Minimum = 0;
                mainChart.ChartAreas[0].AxisY.Maximum = 110;
                mainChart.ChartAreas[0].AxisY.Interval = 5;
                foreach (DataColumn col in ds3.Tables[0].Columns)
                {
                    if (col.Caption == "Name") continue;
                    Series sr = new Series(col.Caption);
                    sr.ChartArea = mainChart.ChartAreas[0].Name;
                    sr.ChartType = SeriesChartType.Spline;
                    sr.SmartLabelStyle.Enabled = true;
                    sr.BorderWidth = 5;
                    for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                    {
                        DataPoint dp = new DataPoint();
                        dp.SetValueXY(i, ds3.Tables[0].Rows[i][col]);
                        sr.Points.Add(dp);
                        dp.ToolTip = "#SERIESNAME : Час - #VALX:00, #VALY%";
                        dp.Label = "#VALY%";
                    }
                    mainChart.Series.Add(sr);
                }
            }
            else
            {
                mainChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                mainChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
                mainChart.ChartAreas[0].AxisX.Minimum = 0;
                mainChart.ChartAreas[0].AxisX.Interval = 1;
                mainChart.ChartAreas[0].AxisX.Title = "Дні";
                mainChart.ChartAreas[0].AxisY.Title = "%";
                mainChart.ChartAreas[0].AxisY.Minimum = 0;
                mainChart.ChartAreas[0].AxisY.Maximum = 110;
                mainChart.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12);
                mainChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12);
                mainChart.ChartAreas[0].AxisY.Interval = 5;
                foreach (DataColumn col in ds3.Tables[0].Columns)
                {
                    if (col.Caption == "Name") continue;
                    Series sr = new Series(col.Caption);
                    sr.ChartArea = mainChart.ChartAreas[0].Name;
                    sr.ChartType = SeriesChartType.Spline;
                    sr.BorderWidth = 5;
                    sr.SmartLabelStyle.Enabled = true;
                    for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                    {
                        DataPoint dp = new DataPoint();
                        dp.SetValueXY(i, ds3.Tables[0].Rows[i][col]);
                        sr.Points.Add(dp);
                        string datetip = dateTimePickerFromChart.Value.Date.AddDays(i).ToShortDateString();
                        dp.ToolTip = "#SERIESNAME : День - " + datetip + " #VALY%";
                        dp.Label = "#VALY%";
                    }
                    mainChart.Series.Add(sr);
                }
            }
            DrawLegend();
            SetLablesVisible();
        }
        private void DrawLegend()
        {

            // mainChart
            mainChart.Legends.Clear();

            // LegendChart
            Legend legend = legendChart.Legends[0];
            legendChart.Series.Clear();
            legend.IsTextAutoFit = false;
            legend.IsEquallySpacedItems = true;
            legend.MaximumAutoSize = 100;
            legend.Docking = Docking.Left;
            legend.LegendStyle = LegendStyle.Column;
            legend.Position.Auto = true;
            legend.Position.Width = 100;
            legend.Position.Height = 100;

            legend.CellColumns[2].Text = "#CUSTOMPROPERTY(" + c_checkCustomPropertyName + ")";

            foreach (var share in mainChart.Series)
            {
                Series series = legendChart.Series.Add(share.Name);
                series.SetCustomProperty(c_checkCustomPropertyName, c_checkedString);
            }
            legendChart.Height = legendChart.Series.Count * c_legendItemHeight + 9; // 9 - seems to be constant value

        }
        public void SetLablesVisible()
        {
            if (showHintsCheckBox.Checked)
            {
                foreach (var ser in mainChart.Series)
                {
                    foreach (var point in ser.Points)
                    {
                        point.Label = "#VALY%";
                    }
                }
            }

            else
            {
                foreach (var ser in mainChart.Series)
                {
                    foreach (var point in ser.Points)
                    {
                        point.Label = "";
                    }
                }
            }
        }

        #endregion

        #region events
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void DBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBSongForm songForm = new DBSongForm();
            songForm.ShowDialog();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayListEditForm pform = new PlayListEditForm();
            pform.ShowDialog();
        }
        private void fromSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFromSiteForm sitefrom = new LoadFromSiteForm();
            sitefrom.ShowDialog();
        }
        private void manualyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillPlaylistForm playform = new FillPlaylistForm();
            playform.ShowDialog();
        }
        private void radiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RadiostationsForm radioform = new RadiostationsForm();
            radioform.ShowDialog();
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetForm setform = new SetForm();
            setform.ShowDialog();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string deleteplaylist = Properties.Settings.Default.ClearWhereClosing ? "\n*Всі плейлисти буде видалено з бази даних." : "";
            var result = MessageBox.Show("Ви дійсно бажаєте вийти?" + deleteplaylist, "Підтвердженя",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

            if (result == DialogResult.Yes && Properties.Settings.Default.ClearWhereClosing)
                Crud.ClearPlayList();
            e.Cancel = (result == DialogResult.No);
        }
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Активно працюємо над довідкою:-)");
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox form = new AboutBox();
            form.ShowDialog();
        }
        private void refreshDataBtn_Click(object sender, EventArgs e)
        {
            if ((dateTimePickerFromTable.Value.Date > dateTimePickerToTable.Value.Date) || (timePickerFromTable.Value > timePickerToTable.Value))
            {
                MessageBox.Show("Некоректні дати/час вибірки!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            InitControls();
            LoadData();
        }
        private void ontopToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            Crud.ontop = ontopToolStripMenuItem.CheckState == CheckState.Checked ? true : false;
            TopMost = Crud.ontop;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
            ds.Tables[0].DefaultView.RowFilter = FilterBuilder(radioComboTable.Text, cryteryCheckBoxTable.Text);
        }
        private void refreshChartBtn_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFromChart.Value.Date > dateTimePickerToChart.Value.Date)
            {
                MessageBox.Show("Некоректні дати вибірки!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DrawChart();
        }
        private void setTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckboxVisiblePickers();
        }
        private void cryteryCheckBoxTable_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void radioComboTable_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void showHintsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetLablesVisible();
        }
        private void stationComboChart_SelectedValueChanged(object sender, EventArgs e)
        {
            refreshChartBtn_Click(null, null);
        }
        private void cryteryComboChart_SelectedValueChanged(object sender, EventArgs e)
        {
            refreshChartBtn_Click(null, null);
        }
        private void resfreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshDataBtn_Click(null, null);
        }
        private void legendChart_MouseClick(object sender, MouseEventArgs e)
        {
            Point mousePosition = legendChart.PointToClient(Control.MousePosition);
            int seriesNo = mousePosition.Y / c_legendItemHeight;
            try
            {
                Series series = legendChart.Series[seriesNo];
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (series.GetCustomProperty(c_checkCustomPropertyName) == c_checkedString)
                    {

                        series.SetCustomProperty(c_checkCustomPropertyName, c_uncheckedString);
                        series.CustomProperties = series.CustomProperties;
                        mainChart.Series[seriesNo].Enabled = false;
                    }
                    else
                    {
                        legendChart.Series[seriesNo].SetCustomProperty(c_checkCustomPropertyName, c_checkedString);
                        series.CustomProperties = series.CustomProperties;
                        mainChart.Series[seriesNo].Enabled = true;
                    }
                }
            }
            catch { }
        }
        private void onlyBadStationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[mainGrid.DataSource];
            currencyManager1.SuspendBinding();
            if (onlyBadStationCheckBox.Checked)
            {
                float quotes = cryteryCheckBoxTable.Text == "Країна" ? Settings.Default.quotesUkrCountry : Settings.Default.quotesUkrLang;
                string nametype = cryteryCheckBoxTable.Text == "Країна" ? "Україна" : "Українська";
                foreach (DataGridViewRow data in mainGrid.Rows)
                {
                    float percent;
                    if (float.TryParse(data.Cells[nametype].Value.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out percent))
                    {
                        data.Visible = quotes > percent;
                    }
                }
                mainGrid.Refresh();
            }
            else
            {
                foreach (DataGridViewRow data in mainGrid.Rows)
                {
                    for (int i = 1; i < data.DataGridView.Columns.Count; i++)
                    {
                        float percent;
                        if (float.TryParse(data.Cells[i].Value.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out percent))
                        {
                            data.Visible = false;
                            if (percent != 0.00 || percent != 0)
                            {
                                data.Visible = true;
                                break;
                            }
                        }
                    }
                }
                mainGrid.Refresh();
                currencyManager1.ResumeBinding();
            }
            toolStripStatusLabel1.Text = string.Format("Кількість записів: {0}", mainGrid.Rows.Cast<DataGridViewRow>().Where(y => y.Visible == true).Count());
        }

        #endregion
    }
}


