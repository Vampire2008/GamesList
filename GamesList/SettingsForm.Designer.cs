namespace GamesList
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.VisMax = new System.Windows.Forms.CheckBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DistrCustText = new System.Windows.Forms.TextBox();
            this.DistrCust = new System.Windows.Forms.RadioButton();
            this.DistrInText = new System.Windows.Forms.TextBox();
            this.DistrIn = new System.Windows.Forms.RadioButton();
            this.DistrRegi = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.MakeCurrent = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.CurrentBase = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(363, 295);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(282, 295);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "ОК";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 289);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.CurrentBase);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.MakeCurrent);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(439, 263);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "База игр";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(342, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Данная база будет автоматически загружаться при старте.\r\nДля создания новой базы " +
    "воспользуйтесь меню в главном окне.";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(404, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 20);
            this.button3.TabIndex = 2;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(123, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(282, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "База по умолчанию:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.VisMax);
            this.tabPage2.Controls.Add(this.numericUpDown2);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(439, 263);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Рецензор";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // VisMax
            // 
            this.VisMax.AutoSize = true;
            this.VisMax.Checked = true;
            this.VisMax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VisMax.Location = new System.Drawing.Point(11, 139);
            this.VisMax.Name = "VisMax";
            this.VisMax.Size = new System.Drawing.Size(385, 17);
            this.VisMax.TabIndex = 7;
            this.VisMax.Text = "Показывать максимальную оценку рядом с текущей (например 9/10).";
            this.VisMax.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown2.Location = new System.Drawing.Point(197, 101);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(38, 20);
            this.numericUpDown2.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Максимальная ваша оценка:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown1.Location = new System.Drawing.Point(197, 73);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(38, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Максимальная оценка рецензора:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(357, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Русских рецензоров вводите в родительном падеже. (Оценка кого?)";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(106, 17);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(325, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Имя рецензора: ";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.DistrCustText);
            this.tabPage3.Controls.Add(this.DistrCust);
            this.tabPage3.Controls.Add(this.DistrInText);
            this.tabPage3.Controls.Add(this.DistrIn);
            this.tabPage3.Controls.Add(this.DistrRegi);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(439, 263);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Региональный издатель";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // DistrCustText
            // 
            this.DistrCustText.Enabled = false;
            this.DistrCustText.Location = new System.Drawing.Point(32, 120);
            this.DistrCustText.Name = "DistrCustText";
            this.DistrCustText.Size = new System.Drawing.Size(246, 20);
            this.DistrCustText.TabIndex = 2;
            // 
            // DistrCust
            // 
            this.DistrCust.AutoSize = true;
            this.DistrCust.Location = new System.Drawing.Point(11, 95);
            this.DistrCust.Name = "DistrCust";
            this.DistrCust.Size = new System.Drawing.Size(94, 17);
            this.DistrCust.TabIndex = 3;
            this.DistrCust.TabStop = true;
            this.DistrCust.Text = "Свой вариант";
            this.DistrCust.UseVisualStyleBackColor = true;
            // 
            // DistrInText
            // 
            this.DistrInText.Location = new System.Drawing.Point(104, 69);
            this.DistrInText.Name = "DistrInText";
            this.DistrInText.Size = new System.Drawing.Size(174, 20);
            this.DistrInText.TabIndex = 4;
            this.DistrInText.Text = "России";
            // 
            // DistrIn
            // 
            this.DistrIn.AutoSize = true;
            this.DistrIn.Checked = true;
            this.DistrIn.Location = new System.Drawing.Point(11, 70);
            this.DistrIn.Name = "DistrIn";
            this.DistrIn.Size = new System.Drawing.Size(86, 17);
            this.DistrIn.TabIndex = 5;
            this.DistrIn.TabStop = true;
            this.DistrIn.Text = "Издатель в ";
            this.DistrIn.UseVisualStyleBackColor = true;
            // 
            // DistrRegi
            // 
            this.DistrRegi.AutoSize = true;
            this.DistrRegi.Location = new System.Drawing.Point(11, 45);
            this.DistrRegi.Name = "DistrRegi";
            this.DistrRegi.Size = new System.Drawing.Size(149, 17);
            this.DistrRegi.TabIndex = 6;
            this.DistrRegi.Text = "Региональный издатель";
            this.DistrRegi.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(250, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Как будет называться региональный издатель:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "MyGames.gdb";
            this.openFileDialog1.Filter = "База игр (*.gdb)|*.gdb";
            // 
            // MakeCurrent
            // 
            this.MakeCurrent.Enabled = false;
            this.MakeCurrent.Location = new System.Drawing.Point(11, 136);
            this.MakeCurrent.Name = "MakeCurrent";
            this.MakeCurrent.Size = new System.Drawing.Size(243, 23);
            this.MakeCurrent.TabIndex = 4;
            this.MakeCurrent.Text = "Сделать текущую базу базой по умолчанию";
            this.MakeCurrent.UseVisualStyleBackColor = true;
            this.MakeCurrent.Click += new System.EventHandler(this.MakeCurrent_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Текущая база:";
            // 
            // CurrentBase
            // 
            this.CurrentBase.Location = new System.Drawing.Point(96, 95);
            this.CurrentBase.Name = "CurrentBase";
            this.CurrentBase.Size = new System.Drawing.Size(335, 38);
            this.CurrentBase.TabIndex = 6;
            this.CurrentBase.Text = "label9";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(447, 330);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Настройки";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox DistrCustText;
        private System.Windows.Forms.RadioButton DistrCust;
        private System.Windows.Forms.TextBox DistrInText;
        private System.Windows.Forms.RadioButton DistrIn;
        private System.Windows.Forms.RadioButton DistrRegi;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox VisMax;
        private System.Windows.Forms.Label CurrentBase;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button MakeCurrent;
    }
}