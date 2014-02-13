namespace GamesList.Forms.Additional_Forms
{
	partial class Load
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
			this.win7StyleTreeView1 = new Fhit.Windows.Forms.Win7StyleTreeView(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// win7StyleTreeView1
			// 
			this.win7StyleTreeView1.CheckBoxes = true;
			this.win7StyleTreeView1.HideSelection = false;
			this.win7StyleTreeView1.HotTracking = true;
			this.win7StyleTreeView1.Location = new System.Drawing.Point(12, 27);
			this.win7StyleTreeView1.Name = "win7StyleTreeView1";
			this.win7StyleTreeView1.ShowLines = false;
			this.win7StyleTreeView1.Size = new System.Drawing.Size(237, 347);
			this.win7StyleTreeView1.TabIndex = 1;
			this.win7StyleTreeView1.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.win7StyleTreeView1_BeforeCheck);
			this.win7StyleTreeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.win7StyleTreeView1_AfterCheck);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "MyGames.gdb";
			this.openFileDialog1.Filter = "База игр (*.gdb)|*.gdb";
			this.openFileDialog1.Title = "Открыть файл с базой игр";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(255, 322);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Перекачать";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(172, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Доступные игры для перекачки:";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 383);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(429, 17);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.Text = "Автоматически добавлять отсутствующие данные из вспомогательных таблиц";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(255, 351);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(502, 23);
			this.progressBar1.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(272, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 27);
			this.label2.TabIndex = 7;
			this.label2.Text = "GameName";
			this.label2.Visible = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(286, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(471, 35);
			this.label3.TabIndex = 8;
			this.label3.Text = "Данная игра вызвала проблемы при перекачке связанные с отсутствием у вас дополнит" +
    "ельных данных. Добавить нехватающие данные?\r\n";
			this.label3.Visible = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(286, 296);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(145, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Осталось проблемных игр:";
			this.label4.Visible = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(437, 296);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "KolProblem";
			this.label5.Visible = false;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(289, 134);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(132, 23);
			this.button2.TabIndex = 11;
			this.button2.Text = "Не добавлять игру";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Visible = false;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(565, 134);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(132, 23);
			this.button3.TabIndex = 12;
			this.button3.Text = "Добавить";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Visible = false;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(427, 134);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(132, 23);
			this.button4.TabIndex = 13;
			this.button4.Text = "Не добавлять";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Visible = false;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(289, 174);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(283, 17);
			this.checkBox2.TabIndex = 14;
			this.checkBox2.Text = "Применить выбор ко всем остальным проблемам";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.Visible = false;
			// 
			// Load
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(769, 412);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.win7StyleTreeView1);
			this.Name = "Load";
			this.Text = "Load";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Fhit.Windows.Forms.Win7StyleTreeView win7StyleTreeView1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.CheckBox checkBox2;
	}
}