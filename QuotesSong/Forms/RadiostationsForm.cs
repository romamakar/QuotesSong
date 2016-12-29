using QuotesSong.Properties;
using System;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuotesSong.Forms
{
    public partial class RadiostationsForm : Form
    {
        #region members
        int countRadios = 0;        
        private DataSet ds;
        private SQLiteConnection con;
        private SQLiteDataAdapter adap;
        private SQLiteCommandBuilder cmdl;
        #endregion
        public RadiostationsForm()
        {
            InitializeComponent();
            LoadData();
            TopMost = Crud.ontop;
        }
        #region methods
        public void LoadData()
        {
            con = new SQLiteConnection();
            ds = new DataSet();
            con.ConnectionString = Settings.Default.SongDBConnectionString;
            con.Open();
            adap = new SQLiteDataAdapter("select Id, Name  from Radiostation", con);
            adap.Fill(ds, "Radiostations");
            mainGrid.DataSource = ds.Tables[0];
            mainGrid.Columns[0].Visible = false;
            mainGrid.Columns[1].HeaderText = "Назва радіостанції";            
            mainGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;            
            countRadios = ds.Tables[0].Rows.Count;            
            ds.Tables[0].Columns["Name"].AllowDBNull = false;
            toolStripStatusLabel1.Text = string.Format("Радіостанцій: {0}", countRadios);
        }

        #endregion

        #region events
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
        private async void saveBtn_Click(object sender, EventArgs e)
        {
            Timer tim = new Timer();
            tim.Interval = 1000;
            tim.Tick += Tim_Tick;
            tim.Start();
            try
            {
                cancelBtn.Enabled = saveBtn.Enabled  = mainGrid.Enabled = false;
                toolStripStatusLabel1.Text = "Зачекайте...";
                cmdl = new SQLiteCommandBuilder(adap);
                await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        adap.Update(ds, "Radiostations");
                    }
                    catch (SQLiteException)
                    {
                        throw new SQLiteException("Некоректні дані!");
                    }
                }
                
                );
                Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message.Replace("unknown error", ""), "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tim.Stop();
            cancelBtn.Enabled = saveBtn.Enabled = mainGrid.Enabled = true;
            toolStripStatusLabel1.Text = string.Format("Радіостанцій: {0}", countRadios);
        }
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void mainGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Перевірте дані в " + (e.RowIndex + 1) + " стрічці \nта колонки " + mainGrid.Columns[e.ColumnIndex].HeaderText, "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion
    }
}
