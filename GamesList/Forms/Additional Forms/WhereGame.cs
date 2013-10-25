using GamesList.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesList.Forms.Additional_Forms
{
	public partial class WhereGame : Form
	{
		private byte state = 0;
		private decimal IDG;
		public WhereGame(Games game)
		{
			InitializeComponent();
			if (game.WhereStatus != "" && game.WhereStatus != null)
			{
				button1.Text = "Игра возвращена";
				textBox1.Text = game.WhereStatus;
				if (game.WherePhoto != null)//Проверяем наличие постера
				{
					var stream = new MemoryStream(game.WherePhoto);//Получаем поток данных постера из базы
					pictureBox1.Image = Image.FromStream(stream);//Создаём изображение для компоненты из потока
				}
				state = 2;
			}
			IDG = game.ID_Game;
			GameName.Text = game.Name;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Games game;
			switch (state)
			{
				case 0:
					textBox1.Enabled = true;
					label13.Visible = true;
					button1.Text = "Сохранить";
					state = 1;
					textBox1.Focus();
					break;
				case 1:
					if (textBox1.Text != "")
					{
						game = Program.context.Games.Find(IDG);
						game.WhereStatus = textBox1.Text;
						if (pictureBox1.Image != null)
						{
							var stream = new MemoryStream();
							pictureBox1.Image.Save(stream, pictureBox1.Image.RawFormat);
							game.WherePhoto = stream.GetBuffer();
						}
						Program.context.SaveChanges();
						textBox1.Enabled = false;
						label13.Visible = false;
						button1.Text = "Возвращена";
						state = 2;
					}
					break;
				case 2:
					game = Program.context.Games.Find(IDG);
					game.WhereStatus = null;
					game.WherePhoto = null;
					textBox1.Clear();
					pictureBox1.Image = null;
					Program.context.SaveChanges();
					button1.Text = "Дать поиграть";
					state = 0;
					break;
			}
		}

		private void pictureBox1_DoubleClick(object sender, EventArgs e)
		{
			if (state == 1)
			{
				if (ChooseImage.ShowDialog() == DialogResult.OK)
				{
					pictureBox1.Image = Image.FromFile(ChooseImage.FileName);
					label13.Visible = false;
				}
			}
		}
	}
}
