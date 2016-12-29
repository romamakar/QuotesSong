namespace QuotesSong.Forms
{
    partial class SetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listConts = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listLangs = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.quoteUkrCountrs = new System.Windows.Forms.NumericUpDown();
            this.quoteUkrLang = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.deletePlaylist = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.x86radio = new System.Windows.Forms.RadioButton();
            this.x64radio = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioChrome = new System.Windows.Forms.RadioButton();
            this.radioMozilla = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quoteUkrCountrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quoteUkrLang)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SaveBtn);
            this.panel1.Controls.Add(this.CancelBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 398);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 24);
            this.panel1.TabIndex = 0;
            // 
            // SaveBtn
            // 
            this.SaveBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.SaveBtn.Location = new System.Drawing.Point(413, 0);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(63, 24);
            this.SaveBtn.TabIndex = 0;
            this.SaveBtn.Text = "Зберегти";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.CancelBtn.Location = new System.Drawing.Point(476, 0);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(69, 24);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Відміна";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(545, 398);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox4);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(269, 398);
            this.panel4.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listConts);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 117);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(269, 281);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Список країн для пісень";
            // 
            // listConts
            // 
            this.listConts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listConts.Location = new System.Drawing.Point(3, 16);
            this.listConts.Multiline = true;
            this.listConts.Name = "listConts";
            this.listConts.Size = new System.Drawing.Size(263, 262);
            this.listConts.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listLangs);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Список мов для пісень";
            // 
            // listLangs
            // 
            this.listLangs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLangs.Location = new System.Drawing.Point(3, 16);
            this.listLangs.Multiline = true;
            this.listLangs.Name = "listLangs";
            this.listLangs.Size = new System.Drawing.Size(263, 98);
            this.listLangs.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(269, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(276, 398);
            this.panel3.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.quoteUkrCountrs);
            this.groupBox3.Controls.Add(this.quoteUkrLang);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.deletePlaylist);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 117);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(276, 281);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Інші налаштування";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Частка пісень створених в Україні (в%)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Частка пісень українською (в%)";
            // 
            // quoteUkrCountrs
            // 
            this.quoteUkrCountrs.Location = new System.Drawing.Point(7, 108);
            this.quoteUkrCountrs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.quoteUkrCountrs.Name = "quoteUkrCountrs";
            this.quoteUkrCountrs.Size = new System.Drawing.Size(55, 20);
            this.quoteUkrCountrs.TabIndex = 4;
            this.quoteUkrCountrs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // quoteUkrLang
            // 
            this.quoteUkrLang.Location = new System.Drawing.Point(7, 73);
            this.quoteUkrLang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.quoteUkrLang.Name = "quoteUkrLang";
            this.quoteUkrLang.Size = new System.Drawing.Size(55, 20);
            this.quoteUkrLang.TabIndex = 3;
            this.quoteUkrLang.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Автоматично видаляти плейлисти ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "з БД при закритті програми";
            // 
            // deletePlaylist
            // 
            this.deletePlaylist.AutoSize = true;
            this.deletePlaylist.Location = new System.Drawing.Point(7, 22);
            this.deletePlaylist.Name = "deletePlaylist";
            this.deletePlaylist.Size = new System.Drawing.Size(15, 14);
            this.deletePlaylist.TabIndex = 0;
            this.deletePlaylist.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 117);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Браузер";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.x86radio);
            this.groupBox6.Controls.Add(this.x64radio);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(150, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(123, 98);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Розрядність";
            // 
            // x86radio
            // 
            this.x86radio.AutoSize = true;
            this.x86radio.Location = new System.Drawing.Point(6, 19);
            this.x86radio.Name = "x86radio";
            this.x86radio.Size = new System.Drawing.Size(42, 17);
            this.x86radio.TabIndex = 0;
            this.x86radio.TabStop = true;
            this.x86radio.Text = "x86";
            this.x86radio.UseVisualStyleBackColor = true;
            // 
            // x64radio
            // 
            this.x64radio.AutoSize = true;
            this.x64radio.Location = new System.Drawing.Point(6, 43);
            this.x64radio.Name = "x64radio";
            this.x64radio.Size = new System.Drawing.Size(42, 17);
            this.x64radio.TabIndex = 1;
            this.x64radio.TabStop = true;
            this.x64radio.Text = "x64";
            this.x64radio.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioChrome);
            this.groupBox5.Controls.Add(this.radioMozilla);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox5.Location = new System.Drawing.Point(3, 16);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(147, 98);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Назва";
            // 
            // radioChrome
            // 
            this.radioChrome.AutoSize = true;
            this.radioChrome.Location = new System.Drawing.Point(13, 19);
            this.radioChrome.Name = "radioChrome";
            this.radioChrome.Size = new System.Drawing.Size(98, 17);
            this.radioChrome.TabIndex = 2;
            this.radioChrome.TabStop = true;
            this.radioChrome.Text = "Google Chrome";
            this.radioChrome.UseVisualStyleBackColor = true;
            this.radioChrome.CheckedChanged += new System.EventHandler(this.radioChrome_CheckedChanged);
            // 
            // radioMozilla
            // 
            this.radioMozilla.AutoSize = true;
            this.radioMozilla.Location = new System.Drawing.Point(13, 43);
            this.radioMozilla.Name = "radioMozilla";
            this.radioMozilla.Size = new System.Drawing.Size(91, 17);
            this.radioMozilla.TabIndex = 3;
            this.radioMozilla.TabStop = true;
            this.radioMozilla.Text = "Mozilla Firefox";
            this.radioMozilla.UseVisualStyleBackColor = true;
            // 
            // SetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(545, 422);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Налаштування";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quoteUkrCountrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quoteUkrLang)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox listConts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox listLangs;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox deletePlaylist;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown quoteUkrCountrs;
        private System.Windows.Forms.NumericUpDown quoteUkrLang;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton x64radio;
        private System.Windows.Forms.RadioButton x86radio;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioChrome;
        private System.Windows.Forms.RadioButton radioMozilla;
    }
}