using System;
using System.Windows.Forms;
using System.Linq;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using GamesBase;
using GamesList.Properties;


namespace GamesList
{
	public partial class ContentForm : Form
	{
		public Boolean fc, IsInit;
		private readonly Games _game;
		private readonly GamesEntities _context = Program.context;
		public ContentForm(Games g)//Точка входа в программу
		{
			InitializeComponent();
			_game = g;
			Text = string.Format("Дополнения для {0}", _game.Name);
			MouseWheel += Wheel;
			IsInit = true;
			Init();
			IsInit = false;
		}

		/// <summary>
		/// Инициализация главной формы. Выполняется загрузка данных из базы данных
		/// </summary>
		private void Init()
		{
			fc = false;
			Changefilter();
			UpdateView();//Настраиваем отображение полей на текущую позицию в базе
		}

		/// <summary>
		/// Обновляет значения полей на форме в соответствии с текущей выбранной игрой
		/// </summary>
		private void UpdateView()
		{
			if (gamesBindingSource.Current == null)//Если не выбрана никакая игра, то обнуляем значения всех полей и выходим из процедуры
			{
				GameName.Text = "Дополнений нет";
				EditionLabel.Text = null;
				JenrName.Text = null;
				DevName.Text = null;
				DistrName.Text = null;
				RFDistrName.Text = null;
				Loc_type.Text = null;
				Online_Protect.Text = null;
				Release_Date.Text = null;
				PlatformsLabel.Text = null;
				last_versionLabel1.Text = null;
				Disks.Text = null;
				BoxLabel.Text = null;
				kol_updatesLabel1.Text = null;
				StatCompl.Text = null;
				pictureBox1.Image = null;
				label32.Visible = true;
				label33.Visible = false;
				SerName.Visible = false;
				DelGame.Enabled = false;
				EditGame.Enabled = false;
				delGamecontextToolStripMenuItem1.Enabled = false;
				editGameContextToolStripMenuItem.Enabled = false;
				return;
			}
			DelGame.Enabled = true;
			EditGame.Enabled = true;
			delGamecontextToolStripMenuItem1.Enabled = true;
			editGameContextToolStripMenuItem.Enabled = true;
			JenrName.Text = ((Games)gamesBindingSource.Current).Genres.Any()
				? String.Join(", ", ((Games)gamesBindingSource.Current).Genres.Select(g => g.Name))
				: "<отсутствует>";

			if (((Games)gamesBindingSource.Current).Original_Name != null)//Если оригинальное название задано, то
			{
				Original.Visible = true;//Делаем поле видимым
				OriginalName.Visible = true;
			}
			else
			{
				Original.Visible = false;//Или невидимым, если отсутствует
				OriginalName.Visible = false;
			}
			if (((Games)gamesBindingSource.Current).Series != null)//Если серия задана, то
			{
				label33.Visible = true;//Делаем поле видимым
				SerName.Visible = true;
				SerName.Text = ((Games)gamesBindingSource.Current).Series.Name;
			}
			else
			{
				label33.Visible = false;//Или невидимым, если отсутствует
				SerName.Visible = false;
			}

			DevName.Text = ((Games)gamesBindingSource.Current).Developers != null ? ((Games)gamesBindingSource.Current).Developers.Name : "<отсутствует>";
			DistrName.Text = ((Games)gamesBindingSource.Current).Publishers != null ? ((Games)gamesBindingSource.Current).Publishers.Name : "<отсутствует>";
			RFDistrName.Text = ((Games)gamesBindingSource.Current).RF_Distributors != null ? ((Games)gamesBindingSource.Current).RF_Distributors.Name : "<отсутствует>";
			//Тоже самое, что и с жанрами, но с онлайн-защитами
			Online_Protect.Text = ((Games)gamesBindingSource.Current).Online_protections.Any()
				? String.Join(", ", ((Games)gamesBindingSource.Current).Online_protections.Select(op => op.Name))
				: "<отсутствуют>";

			switch (((Games)gamesBindingSource.Current).Localisation_Type.ToString())//Выставляем типу локализации в соответствующий текст
			{
				case "0":
					Loc_type.Text = "Нет";
					break;
				case "1":
					Loc_type.Text = "Субтиры";
					break;
				case "2":
					Loc_type.Text = "Полная";
					break;
			}
			//Тоже самое, что и с жанрами, но с платформами
			PlatformsLabel.Text = ((Games)gamesBindingSource.Current).Platforms.Any()
				? String.Join(", ", ((Games)gamesBindingSource.Current).Platforms.Select(p => p.Name))
				: "<отсутствуют>";

			//Проверяем наличие даты релиза и выводим её в нужном формате
			Release_Date.Text = ((Games)gamesBindingSource.Current).Date_Release.HasValue ? ((Games)gamesBindingSource.Current).Date_Release.Value.ToShortDateString() : "<отсутствует>";
			//Проверяем статус пройденности и выводим соответствующуюю строку
			switch (((Games)gamesBindingSource.Current).Status_complite.ToString())
			{
				case "0":
					StatCompl.Text = "Не пройдено";
					StatCompl.ForeColor = Color.Red;
					break;
				case "1":
					StatCompl.Text = "Ожидает прохождения";
					StatCompl.ForeColor = Color.Yellow;
					break;
				case "2":
					StatCompl.Text = "Пройдено (не 100%)";
					StatCompl.ForeColor = Color.Green;
					break;
				case "3":
					StatCompl.Text = "Пройдено";
					StatCompl.ForeColor = Color.Green;
					break;
				case "4":
					StatCompl.Text = "Сетевая игра";
					StatCompl.ForeColor = Color.Blue;
					break;
				case "5":
					StatCompl.Text = "Бесконечно";
					StatCompl.ForeColor = Color.Blue;
					break;
				case "6":
					StatCompl.Text = "Свистелка";
					StatCompl.ForeColor = Color.Blue;
					break;
			}
			//Тоже самое, что и с жанрами, но с дисками
			Disks.Text = ((Games)gamesBindingSource.Current).Game_disks.Any()
				? String.Join(" + ", ((Games)gamesBindingSource.Current).Game_disks.Select(gd => String.Format("{0} {1}", gd.Kol_vo, gd.Disk_types.Name)))
				: "<отсутствуют>";

			BoxLabel.Text = ((Games)gamesBindingSource.Current).Boxes != null ? ((Games)gamesBindingSource.Current).Boxes.Name : "<отсутствует>";
			EditionLabel.Text = ((Games)gamesBindingSource.Current).Editions != null ? ((Games)gamesBindingSource.Current).Editions.Name : "<отсутствует>";
			GameName.Text = ((Games)gamesBindingSource.Current).GetFullName();
			if ((bool)((Games)gamesBindingSource.Current).Game_Type)
			{
				Pirat.Visible = false;
			}
			else
			{
				Pirat.Visible = true;
			}
			if (((Games)gamesBindingSource.Current).Poster != null)//Проверяем наличие постера
			{
				var stream = new MemoryStream(((Games)gamesBindingSource.Current).Poster);//Получаем поток данных постера из базы
				pictureBox1.Image = Image.FromStream(stream);//Создаём изображение для компоненты из потока
				label32.Visible = false;
			}
			else
			{
				pictureBox1.Image = null;//Если постера нет, очищаем изображение
				label32.Visible = true;
			}
			AddContent.Enabled = добавитьДополнениеToolStripMenuItem.Enabled = Stand.Visible = ((Games)gamesBindingSource.Current).TypeContent ?? false;
			if (Settings.Default.VisMax)
			{
				IgromaniaRate.Text = ((Games)gamesBindingSource.Current).Rate_Igromania.HasValue ? string.Format("{0}/{1}", ((Games)gamesBindingSource.Current).Rate_Igromania, Settings.Default.MaxRecenzorRating) : "";
				PersonalRate.Text = ((Games)gamesBindingSource.Current).Rate_person.HasValue ? string.Format("{0}/{1}", ((Games)gamesBindingSource.Current).Rate_person, Settings.Default.MaxYourRating) : "";
				IgromaniaRate.Font = new Font("Microsoft Sans Serif", 40, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
				PersonalRate.Font = new Font("Microsoft Sans Serif", 40, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
			}
			else
			{
				IgromaniaRate.Text = ((Games)gamesBindingSource.Current).Rate_Igromania.ToString();
				PersonalRate.Text = ((Games)gamesBindingSource.Current).Rate_person.ToString();
				IgromaniaRate.Font = new Font("Microsoft Sans Serif", 48, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
				PersonalRate.Font = new Font("Microsoft Sans Serif", 48, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
			}
			button1.Visible = ((Games)gamesBindingSource.Current).Content.Count > 0;

			//Нормализуем вывод информации
			var i = 15;
			while (!IsInit)
			{
				SerName.Top = label33.Top = label33.Visible ? GameName.Bottom + i : GameName.Bottom - label33.Height;
				OriginalName.Top = Original.Top = Original.Visible ? label33.Bottom + i : label33.Bottom - Original.Height;
				EditionLabel.Top = label31.Top = Original.Bottom + i;
				JenrName.Top = label15.Top = label15.Visible ? label31.Bottom + i : label31.Bottom - label15.Height;
				DevName.Top = Dev.Top = Dev.Visible ? JenrName.Bottom + i : JenrName.Bottom - Dev.Height;
				DistrName.Top = label16.Top = Dev.Bottom + i;
				RFDistrName.Top = label14.Top = label16.Bottom + i;
				Loc_type.Top = label24.Top = label24.Visible ? label14.Bottom + i : label14.Bottom - label24.Height;
				Online_Protect.Top = label21.Top = label24.Bottom + i;
				PlatformsLabel.Top = label22.Top = Online_Protect.Bottom + i;
				Release_Date.Top = label23.Top = PlatformsLabel.Bottom + i;
				last_versionLabel1.Top = label28.Top = label28.Visible ? label23.Bottom + i : label23.Bottom - label28.Height;
				Disks.Top = label29.Top = label28.Bottom + i;
				BoxLabel.Top = label30.Top = Disks.Bottom + i;
				kol_updatesLabel1.Top = kol_updatesLabel.Top = kol_updatesLabel.Visible ? label30.Bottom + i : label30.Bottom - kol_updatesLabel.Height;
				decimal different = kol_updatesLabel.Bottom - groupBox1.Bottom + 61;
				//if ((different > -15)&&(different<=0))
				if (different <= 0)
					break;
				different = different / 15;
				i = i - Decimal.ToInt32(Math.Ceiling(different));
			}
		}

		protected void Changefilter()
		{
			var s = textBox1.Text.ToUpper();
			var query = _context.Games.Where(g => (g.ID_Content == _game.ID_Game || g.OriginalGame.ID_Content == _game.ID_Game) &&
				(fc || g.OriginalGame.OriginalGame == null))
				.ToList()
				.OrderBy(g => g.GetContentName())
				.Where(g => String.IsNullOrEmpty(s) || (g.GetContentName().ToUpper().Contains(s)));
			gamesBindingSource.DataSource = query.Any() ? query : new Games[] { }; ;
		}


		private void gamesBindingSource_CurrentChanged(object sender, EventArgs e)
		{
			UpdateView();
		}

		private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
				return;

			var hti = ((DataGridView)sender).HitTest(e.X, e.Y);
			if (hti.Type != DataGridViewHitTestType.Cell)
				return;

			contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
		}

		private void addGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var addC = new AddContent(_game, 0);
			if (addC.ShowDialog() != DialogResult.OK) return;
			Changefilter();
			UpdateView();
		}

		private void delGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (
				MessageBox.Show(
					string.Format("Вы действительно хотите удалить дополнение {0} из базы?", ((Games) gamesBindingSource.Current).Name), "Удаление",
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
			_context.Games.Remove((Games)gamesBindingSource.Current);
			_context.SaveChanges();
			Changefilter();
			UpdateView();
		}

		private void editGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var editGame = new AddContent(_context.Games.Find(((Games)gamesBindingSource.Current).ID_Game), 0);
			editGame.ShowDialog();
			UpdateView();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			Changefilter();
		}

		private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
				return;

			var hti = ((DataGridView)sender).HitTest(e.X, e.Y);
			if (hti.Type != DataGridViewHitTestType.Cell)
				return;

			gamesBindingSource.Position = hti.RowIndex;
		}

		private void DevName_Click(object sender, EventArgs e)
		{
			if (((Games) gamesBindingSource.Current).Developers == null) return;
			var infa = new InformationWindows(0, ((Games)gamesBindingSource.Current).Developers);
			infa.Show();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			if (pictureBox1.Image == null) return;
			var MP = new MaxPoster(pictureBox1.Image) {Text = ((Games) gamesBindingSource.Current).Name};
			MP.Show();
		}

		private void pictureBox4_Click(object sender, EventArgs e)
		{
			if (((Games) gamesBindingSource.Current).Publishers == null) return;
			var infa = new InformationWindows(1, ((Games)gamesBindingSource.Current).Publishers);
			infa.Show();
		}

		private void pictureBox5_Click(object sender, EventArgs e)
		{
			if (((Games) gamesBindingSource.Current).RF_Distributors == null) return;
			var infa = new InformationWindows(2, ((Games)gamesBindingSource.Current).RF_Distributors);
			infa.Show();
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			var infa = new SeriesInf(((Games)gamesBindingSource.Current).Series);
			infa.Show();
		}

		private void сделатьОбычнойИгройToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (
				MessageBox.Show(
					string.Format("Вы правда хотите {0} сделать обычной игрой?", ((Games) gamesBindingSource.Current).Name),
					"Уточнение", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes) return;
			var g = ((Games)gamesBindingSource.Current);
			g.Name = g.GetFullName();
			g.TypeContent = null;
			g.OriginalGame = null;
			g.ID_Content = null;
			_context.SaveChanges();
			Changefilter();
			((GamesForm)Owner).ChangeFilter();
		}

		private void Wheel(object sender, MouseEventArgs e)
		{
			if ((e.X >= dataGridView1.Left) && (e.X <= dataGridView1.Right) && (e.Y <= dataGridView1.Bottom) && (e.Y >= dataGridView1.Top))
				dataGridView1.Focus();
		}

		private void AddContent_Click(object sender, EventArgs e)
		{
			var addC = new AddContent(((Games)gamesBindingSource.Current), 1);
			if (addC.ShowDialog() != DialogResult.OK) return;
			Changefilter();
			UpdateView();
		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			e.Value = ((Games) dataGridView1.Rows[e.RowIndex].DataBoundItem).GetContentName();
		}

		private void toolStripButton1_CheckedChanged(object sender, EventArgs e)
		{
			fc = toolStripButton1.Checked;
			Changefilter();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!((Games) gamesBindingSource.Current).Content.Any()) return;
			var con = new ContentForm((Games)gamesBindingSource.Current)
			{
				Owner = this,
				fc = true,
				toolStripButton1 = {Enabled = false}
			};
			con.Changefilter();
			con.Show();
		}

		private void ContentForm_Shown(object sender, EventArgs e)
		{
			UpdateView();
		}
	}
}
