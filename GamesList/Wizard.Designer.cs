namespace GamesList
{
    partial class Wizard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wizard));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.HelloPage = new AeroWizard.WizardPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BasePage = new AeroWizard.WizardPage();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.OpenButton = new System.Windows.Forms.Button();
            this.OpenPath = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SavePath = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.RatingPage = new AeroWizard.WizardPage();
            this.MaxYourRating = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.MaxRating = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.Recenzor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DistrPage = new AeroWizard.WizardPage();
            this.DistrCustText = new System.Windows.Forms.TextBox();
            this.DistrCust = new System.Windows.Forms.RadioButton();
            this.DistrInText = new System.Windows.Forms.TextBox();
            this.DistrIn = new System.Windows.Forms.RadioButton();
            this.DistrRegi = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.glassExtenderProvider1 = new Microsoft.Win32.DesktopWindowManager.GlassExtenderProvider();
            this.BaseCompl = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.HelloPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.BasePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.RatingPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxYourRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxRating)).BeginInit();
            this.DistrPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.CancelButtonText = "Отмена";
            this.wizardControl1.FinishButtonText = "&Завершить";
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonText = "&Далее";
            this.wizardControl1.Pages.Add(this.HelloPage);
            this.wizardControl1.Pages.Add(this.BasePage);
            this.wizardControl1.Pages.Add(this.RatingPage);
            this.wizardControl1.Pages.Add(this.DistrPage);
            this.wizardControl1.Size = new System.Drawing.Size(576, 481);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "Мастер настройки";
            this.wizardControl1.Finished += new System.EventHandler(this.wizardControl1_Finished);
            // 
            // HelloPage
            // 
            this.HelloPage.Controls.Add(this.pictureBox2);
            this.HelloPage.Controls.Add(this.label2);
            this.HelloPage.Controls.Add(this.label1);
            this.HelloPage.Name = "HelloPage";
            this.HelloPage.NextPage = this.BasePage;
            this.HelloPage.Size = new System.Drawing.Size(529, 326);
            this.HelloPage.TabIndex = 0;
            this.HelloPage.Text = "Приветствие";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GamesList.Properties.Resources.Wizard;
            this.pictureBox2.Location = new System.Drawing.Point(21, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(222, 320);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(301, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 271);
            this.label2.TabIndex = 1;
            this.label2.Text = "Вас приветствует мастер настройки Списка Игр.\r\nЗа несколько шагов мы настроим про" +
    "грамму, в соотвтетствии с вашими вкусами.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Картинка";
            // 
            // BasePage
            // 
            this.BasePage.Controls.Add(this.label6);
            this.BasePage.Controls.Add(this.pictureBox1);
            this.BasePage.Controls.Add(this.label5);
            this.BasePage.Controls.Add(this.OpenButton);
            this.BasePage.Controls.Add(this.OpenPath);
            this.BasePage.Controls.Add(this.SaveButton);
            this.BasePage.Controls.Add(this.label4);
            this.BasePage.Controls.Add(this.SavePath);
            this.BasePage.Controls.Add(this.checkBox1);
            this.BasePage.Controls.Add(this.radioButton2);
            this.BasePage.Controls.Add(this.radioButton1);
            this.BasePage.Controls.Add(this.label3);
            this.BasePage.Name = "BasePage";
            this.BasePage.NextPage = this.RatingPage;
            this.BasePage.Size = new System.Drawing.Size(529, 326);
            this.BasePage.TabIndex = 1;
            this.BasePage.Text = "Выбор базы данных";
            this.BasePage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.BasePage_Commit);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(279, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Введите существующий каталог в одно из полей!";
            this.label6.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(23, 272);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(20, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(486, 36);
            this.label5.TabIndex = 1;
            this.label5.Text = "Эта база будет использоваться для подключения по умолчанию. Потом вы можете измен" +
    "ить параметр.";
            // 
            // OpenButton
            // 
            this.OpenButton.Enabled = false;
            this.OpenButton.Location = new System.Drawing.Point(433, 183);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(24, 23);
            this.OpenButton.TabIndex = 1;
            this.OpenButton.Text = "...";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // OpenPath
            // 
            this.OpenPath.Enabled = false;
            this.OpenPath.Location = new System.Drawing.Point(44, 183);
            this.OpenPath.Name = "OpenPath";
            this.OpenPath.Size = new System.Drawing.Size(392, 23);
            this.OpenPath.TabIndex = 1;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(433, 129);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(24, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "...";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Путь сохранения базы:";
            // 
            // SavePath
            // 
            this.SavePath.Location = new System.Drawing.Point(44, 129);
            this.SavePath.Name = "SavePath";
            this.SavePath.Size = new System.Drawing.Size(392, 23);
            this.SavePath.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(44, 78);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(441, 19);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Включить в базу данные об издателях, разработчиках, издателях в России...";
            this.BaseCompl.SetToolTip(this.checkBox1, "Включить в базу данные об издателях, разработчиках, издателях в России, типах изд" +
        "аний, коробок, дисков, платформ, жанров.");
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(23, 158);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(267, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Открыть существующий файл базы данных";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(23, 53);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(253, 19);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Создать новый пустой файл базы данных";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(320, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Для работы программы требуется файл с базой данных.";
            // 
            // RatingPage
            // 
            this.RatingPage.Controls.Add(this.MaxYourRating);
            this.RatingPage.Controls.Add(this.label10);
            this.RatingPage.Controls.Add(this.MaxRating);
            this.RatingPage.Controls.Add(this.label9);
            this.RatingPage.Controls.Add(this.Recenzor);
            this.RatingPage.Controls.Add(this.label8);
            this.RatingPage.Controls.Add(this.label7);
            this.RatingPage.Name = "RatingPage";
            this.RatingPage.NextPage = this.DistrPage;
            this.RatingPage.Size = new System.Drawing.Size(529, 326);
            this.RatingPage.TabIndex = 2;
            this.RatingPage.Text = "Рейтинги";
            // 
            // MaxYourRating
            // 
            this.MaxYourRating.Location = new System.Drawing.Point(26, 199);
            this.MaxYourRating.Name = "MaxYourRating";
            this.MaxYourRating.Size = new System.Drawing.Size(44, 23);
            this.MaxYourRating.TabIndex = 1;
            this.MaxYourRating.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 181);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(227, 15);
            this.label10.TabIndex = 1;
            this.label10.Text = "Максимально возможная ваша оценка:";
            // 
            // MaxRating
            // 
            this.MaxRating.Location = new System.Drawing.Point(26, 141);
            this.MaxRating.Name = "MaxRating";
            this.MaxRating.Size = new System.Drawing.Size(44, 23);
            this.MaxRating.TabIndex = 1;
            this.MaxRating.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(260, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "Максимально возможная оценка рецензента:";
            // 
            // Recenzor
            // 
            this.Recenzor.Location = new System.Drawing.Point(26, 88);
            this.Recenzor.Name = "Recenzor";
            this.Recenzor.Size = new System.Drawing.Size(212, 23);
            this.Recenzor.TabIndex = 1;
            this.Recenzor.Text = "Игромании";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(502, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "Введите название вашего рецензента (если имя русское, вводите в родительном падеж" +
    "е):";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(23, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(485, 34);
            this.label7.TabIndex = 1;
            this.label7.Text = "В программе можно указывать свою личную оценку игре и оценку какого-либо рецензен" +
    "та ";
            // 
            // DistrPage
            // 
            this.DistrPage.Controls.Add(this.DistrCustText);
            this.DistrPage.Controls.Add(this.DistrCust);
            this.DistrPage.Controls.Add(this.DistrInText);
            this.DistrPage.Controls.Add(this.DistrIn);
            this.DistrPage.Controls.Add(this.DistrRegi);
            this.DistrPage.Controls.Add(this.label12);
            this.DistrPage.Controls.Add(this.label11);
            this.DistrPage.IsFinishPage = true;
            this.DistrPage.Name = "DistrPage";
            this.DistrPage.Size = new System.Drawing.Size(529, 326);
            this.DistrPage.TabIndex = 3;
            this.DistrPage.Text = "Региональный издатель";
            // 
            // DistrCustText
            // 
            this.DistrCustText.Enabled = false;
            this.DistrCustText.Location = new System.Drawing.Point(47, 148);
            this.DistrCustText.Name = "DistrCustText";
            this.DistrCustText.Size = new System.Drawing.Size(246, 23);
            this.DistrCustText.TabIndex = 1;
            // 
            // DistrCust
            // 
            this.DistrCust.AutoSize = true;
            this.DistrCust.Location = new System.Drawing.Point(26, 123);
            this.DistrCust.Name = "DistrCust";
            this.DistrCust.Size = new System.Drawing.Size(100, 19);
            this.DistrCust.TabIndex = 1;
            this.DistrCust.TabStop = true;
            this.DistrCust.Text = "Свой вариант";
            this.DistrCust.UseVisualStyleBackColor = true;
            this.DistrCust.CheckedChanged += new System.EventHandler(this.DistrCust_CheckedChanged);
            // 
            // DistrInText
            // 
            this.DistrInText.Location = new System.Drawing.Point(119, 97);
            this.DistrInText.Name = "DistrInText";
            this.DistrInText.Size = new System.Drawing.Size(174, 23);
            this.DistrInText.TabIndex = 1;
            this.DistrInText.Text = "России";
            // 
            // DistrIn
            // 
            this.DistrIn.AutoSize = true;
            this.DistrIn.Checked = true;
            this.DistrIn.Location = new System.Drawing.Point(26, 98);
            this.DistrIn.Name = "DistrIn";
            this.DistrIn.Size = new System.Drawing.Size(87, 19);
            this.DistrIn.TabIndex = 1;
            this.DistrIn.TabStop = true;
            this.DistrIn.Text = "Издатель в ";
            this.DistrIn.UseVisualStyleBackColor = true;
            this.DistrIn.CheckedChanged += new System.EventHandler(this.DistrIn_CheckedChanged);
            // 
            // DistrRegi
            // 
            this.DistrRegi.AutoSize = true;
            this.DistrRegi.Location = new System.Drawing.Point(26, 73);
            this.DistrRegi.Name = "DistrRegi";
            this.DistrRegi.Size = new System.Drawing.Size(157, 19);
            this.DistrRegi.TabIndex = 1;
            this.DistrRegi.Text = "Региональный издатель";
            this.DistrRegi.UseVisualStyleBackColor = true;
            this.DistrRegi.CheckedChanged += new System.EventHandler(this.DistrRegi_CheckedChanged);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(23, 262);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(485, 40);
            this.label12.TabIndex = 1;
            this.label12.Text = "Данную опцию нельзя отключить, если в вашей стране нет такого, просто выбирайте з" +
    "начение <отсутствует>.";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(23, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(485, 37);
            this.label11.TabIndex = 1;
            this.label11.Text = "В программе также предусмотрен выбор регионального издателя игры.\r\nВы можете выбр" +
    "ать как будет называться эта опция.";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "MyGames.gdb";
            this.openFileDialog1.Filter = "База игр (*.gdb)|*.gdb";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "MyGames.gdb";
            this.saveFileDialog1.Filter = "База игр (*.gdb)|*.gdb";
            // 
            // Wizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 481);
            this.Controls.Add(this.wizardControl1);
            this.glassExtenderProvider1.SetGlassMargins(this, new System.Windows.Forms.Padding(0));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Wizard";
            this.Text = "Мастер настройки";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.HelloPage.ResumeLayout(false);
            this.HelloPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.BasePage.ResumeLayout(false);
            this.BasePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.RatingPage.ResumeLayout(false);
            this.RatingPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxYourRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxRating)).EndInit();
            this.DistrPage.ResumeLayout(false);
            this.DistrPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage HelloPage;
        private Microsoft.Win32.DesktopWindowManager.GlassExtenderProvider glassExtenderProvider1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AeroWizard.WizardPage BasePage;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolTip BaseCompl;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SavePath;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.TextBox OpenPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label5;
        private AeroWizard.WizardPage RatingPage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Recenzor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown MaxRating;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown MaxYourRating;
        private AeroWizard.WizardPage DistrPage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton DistrIn;
        private System.Windows.Forms.RadioButton DistrRegi;
        private System.Windows.Forms.TextBox DistrInText;
        private System.Windows.Forms.RadioButton DistrCust;
        private System.Windows.Forms.TextBox DistrCustText;
        private System.Windows.Forms.PictureBox pictureBox2;

    }
}