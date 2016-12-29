namespace QuotesSong.Forms
{
    partial class FillPlaylistForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FillPlaylistForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.radioComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.forthComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.thirdComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.secondComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.firstComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openBtn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.radioComboBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.saveBtn);
            this.panel1.Controls.Add(this.cancelBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 312);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 26);
            this.panel1.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(293, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(122, 20);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // radioComboBox
            // 
            this.radioComboBox.FormattingEnabled = true;
            this.radioComboBox.Location = new System.Drawing.Point(80, 3);
            this.radioComboBox.Name = "radioComboBox";
            this.radioComboBox.Size = new System.Drawing.Size(168, 21);
            this.radioComboBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Дата";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Радіостанція";
            // 
            // saveBtn
            // 
            this.saveBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveBtn.Location = new System.Drawing.Point(574, 0);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(84, 26);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Завантажити";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelBtn.Location = new System.Drawing.Point(658, 0);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(86, 26);
            this.cancelBtn.TabIndex = 0;
            this.cancelBtn.Text = "Відміна";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.forthComboBox);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.thirdComboBox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.secondComboBox);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.firstComboBox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.openBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(744, 39);
            this.panel2.TabIndex = 1;
            // 
            // forthComboBox
            // 
            this.forthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.forthComboBox.FormattingEnabled = true;
            this.forthComboBox.Location = new System.Drawing.Point(531, 10);
            this.forthComboBox.Name = "forthComboBox";
            this.forthComboBox.Size = new System.Drawing.Size(121, 21);
            this.forthComboBox.TabIndex = 9;
            this.forthComboBox.SelectedValueChanged += new System.EventHandler(this.forthComboBox_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(509, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = " - ";
            // 
            // thirdComboBox
            // 
            this.thirdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thirdComboBox.FormattingEnabled = true;
            this.thirdComboBox.Location = new System.Drawing.Point(378, 10);
            this.thirdComboBox.Name = "thirdComboBox";
            this.thirdComboBox.Size = new System.Drawing.Size(121, 21);
            this.thirdComboBox.TabIndex = 7;
            this.thirdComboBox.SelectedValueChanged += new System.EventHandler(this.forthComboBox_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(356, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = " - ";
            // 
            // secondComboBox
            // 
            this.secondComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secondComboBox.FormattingEnabled = true;
            this.secondComboBox.Location = new System.Drawing.Point(229, 10);
            this.secondComboBox.Name = "secondComboBox";
            this.secondComboBox.Size = new System.Drawing.Size(121, 21);
            this.secondComboBox.TabIndex = 5;
            this.secondComboBox.SelectedValueChanged += new System.EventHandler(this.forthComboBox_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(207, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = " - ";
            // 
            // firstComboBox
            // 
            this.firstComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.firstComboBox.FormattingEnabled = true;
            this.firstComboBox.Location = new System.Drawing.Point(80, 10);
            this.firstComboBox.Name = "firstComboBox";
            this.firstComboBox.Size = new System.Drawing.Size(121, 21);
            this.firstComboBox.TabIndex = 3;
            this.firstComboBox.SelectedValueChanged += new System.EventHandler(this.forthComboBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Порядок - ";
            // 
            // openBtn
            // 
            this.openBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.openBtn.Location = new System.Drawing.Point(658, 0);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(86, 39);
            this.openBtn.TabIndex = 1;
            this.openBtn.Text = "Витягнути з файлу";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(744, 273);
            this.panel3.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(744, 273);
            this.textBox1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Текстові файли|*.txt";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 338);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(744, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(46, 17);
            this.toolStripStatusLabel1.Text = "Статус:";
            // 
            // FillPlaylistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 360);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FillPlaylistForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Заповнити вручну";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox radioComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox thirdComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox secondComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox firstComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox forthComboBox;
        private System.Windows.Forms.Label label6;
    }
}