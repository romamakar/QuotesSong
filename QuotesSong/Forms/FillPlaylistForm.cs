using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuotesSong.Forms
{
    public partial class FillPlaylistForm : Form
    {
        #region members
        ComboBox[] boxes;
        Dictionary<int, ComboBox> idCombos = new Dictionary<int, ComboBox>();
        string[] items = { "Виконавець", "Назва пісні", "Тривалість (хх:сс)", "Час (гг:хх)" };
        #endregion

        public FillPlaylistForm()
        {
            InitializeComponent();
            boxes = new[] { firstComboBox, secondComboBox, thirdComboBox, forthComboBox };
            initRadioComboBox();
            TopMost = Crud.ontop;
        }

        #region methods
        public async void ParseAndSave()
        {
            System.Windows.Forms.Timer tim = new System.Windows.Forms.Timer();
            tim.Interval = 1000;
            tim.Tick += Tim_Tick;
            tim.Start();
            textBox1.Enabled = cancelBtn.Enabled = saveBtn.Enabled = openBtn.Enabled = radioComboBox.Enabled = firstComboBox.Enabled = secondComboBox.Enabled = thirdComboBox.Enabled = forthComboBox.Enabled = dateTimePicker1.Enabled = false;
            toolStripStatusLabel1.Text = "Зачекайте...";
            string[] lines = textBox1.Text.Split('\n');
            Crud.InsertIntoRadiostation(radioComboBox.Text);
            foreach (var line in lines)
            {
                string curline = line.Replace(" - ", "|");
                string[] rows = curline.Split('|');
                string name = string.Empty;
                string author = string.Empty;
                int duration = 0;
                DateTime date = dateTimePicker1.Value.Date;
                try
                {
                    for (int i = 0; i < boxes.Count(); i++)
                    {
                        if (boxes[i].Text == "Виконавець")
                        {
                            author = rows[i];
                        }
                        else if (boxes[i].Text == "Назва пісні")
                        {
                            name = rows[i];
                        }
                        else if (boxes[i].Text == "Тривалість (хх:сс)")
                        {
                            duration = Crud.GetMinAndSecFromText(rows[i]);
                        }

                        else if (boxes[i].Text == "Час (гг:хх)")
                        {
                            int hour = Convert.ToInt32(rows[i].Split(':')[0]);
                            int minutes = Convert.ToInt32(rows[i].Split(':')[1]);
                            date = dateTimePicker1.Value.Date.AddHours(hour).AddMinutes(minutes);
                        }
                    }
                }
                catch (Exception)
                {
                    tim.Stop();
                    toolStripStatusLabel1.Text = "Помилка: некоректні дані";
                    MessageBox.Show("Перевірте коректність даних!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Enabled = cancelBtn.Enabled = saveBtn.Enabled = openBtn.Enabled = radioComboBox.Enabled = firstComboBox.Enabled = secondComboBox.Enabled = thirdComboBox.Enabled = forthComboBox.Enabled = dateTimePicker1.Enabled = true;
                    return;
                }

                string lang = Crud.ReturnUkrLangForSong(name);
                try
                {
                    string str = radioComboBox.Text;
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            Crud.InsertIntoSongs(name, author, string.Empty, lang);
                            Crud.InsertIntoPlaylist(name, author, str, duration, "Manual", date);
                        }

                        catch (SQLiteException)
                        {
                            throw new SQLiteException("Некоректні дані!");
                        }
                    });
                    toolStripStatusLabel1.Text = "Статус: Успішно";
                    Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message.Replace("unknown error", ""), "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    toolStripStatusLabel1.Text = "Статус: Помилка";
                }
                textBox1.Enabled = cancelBtn.Enabled = saveBtn.Enabled = openBtn.Enabled = radioComboBox.Enabled = firstComboBox.Enabled = secondComboBox.Enabled = thirdComboBox.Enabled = dateTimePicker1.Enabled = false;
                tim.Stop();
                Crud.LoadEmptyLangWindow();
            }
        }        
        public void initRadioComboBox()
        {
            radioComboBox.Items.AddRange(Crud.GetAllRadiostations().ToArray());
            if (radioComboBox.Items.Count > 0)
                radioComboBox.SelectedItem = radioComboBox.Items[0];
            firstComboBox.Items.AddRange(items);
            secondComboBox.Items.AddRange(items);
            thirdComboBox.Items.AddRange(items);
            forthComboBox.Items.AddRange(items);
            firstComboBox.SelectedItem = items[0];
            secondComboBox.SelectedItem = items[1];
            thirdComboBox.SelectedItem = items[2];
            forthComboBox.SelectedItem = items[3];
            idCombos.Add(0, firstComboBox);
            idCombos.Add(1, secondComboBox);
            idCombos.Add(2, thirdComboBox);
            idCombos.Add(3, forthComboBox);
        }
        #endregion

        #region events
        private void openBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);
            }
        }
        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(radioComboBox.Text))
            {
                MessageBox.Show("Введіть назву радіостанції", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Дані не заповнено", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ParseAndSave();
        }
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void forthComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            int id = 0;
            var cmb = idCombos.Where(x => x.Value == combo).Count();
            if (cmb == 0)
                return;
            id = idCombos.Where(x => x.Value == combo).FirstOrDefault().Key;
            var boxesEnd = boxes.Where(x => x != combo);
            var combotoChanged = boxesEnd.Where(x => x.Text == combo.Text).FirstOrDefault();
            if (combotoChanged != null)
            {
                combotoChanged.Text = items[id];

                for (int i = 0; i < boxes.Count(); i++)
                {
                    items[i] = boxes[i].Text;
                }
            }
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

        #endregion 
    }
}
