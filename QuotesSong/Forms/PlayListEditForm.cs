using QuotesSong.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuotesSong.Forms
{
    public partial class PlayListEditForm : Form
    {
        #region members 
        ToolTip toolTip1 = new ToolTip();
        private DataSet ds;
        SQLiteCommandBuilder cmdl;
        SQLiteDataAdapter adap;
        SQLiteConnection con;
        int radioid = 0;
        #endregion
        public PlayListEditForm()
        {
            InitializeComponent();
            InitControls();
            LoadData();
            TopMost = Crud.ontop;
        }

        #region methods
        public void LoadData()
        {
            mainGrid.Columns.Clear();
            string radio = radioComboBox.Text.Replace("'", "''");
            radioid = Crud.GetRadiostationId(radioComboBox.Text);
            ds = new DataSet();
            con = new SQLiteConnection();

            con.ConnectionString = Settings.Default.SongDBConnectionString;
            con.Open();
            DateTime fromdt = dateTimePicker1.Value.Date;
            DateTime todt = dateTimePicker1.Value.Date.AddHours(23).AddMinutes(59);
            adap = new SQLiteDataAdapter(string.Format("select playlist.id , song.author, song.name, playlist.duration, playlist.datetimesong, playlist.site, song.language, song.country from playlist join song on song.id=playlist.song_id join radiostation on radiostation.id=playlist.station_id where radiostation.name ='{0}' and (playlist.datetimesong between \u0022{1}\u0022 and \u0022{2}\u0022) order by playlist.datetimesong", radio, Crud.DateTimeSQLite(fromdt), Crud.DateTimeSQLite(todt)), con);

            adap.Fill(ds, "Playlist");

            adap.UpdateCommand =
            new SQLiteCommand(
                "  BEGIN transaction; INSERT into Song (name, author, language, country) Select @songname, @songauthor, @language, @country  WHERE  (select count(*) from song where name=@songname and author=@songauthor)<1; " +
                "  UPDATE song set country = @country, language = @language where name=@songname and author=@songauthor;" +
                "  UPDATE playlist SET song_id = (select id from Song where name=@songname and author=@songauthor), duration=@duration, datetimesong=@datetimesong  where id=@playlistid;" + " COMMIT; ", con);

            SQLiteParameter parPlayListId = adap.UpdateCommand.Parameters.Add("@playlistid", DbType.Int32);
            parPlayListId.SourceColumn = "id";
            parPlayListId.SourceVersion = DataRowVersion.Default;

            SQLiteParameter parSongName = adap.UpdateCommand.Parameters.Add("@songname", DbType.String);
            parSongName.SourceColumn = "name";
            parSongName.SourceVersion = DataRowVersion.Default;

            SQLiteParameter parSongAuthor = adap.UpdateCommand.Parameters.Add("@songauthor", DbType.String);
            parSongAuthor.SourceColumn = "author";
            parSongAuthor.SourceVersion = DataRowVersion.Default;

            SQLiteParameter parDuration = adap.UpdateCommand.Parameters.Add("@duration", DbType.Int32);
            parDuration.SourceColumn = "duration";
            parDuration.SourceVersion = DataRowVersion.Default;

            SQLiteParameter parlanguage = adap.UpdateCommand.Parameters.Add("@language", DbType.String);
            parlanguage.SourceColumn = "Language";
            parlanguage.SourceVersion = DataRowVersion.Default;

            SQLiteParameter parCountry = adap.UpdateCommand.Parameters.Add("@country", DbType.String);
            parCountry.SourceColumn = "Country";
            parCountry.SourceVersion = DataRowVersion.Default;

            SQLiteParameter parDateTime = adap.UpdateCommand.Parameters.Add("@datetimesong", DbType.DateTime);
            parDateTime.SourceColumn = "datetimesong";
            parDateTime.SourceVersion = DataRowVersion.Default;

            adap.UpdateCommand.Connection = con;

            adap.InsertCommand = new SQLiteCommand(string.Format(
                "  BEGIN transaction; INSERT into Song (name, author, language, country) Select @songname, @songauthor, @language, @country  WHERE  (select count(*) from song where name=@songname and author=@songauthor)<1;" +
                "  INSERT INTO playlist(song_id, station_id, duration, datetimesong, site)  VALUES((select id from song where name=@songname and author=@songauthor), {0}, @duration, @datetimesong, 'manual');" + " COMMIT; ", radioid)
                , con);
            SQLiteParameter parSongNameIns = adap.InsertCommand.Parameters.Add("@songname", DbType.String);
            parSongNameIns.SourceColumn = "name";
            parSongNameIns.SourceVersion = DataRowVersion.Default;

            SQLiteParameter parSongAuthorIns = adap.InsertCommand.Parameters.Add("@songauthor", DbType.String);
            parSongAuthorIns.SourceColumn = "author";
            parSongAuthorIns.SourceVersion = DataRowVersion.Default;

            SQLiteParameter parDurationIns = adap.InsertCommand.Parameters.Add("@duration", DbType.Int32);
            parDurationIns.SourceColumn = "duration";
            parDurationIns.SourceVersion = DataRowVersion.Default;

            SQLiteParameter insDateTime = adap.InsertCommand.Parameters.Add("@datetimesong", DbType.DateTime);
            insDateTime.SourceColumn = "datetimesong";
            insDateTime.SourceVersion = DataRowVersion.Default;


            adap.InsertCommand.Connection = con;
            adap.DeleteCommand = new SQLiteCommand("delete from playlist where id=@playlistid", con);

            SQLiteParameter parlanguageIns = adap.InsertCommand.Parameters.Add("@language", DbType.String);
            parlanguageIns.SourceColumn = "Language";
            parlanguageIns.SourceVersion = DataRowVersion.Default;

            SQLiteParameter parCountryIns = adap.InsertCommand.Parameters.Add("@country", DbType.String);
            parCountryIns.SourceColumn = "Country";
            parCountryIns.SourceVersion = DataRowVersion.Default;

            SQLiteParameter playlistIdDel = adap.DeleteCommand.Parameters.Add("@playlistid", DbType.Int32);
            playlistIdDel.SourceColumn = "id";
            playlistIdDel.SourceVersion = DataRowVersion.Default;

            adap.DeleteCommand.Connection = con;
            ds.Tables[0].Columns["datetimesong"].AllowDBNull = false;
            ds.Tables[0].Columns["Author"].AllowDBNull = false;
            ds.Tables[0].Columns["Name"].AllowDBNull = false;
            ds.Tables[0].Columns["duration"].AllowDBNull = false;

            mainGrid.DataSource = ds.Tables[0];
            mainGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            mainGrid.Columns[0].Visible = mainGrid.Columns[5].Visible = mainGrid.Columns[6].Visible = mainGrid.Columns[7].Visible = false;
            mainGrid.Columns[1].HeaderText = "Виконавець";
            mainGrid.Columns[2].HeaderText = "Назва Пісні";
            mainGrid.Columns[3].HeaderText = "Тривалість (в сек.)";
            mainGrid.Columns[4].HeaderText = "Час (гг:хх)";

            mainGrid.Columns[4].DefaultCellStyle.Format = "HH:mm";

            DataGridViewComboBoxColumn coutrycol = new DataGridViewComboBoxColumn();
            coutrycol.DataSource = new Crud().listCountry;
            coutrycol.DataPropertyName = "Country";
            coutrycol.HeaderText = "Країна";
            coutrycol.Name = "Країна";

            DataGridViewComboBoxColumn langcol = new DataGridViewComboBoxColumn();
            langcol.DataSource = new Crud().listLanguage;
            langcol.DataPropertyName = "Language";
            langcol.HeaderText = "Мова";
            langcol.Name = "Мова";

            langcol.SortMode = coutrycol.SortMode = DataGridViewColumnSortMode.Automatic;
            mainGrid.Columns.Add(langcol);
            mainGrid.Columns.Add(coutrycol);

            toolStripStatusLabel1.Text = "Знайдено пісень: " + (mainGrid.Rows.Count - 1);
        }
        public void InitControls()
        {
            dateTimePicker1.Value = DateTime.Today;
            radioComboBox.Items.AddRange(Crud.GetAllRadiostations().ToArray());
            if (radioComboBox.Items.Count > 0)
                radioComboBox.SelectedItem = radioComboBox.Items[0];
        }

        #endregion

        #region events
        private async void saveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(radioComboBox.Text))
            {
                MessageBox.Show("Поле Радіостанція не може бути пустим!\nДодайте радіостанцію через Дані->Радіостанції", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            System.Windows.Forms.Timer tim = new System.Windows.Forms.Timer();
            tim.Interval = 1000;
            tim.Tick += Tim_Tick;
            tim.Start();
            try
            {
                cancelBtn.Enabled = saveBtn.Enabled = radioComboBox.Enabled = dateTimePicker1.Enabled = mainGrid.Enabled = false;
                toolStripStatusLabel1.Text = "Зачекайте...";
                cmdl = new SQLiteCommandBuilder(adap);
                await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        adap.Update(ds.Tables[0].Select(null, null, DataViewRowState.Deleted));
                        adap.Update(ds.Tables[0].Select(null, null, DataViewRowState.ModifiedCurrent));
                        adap.Update(ds.Tables[0].Select(null, null, DataViewRowState.Added));
                    }
                    catch (SQLiteException em)
                    {
                        throw new SQLiteException("Некоректні дані!", em);
                    }
                });
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Replace("unknown error", ""), "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tim.Stop();
            cancelBtn.Enabled = saveBtn.Enabled = radioComboBox.Enabled = dateTimePicker1.Enabled = mainGrid.Enabled = true;
            toolStripStatusLabel1.Text = "Знайдено пісень: " + (mainGrid.Rows.Count - 1);
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
        private void radioComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void mainGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                mainGrid.Rows[e.RowIndex].Cells[6].Value = Crud.ReturnUkrLangForSong(mainGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
            }

            if (e.ColumnIndex == 4)
            {
                if (!(mainGrid.Rows[e.RowIndex].Cells[4].Value is DBNull))
                {
                    mainGrid.Rows[e.RowIndex].Cells[4].Value = dateTimePicker1.Value.Date.AddHours(Convert.ToDateTime(mainGrid.Rows[e.RowIndex].Cells[4].Value).Hour).AddMinutes(Convert.ToDateTime(mainGrid.Rows[e.RowIndex].Cells[4].Value).Minute);
                }
            }
        }
        private void mainGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Перевірте дані в " + (e.RowIndex + 1) + " стрічці \nта в колонці " + mainGrid.Columns[e.ColumnIndex].HeaderText, "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["datetimesong"].Value = dateTimePicker1.Value.Date;

        }
        private void mainGrid_MouseClick(object sender, MouseEventArgs e)
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
        private void PlayListEditForm_Load(object sender, EventArgs e)
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
