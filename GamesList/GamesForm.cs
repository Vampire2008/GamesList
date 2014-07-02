using System;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using GamesBase;
using GamesList.Forms.Additional_Forms;
using GamesList.Properties;


namespace GamesList
{
	public partial class GamesForm : Form
	{
		private Boolean fp, fd, frf, fop, fg, fpl, fs, fyr, fir, fda, fl, fc, ef, flp;
		private Boolean IsInit;
		private GamesEntities _context = Program.context;
		public GamesForm()//Точка входа в программу
		{
			InitializeComponent();
			/*var Query = from Games in _context.Games
						select Games;
			gamesBindingSource.DataSource = Query.ToList();*/
			Text = string.Format("Список игр - {0}", Path.GetFileName(Program.CurrentBase));
			label9.Text = label26.Text = string.Format("Рейтинг {0}:", Settings.Default.Recenzor);
			label1.Text = label14.Text = string.Format("{0}:", Settings.Default.DistrReg);
			numericUpDown1.Maximum = numericUpDown2.Maximum = Settings.Default.MaxYourRating;
			numericUpDown3.Maximum = numericUpDown4.Maximum = Settings.Default.MaxRecenzorRating;
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
			fp = fd = frf = fop = fg = fpl = fs = fyr = fir = fda = fl = flp = true;
			fc = ef = false;
			//Загружаем игры из базы
			//gamesBindingSource.DataSource = _context.Games.Local.ToBindingList().Where(g => g.ID_Content==null).OrderBy(g => g.Name);
			//_context.Games.Load();
			/*var Query = from Games in _context.Games
						where Games.ID_Content == null
						orderby Games.Name
						select Games;
			gamesBindingSource.DataSource = Query.ToList();*/

			//gamesBindingSource.Filter = "Name = 'DooM'";
			//Загружаем издателей из базы
			var LP = new List<Publishers> {new Publishers {Name = "<не важно>", Id_Publisher = 0}};
			LP.AddRange(_context.Publishers.OrderBy(p => p.Name).ToArray());
			publishersBindingSource.DataSource = LP;
			// publishersBindingSource.DataSource = _context.Publishers.Local.ToBindingList();
			//_context.Publishers.Load();
			//Загружаем разработчиков из базы
			var LD = new List<Developers> {new Developers {Name = "<не важно>", ID_Developer = 0}};
			LD.AddRange(_context.Developers.OrderBy(p => p.Name).ToArray());
			developersBindingSource.DataSource = LD;
			//developersBindingSource.DataSource = _context.Developers.Local.ToBindingList();
			//rogram.context.Developers.Load();
			//Загружаем издателей в России из базы
			var LRF = new List<RF_Distributors> {new RF_Distributors {Name = "<не важно>", ID_RF_Distributor = 0}};
			LRF.AddRange(_context.RF_Distributors.OrderBy(p => p.Name).ToArray());
			rFDistributorsBindingSource.DataSource = LRF;
			//rFDistributorsBindingSource.DataSource = _context.RF_Distributors.Local.ToBindingList();
			//_context.RF_Distributors.Load();
			//Загружаем виды онлайн-защит из базы
			var LOP = new List<Online_protections> { new Online_protections { Name = "<не важно>", ID_Protect = 0 } };
			LOP.AddRange(_context.Online_protections.OrderBy(op => op.Name).ToArray());
			onlineprotectionsBindingSource.DataSource = LOP;
			//onlineprotectionsBindingSource.DataSource = _context.Online_protections.Local.ToBindingList();
			//_context.Online_protections.Load();
			//Загружаем жанры из базы
			var LG = new List<Genres> {new Genres {Name = "<не важно>", ID_Genre = 0}};
			LG.AddRange(_context.Genres.ToArray());
			genresBindingSource.DataSource = LG;
			//genresBindingSource.DataSource = _context.Genres.Local.ToBindingList();
			//_context.Genres.Load();
			//Загружаем платформы из базы
			var LPL = new List<Platforms> {new Platforms {Name = "<не важно>", ID_Platform = 0}};
			LPL.AddRange(_context.Platforms.ToArray());
			platformsBindingSource.DataSource = LPL;
			//platformsBindingSource.DataSource = _context.Platforms.Local.ToBindingList();
			//_context.Platforms.Load();
			comboBox10.SelectedIndex = 0;
			comboBox7.SelectedIndex = 0;
			comboBox8.SelectedIndex = 0;
			ChangeFilter();
			UpdateView();//Настраиваем отображение полей на текущую позицию в базе
		}

		/// <summary>
		/// Обновляет значения полей на форме в соответствии с текущей выбранной игрой
		/// </summary>
		private void UpdateView()
		{
			if (gamesBindingSource.Current == null)//Если не выбрана никакая игра, то обнуляем значения всех полей и выходим из процедуры
			{
				GameName.Text = "Игор нет";
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
				button1.Visible = false;
				viewContent.Enabled = false;
				viewContentToolStripMenuItem.Enabled = false;
				addContentToolStripMenuItem.Enabled = false;
				DelGame.Enabled = false;
				EditGame.Enabled = false;
				delGameToolStripMenuItem.Enabled = false;
				delGamecontextToolStripMenuItem1.Enabled = false;
				editGameContextToolStripMenuItem.Enabled = false;
				editGameToolStripMenuItem.Enabled = false;
				toolStripButton1.Text = "Игор нет";
				return;
			}
			addContentToolStripMenuItem.Enabled = true;
			DelGame.Enabled = true;
			EditGame.Enabled = true;
			delGameToolStripMenuItem.Enabled = true;
			delGamecontextToolStripMenuItem1.Enabled = true;
			editGameContextToolStripMenuItem.Enabled = true;
			editGameToolStripMenuItem.Enabled = true;
			JenrName.Text = "";//Очищаем строку
			if (((Games)gamesBindingSource.Current).Localisation_Type == -1)
			{
				JenrName.Visible = false;
				label15.Visible = false;
				label33.Visible = false;
				SerName.Visible = false;
				DevName.Visible = false;
				Dev.Visible = false;
				label27.Visible = false;
				StatCompl.Visible = false;
				label24.Visible = false;
				Loc_type.Visible = false;
				button1.Visible = false;
				viewContent.Enabled = false;
				viewContentToolStripMenuItem.Enabled = false;
				label28.Visible = false;
				kol_updatesLabel.Visible = false;
				kol_updatesLabel1.Visible = false;
				label32.Visible = false;
				label26.Visible = false;
				IgromaniaRate.Visible = false;
				label25.Visible = false;
				PersonalRate.Visible = false;
				CollectedLabel.Visible = true;
				gamesListBox.Visible = true;
				var q = ((Games)gamesBindingSource.Current).GamesInCollect
						.OrderBy(g => g.Name);
				gamesBindingSource1.DataSource = q.ToList();
				viewContentToolStripMenuItem.Text = button1.Text = viewContent.Text = "Просмотр игр";
				editGameToolStripMenuItem.Text = editGameContextToolStripMenuItem.Text = "Редактировать сборник";
				delGameToolStripMenuItem.Text = delGamecontextToolStripMenuItem1.Text = "Удалить сборник";
				addContentToolStripMenuItem.Enabled = addContentContextToolStripMenuItem.Enabled = сделатьДополнениемToolStripMenuItem.Enabled = false;
			}
			else
			{
				if (((Games)gamesBindingSource.Current).ID_Content != null)
				{
					editGameToolStripMenuItem.Text = editGameContextToolStripMenuItem.Text = "Редактировать дополнение";
					сделатьДополнениемToolStripMenuItem.Text = "Сделать обычной игрой";
					var f = ((Games)gamesBindingSource.Current).TypeContent ?? false;
					if (f)
						addContentToolStripMenuItem.Enabled = addContentContextToolStripMenuItem.Enabled = true;
					else
						addContentToolStripMenuItem.Enabled = addContentContextToolStripMenuItem.Enabled = false;
				}
				else
				{
					addContentToolStripMenuItem.Enabled = addContentContextToolStripMenuItem.Enabled = true;
					editGameToolStripMenuItem.Text = editGameContextToolStripMenuItem.Text = "Редактировать игру";
					сделатьДополнениемToolStripMenuItem.Text = "Сделать дополнением";
				}
				viewContentToolStripMenuItem.Text = button1.Text = viewContent.Text = "Просмотр дополнений";
				delGameToolStripMenuItem.Text = delGamecontextToolStripMenuItem1.Text = "Удалить игру";
				сделатьДополнениемToolStripMenuItem.Enabled = true;
				CollectedLabel.Visible = false;
				gamesListBox.Visible = false;
				label26.Visible = true;
				IgromaniaRate.Visible = true;
				label25.Visible = true;
				PersonalRate.Visible = true;
				label28.Visible = true;
				kol_updatesLabel.Visible = true;
				kol_updatesLabel1.Visible = true;
				label32.Visible = true;
				JenrName.Visible = true;
				label15.Visible = true;
				JenrName.Text = ((Games)gamesBindingSource.Current).Genres.Any() 
					? String.Join(", ", ((Games) gamesBindingSource.Current).Genres.Select(g => g.Name))
					: "<отсутствует>";

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
				Dev.Visible = true;
				DevName.Visible = true;
				DevName.Text = ((Games)gamesBindingSource.Current).Developers != null ? ((Games)gamesBindingSource.Current).Developers.Name : "<отсутствует>";
				label27.Visible = true;
				StatCompl.Visible = true;
				//Проверяем статус пройденности и выводим соответствующуюю строку
				switch (((Games)gamesBindingSource.Current).Status_complite.ToString())
				{
					case "0":
						StatCompl.Text = "Не пройдено";
						StatCompl.ForeColor = Color.Red;
						break;
					case "1":
						StatCompl.Text = "Ожидает прохождения";
						StatCompl.ForeColor = Color.Orange;
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
				label24.Visible = true;
				Loc_type.Visible = true;
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
			}
			if ((((Games)gamesBindingSource.Current).Content.Any()) || (((Games)gamesBindingSource.Current).GamesInCollect.Any()))
			{
				button1.Visible = true;
				viewContent.Enabled = true;
				viewContentToolStripMenuItem.Enabled = true;
			}
			else
			{
				button1.Visible = false;
				viewContent.Enabled = false;
				viewContentToolStripMenuItem.Enabled = false;
			}


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

			DistrName.Text = ((Games)gamesBindingSource.Current).Publishers != null ? ((Games)gamesBindingSource.Current).Publishers.Name : "<отсутствует>";
			RFDistrName.Text = ((Games)gamesBindingSource.Current).RF_Distributors != null ? ((Games)gamesBindingSource.Current).RF_Distributors.Name : "<отсутствует>";
			//Тоже самое, что и с жанрами, но с онлайн-защитами
			Online_Protect.Text = ((Games) gamesBindingSource.Current).Online_protections.Any()
				? String.Join(", ", ((Games) gamesBindingSource.Current).Online_protections.Select(op => op.Name))
				: "<отсутствуют>";

			//Тоже самое, что и с жанрами, но с платформами
			PlatformsLabel.Text = ((Games)gamesBindingSource.Current).Platforms.Any()
				? String.Join(", ",((Games)gamesBindingSource.Current).Platforms.Select(p => p.Name))
				: "<отсутствуют>";
			//Проверяем наличие даты релиза и выводим её в нужном формате
			Release_Date.Text = ((Games)gamesBindingSource.Current).Date_Release.HasValue ? ((Games)gamesBindingSource.Current).Date_Release.Value.ToLongDateString() : "<отсутствует>";

			//Тоже самое, что и с жанрами, но с дисками
			Disks.Text = ((Games)gamesBindingSource.Current).Game_disks.Any()
				? String.Join(" + ", ((Games)gamesBindingSource.Current).Game_disks.Select(gd => String.Format("{0} {1}",gd.Kol_vo, gd.Disk_types.Name)))
				: "<отсутствуют>";

			BoxLabel.Text = ((Games)gamesBindingSource.Current).Boxes != null ? ((Games)gamesBindingSource.Current).Boxes.Name : "<отсутствует>";
			EditionLabel.Text = ((Games)gamesBindingSource.Current).Editions != null ? ((Games)gamesBindingSource.Current).Editions.Name : "<отсутствует>";

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
			if (((Games)gamesBindingSource.Current).ID_Content != null)
			{

			}
			toolStripButton1.Text = string.Format("Кол-во игр: {0}", _context.Games.Count(g => g.Localisation_Type > -1 && g.Status_complite != 6));
			if (Settings.Default.VisMax)
			{
				IgromaniaRate.Text = ((Games)gamesBindingSource.Current).Rate_Igromania.HasValue ? string.Format("{0}/{1}", ((Games)gamesBindingSource.Current).Rate_Igromania, Settings.Default.MaxRecenzorRating) : "";
				PersonalRate.Text = ((Games)gamesBindingSource.Current).Rate_person.HasValue ? string.Format("{0}/{1}", ((Games)gamesBindingSource.Current).Rate_person, Settings.Default.MaxYourRating) : "";
				IgromaniaRate.Font = new Font("Microsoft Sans Serif", 38, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
				PersonalRate.Font = new Font("Microsoft Sans Serif", 38, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
			}
			else
			{
				IgromaniaRate.Text = ((Games)gamesBindingSource.Current).Rate_Igromania.ToString();
				PersonalRate.Text = ((Games)gamesBindingSource.Current).Rate_person.ToString();
				IgromaniaRate.Font = new Font("Microsoft Sans Serif", 48, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
				PersonalRate.Font = new Font("Microsoft Sans Serif", 48, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
			}
			Stand.Visible = ((Games)gamesBindingSource.Current).TypeContent ?? false;
			GameName.Text = ((Games)gamesBindingSource.Current).GetFullName();
			Pirat.Visible = !(bool) ((Games) gamesBindingSource.Current).Game_Type;
			Where.Visible = !string.IsNullOrEmpty(((Games)gamesBindingSource.Current).WhereStatus);

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

		public void ChangeFilter()
		{
			if (ef || (fp && fd && frf && fop && fg && fpl && fs && fyr && fir && fda && fl && !fc && flp))
			{
				ovalShape1.FillColor = Color.White;
			}
			else
			{
				ovalShape1.FillColor = Color.Green;
			}
			double? n1 = Decimal.ToDouble(numericUpDown1.Value);
			double? n2 = Decimal.ToDouble(numericUpDown2.Value);
			double? n3 = Decimal.ToDouble(numericUpDown3.Value);
			double? n4 = Decimal.ToDouble(numericUpDown4.Value);
			var s = textBox1.Text.ToUpper();
			var filterLicence = comboBox8.SelectedIndex == 1;
			try
			{
				var GQuery = _context.Games.Where(g => ((!ef && fc) || g.ID_Content == null) &&
				                                        (ef || (
					                                        (fp  || g.ID_Publisher == (decimal?) comboBox2.SelectedValue) &&
					                                        (fd  || g.ID_Developer == (decimal?) comboBox3.SelectedValue) &&
					                                        (frf || g.ID_RF_Distributor == (decimal?) comboBox1.SelectedValue) &&
					                                        (fop || g.Online_protections.Select(o => o.ID_Protect).Contains((decimal) comboBox4.SelectedValue)) &&
					                                        (fg  || g.Genres.Select(gn => gn.ID_Genre).Contains((decimal) comboBox5.SelectedValue)) &&
					                                        (fpl || g.Platforms.Select(p => p.ID_Platform).Contains((decimal) comboBox6.SelectedValue)) &&
					                                        (fs  || g.Status_complite == (comboBox7.SelectedIndex - 1)) &&
					                                        (fl  || g.Localisation_Type == (comboBox10.SelectedIndex - 1)) &&
					                                        (fyr || (g.Rate_person >= n1 && g.Rate_person <= n2)) &&
					                                        (fir || (g.Rate_Igromania >= n3 && g.Rate_Igromania <= n4)) &&
					                                        (fda || (g.Date_Release >= dateTimePicker1.Value && g.Date_Release <= dateTimePicker2.Value)) &&
					                                        (flp || g.Game_Type == filterLicence))
															))
											.ToList()
											.OrderBy(g => g.GetFullName())
											.Where(g => (s == "") || (g.GetFullName().ToUpper().Contains(s)) || ((g.Original_Name != null) && (g.Original_Name.ToUpper().Contains(s))));
				gamesBindingSource.DataSource = GQuery.Any() ? GQuery : new Games[] { };
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void managePublishersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form managePublisher = new ManagePDL(0);
			managePublisher.ShowDialog();
		}

		private void GamesForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void manageDevelopersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form managePublisher = new ManagePDL(1);
			managePublisher.ShowDialog();
		}

		private void manageRFDistrToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form managePublisher = new ManagePDL(2);
			managePublisher.ShowDialog();
		}

		private void manageGenresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form manageGenres = new ManageGEB(0);
			manageGenres.ShowDialog();
		}

		private void manageOnlineProtectsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form manageOnlineProtections = new ManageOPOS(0);
			manageOnlineProtections.ShowDialog();
		}

		private void managePatformsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form managePlatforms = new ManageOPOS(1);
			managePlatforms.ShowDialog();
		}

		private void manageDiskTypesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form manageDiskTypes = new ManageDiskTypes();
			manageDiskTypes.ShowDialog();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox2.SelectedValue == null) return;
			if ((decimal)comboBox2.SelectedValue == 0)
			{
				fp = true;
			}
			else
			{
				fp = false;
				ovalShape1.FillColor = Color.Green;
			}
			ChangeFilter();
		}

		private void gamesBindingSource_CurrentChanged(object sender, EventArgs e)
		{
			UpdateView();
		}

		private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
		{
			//ОбновитьВид();
			if (e.Button != MouseButtons.Right)
				return;

			var hti = ((DataGridView)sender).HitTest(e.X, e.Y);
			if (hti.Type != DataGridViewHitTestType.Cell)
				return;

			contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
		}

		private void addGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
				var добавитьИгру = new AddGame(null);
				добавитьИгру.ShowDialog();
				ChangeFilter();
				UpdateView();
		}

		private void delGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if ((gamesBindingSource.Current == null) ||
			    (MessageBox.Show(
				    string.Format("Вы действительно хотите удалить игру {0} из базы?", ((Games) gamesBindingSource.Current).Name),
				    "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)) return;
			_context.Games.Remove((Games)gamesBindingSource.Current);
			_context.SaveChanges();
			ChangeFilter();
			UpdateView();
		}

		private void manageEditionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var manageEditions = new ManageGEB(1);
			manageEditions.ShowDialog();
		}

		private void manageBoxesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var manageBoxes = new ManageGEB(2);
			manageBoxes.ShowDialog();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var about = new AboutBox();
			about.Show();
		}

		private void editGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form editGame;
			if (((Games)gamesBindingSource.Current).Localisation_Type > -1)
			{
				if (((Games)gamesBindingSource.Current).ID_Content != null)
				{
					editGame = new AddContent(_context.Games.Find(((Games)gamesBindingSource.Current).ID_Game), 0)
					{
						Text = string.Format("Редактирование дополнения {0}", ((Games) gamesBindingSource.Current).Name)
					};
				}
				else
				{
					editGame = new AddGame(_context.Games.Find(((Games)gamesBindingSource.Current).ID_Game))
					{
						Text = string.Format("Редактирование игры {0}", ((Games) gamesBindingSource.Current).Name)
					};
				}
			}
			else
			{
				editGame = new AddCollect(_context.Games.Find(((Games)gamesBindingSource.Current).ID_Game))
				{
					Text = string.Format("Редактирование сборника {0}", ((Games) gamesBindingSource.Current).Name)
				};
			}
			editGame.ShowDialog();
			ChangeFilter();
			UpdateView();
		}

		private void createBaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
			Cursor = Cursors.WaitCursor;
			if (File.Exists(saveFileDialog1.FileName))
			{
				try
				{
					File.Delete(saveFileDialog1.FileName);
				}
				catch
				{
					MessageBox.Show("Не удаётся перезаписать файл!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Cursor = Cursors.Default;
					return;
				}
			}
			_context.Dispose();
			Program.context = new GamesEntities(saveFileDialog1.FileName);
			Program.context.Database.Create();
			Program.context.Database.ExecuteSqlCommand(string.Format("INSERT INTO GLDBVersion (ID_V, Version) VALUES (1,{0});", Settings.Default.DatabaseVersion));
			_context = Program.context;
			Init();
			Program.CurrentBase = saveFileDialog1.FileName;
			Text = string.Format("Список игр - {0}", Path.GetFileName(saveFileDialog1.FileName));
			Cursor = Cursors.Default;
		}

		private void openBaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				_context.Dispose();
				try
				{
					Program.context = new GamesEntities(openFileDialog1.FileName);
					Program.context.Database.Connection.Open();
					Text = string.Format("Список игр - {0}", Path.GetFileName(openFileDialog1.FileName));
					if (!DBUpdater.checkDBVersion(Program.context))
					{
						Program.context = new GamesEntities(Settings.Default.DefaultConStr);
						Text = string.Format("Список игр - {0}", Path.GetFileName(Settings.Default.DefaultConStr));
					}
				}
				catch
				{
					MessageBox.Show("Не удалось открыть файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Program.context = new GamesEntities(Settings.Default.DefaultConStr);
				}
				Program.CurrentBase = openFileDialog1.FileName;
				_context = Program.context;
				Init();
				//this.Text = "Список игр - " + Path.GetFileName(openFileDialog1.FileName);
				Cursor = Cursors.Default;
			}
		}

		private void clearBaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Это уничтожит все данные в текущей базе. Вы уверенны?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				if (MessageBox.Show("База очищена. Желаете ли добавить данные вспомогательных таблицы?", "Вспомогательные таблицы", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.OK)
				{
					MessageBox.Show("Данный функционал ещё не реализован. Пользуйтесь пустой базой.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					_context.Database.Delete();
					_context.Database.Create();
					_context.Database.ExecuteSqlCommand(string.Format("INSERT INTO GLDBVersion (ID_V, Version) VALUES (1,{0});", Settings.Default.DatabaseVersion));
					Init();
				}
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			label34.Visible = textBox1.Text != "";
			ChangeFilter();
		}

		private void manageSeriesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var manageSeries = new ManageOPOS(2);
			manageSeries.ShowDialog();
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

		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((comboBox3 != null) && (comboBox3.SelectedValue != null))
			{
				if ((decimal)comboBox3.SelectedValue == 0)
				{
					fd = true;
				}
				else
				{
					fd = false;
					ef = false;
				}
				ChangeFilter();
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((comboBox1 != null) && (comboBox1.SelectedValue != null))
			{
				if ((decimal)comboBox1.SelectedValue == 0)
				{
					frf = true;
				}
				else
				{
					frf = false;
					ef = false;
				}
				ChangeFilter();
			}
		}

		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((comboBox4 != null) && (comboBox4.SelectedValue != null))
			{
				if ((decimal)comboBox4.SelectedValue == 0)
				{
					fop = true;
				}
				else
				{
					fop = false;
					ef = false;
				}
				ChangeFilter();
			}
		}

		private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((comboBox5 != null) && (comboBox5.SelectedValue != null))
			{
				if ((decimal)comboBox5.SelectedValue == 0)
				{
					fg = true;
				}
				else
				{
					fg = false;
					ef = false;
				}
				ChangeFilter();
			}
		}

		private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((comboBox6 != null) && (comboBox6.SelectedValue != null))
			{
				if ((decimal)comboBox6.SelectedValue == 0)
				{
					fpl = true;
				}
				else
				{
					fpl = false;
					ef = false;
				}
				ChangeFilter();
			}
		}

		private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((decimal)comboBox7.SelectedIndex == 0)
			{
				fs = true;
			}
			else
			{
				fs = false;
				ef = false;
			}
			ChangeFilter();
		}

		private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((decimal)comboBox10.SelectedIndex == 0)
			{
				fl = true;
			}
			else
			{
				fl = false;
				ef = false;
			}
			ChangeFilter();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			if ((numericUpDown1.Value == 0) && (numericUpDown2.Value == Settings.Default.MaxYourRating))
			{
				fyr = true;
			}
			else
			{
				fyr = false;
				ef = false;
			}
			ChangeFilter();
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			if ((numericUpDown1.Value == 0) && (numericUpDown2.Value == Settings.Default.MaxYourRating))
			{
				fyr = true;
			}
			else
			{
				fyr = false;
				ef = false;
			}
			ChangeFilter();
		}

		private void numericUpDown3_ValueChanged(object sender, EventArgs e)
		{
			if ((numericUpDown3.Value == 0) && (numericUpDown4.Value == Settings.Default.MaxRecenzorRating))
			{
				fir = true;
			}
			else
			{
				fir = false;
				ef = false;
			}
			ChangeFilter();
		}

		private void numericUpDown4_ValueChanged(object sender, EventArgs e)
		{
			if ((numericUpDown3.Value == 0) && (numericUpDown4.Value == Settings.Default.MaxRecenzorRating))
			{
				fir = true;
			}
			else
			{
				fir = false;
				ef = false;
			}
			ChangeFilter();
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			if ((dateTimePicker1.Value < _context.Games.Select(g => g.Date_Release).Min()) && (dateTimePicker2.Value > _context.Games.Select(g => g.Date_Release).Max()))
			{
				fda = true;
			}
			else
			{
				fda = false;
				ef = false;
			}
			ChangeFilter();
		}

		private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
		{
			if ((dateTimePicker1.Value < _context.Games.Select(g => g.Date_Release).Min()) && (dateTimePicker2.Value > _context.Games.Select(g => g.Date_Release).Max()))
			{
				fda = true;
			}
			else
			{
				fda = false;
				ef = false;
			}
			ChangeFilter();
		}

		private void ovalShape1_Click(object sender, EventArgs e)
		{
			ef = !ef;
			ChangeFilter();
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

		private void configToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var set = new SettingsForm();
			if (set.ShowDialog() == DialogResult.OK)
			{
				label26.Text = string.Format("Оценка {0}:", Settings.Default.Recenzor);
				label1.Text = label14.Text = string.Format("{0}:", Settings.Default.DistrReg);
				if (numericUpDown1.Value > Settings.Default.MaxYourRating)
					numericUpDown1.Value = Settings.Default.MaxYourRating;
				if (numericUpDown2.Value > Settings.Default.MaxYourRating)
					numericUpDown2.Value = Settings.Default.MaxYourRating;
				if (numericUpDown3.Value > Settings.Default.MaxRecenzorRating)
					numericUpDown3.Value = Settings.Default.MaxRecenzorRating;
				if (numericUpDown4.Value > Settings.Default.MaxRecenzorRating)
					numericUpDown4.Value = Settings.Default.MaxRecenzorRating;
				numericUpDown1.Maximum = numericUpDown2.Maximum = Settings.Default.MaxYourRating;
				numericUpDown3.Maximum = numericUpDown4.Maximum = Settings.Default.MaxRecenzorRating;
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
			}
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
			var infa = new SeriesInf(((Games)gamesBindingSource.Current).Series) {Owner = this};
			infa.Show();
		}

		private void AddContent_Click(object sender, EventArgs e)
		{
			var addC = new AddContent((Games)gamesBindingSource.Current, 0);
			if (addC.ShowDialog() == DialogResult.OK)
				UpdateView();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (((Games)gamesBindingSource.Current).Content.Any())
			{
				var con = new ContentForm((Games)gamesBindingSource.Current) {Owner = this};
				con.Show();
			}
			else if (((Games)gamesBindingSource.Current).GamesInCollect.Count > 0)
			{
				var col = new SeriesInf((Games)gamesBindingSource.Current)
				{
					Text = string.Format("Игры сборника {0}", ((Games) gamesBindingSource.Current).Name)
				};
				col.Show();
			}
		}

		private void AddCollect_Click(object sender, EventArgs e)
		{
			var addCol = new AddCollect(null);
			addCol.ShowDialog();
			ChangeFilter();
			UpdateView();
		}

		private void gamesListBox_Format(object sender, ListControlConvertEventArgs e)
		{
			if (((Games) gamesBindingSource.Current).Localisation_Type != -1 || ((Games) e.ListItem).ID_Collect == null) return;
			e.Value = ((Games) e.ListItem).GetFullName();
		}

		private void сделатьДополнениемToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (((Games)gamesBindingSource.Current).ID_Content == null)
			{
				var CG = new ChooseGame((Games)gamesBindingSource.Current);
				if (CG.ShowDialog() != DialogResult.OK) return;
				ChangeFilter();
				UpdateView();
			}
			else
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
				ChangeFilter();
			}
		}

		private void DispContent_CheckedChanged(object sender, EventArgs e)
		{
			fc = DispContent.Checked;
			ef = false;
			ChangeFilter();
		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			e.Value = ((Games)dataGridView1.Rows[e.RowIndex].DataBoundItem).GetFullName();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Всего игр (с дополнениями и свистелками): " + _context.Games.Count(g => g.Localisation_Type > -1) + "\n" +
				"Всего игр (с дополнениями): " + _context.Games.Count(g => g.Localisation_Type > -1 && g.Status_complite != 6) + "\n" +
				"Лицензионных игр (с учётом дополнений и свистелок): " + _context.Games.Count(g => g.Localisation_Type > -1 && (bool)g.Game_Type) + "\n" +
				"Лицензионных игр (с учётом дополнений): " + _context.Games.Count(g => g.Localisation_Type > -1 && (bool)g.Game_Type && g.Status_complite != 6) + "\n" +
				"Лицензионных игр (без учёта дополнений): " + _context.Games.Count(g => g.Localisation_Type > -1 && (bool)g.Game_Type && g.ID_Content == null) + "\n" +
				"Пиратских игр (с учётом дополнений и свистелок): " + _context.Games.Count(g => g.Localisation_Type > -1 && !(bool)g.Game_Type) + "\n" +
				"Пиратских игр (с учётом дополнений): " + _context.Games.Count(g => g.Localisation_Type > -1 && !(bool)g.Game_Type && g.Status_complite != 6) + "\n" +
				"Пиратских игр (без учёта дополнений): " + _context.Games.Count(g => g.Localisation_Type > -1 && g.ID_Content == null && !(bool)g.Game_Type) + "\n" +
				"Лицензионных сборников: " + _context.Games.Count(g => g.Localisation_Type == -1 && (bool)g.Game_Type) + "\n" +
				"Пиратских сборников: " + _context.Games.Count(g => g.Localisation_Type == -1 && !(bool)g.Game_Type), "Количество игр");
		}

		private void Wheel(object sender, MouseEventArgs e)
		{
			if ((e.X >= dataGridView1.Left) && (e.X <= dataGridView1.Right) && (e.Y <= dataGridView1.Bottom) && (e.Y >= dataGridView1.Top))
				dataGridView1.Focus();
		}

		private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			editGameToolStripMenuItem.PerformClick();
		}

		private void label34_Click(object sender, EventArgs e)
		{
			textBox1.Clear();
			label34.Visible = false;
		}

		private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((decimal)comboBox8.SelectedIndex == 0)
			{
				flp = true;
			}
			else
			{
				flp = false;
				ef = false;
			}
			ChangeFilter();
		}

		private void giveGameStripButton_Click(object sender, EventArgs e)
		{
			var whereGame = new WhereGame(((Games)gamesBindingSource.Current));
			whereGame.ShowDialog();
			UpdateView();
		}

		private void Where_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var whereGame = new WhereGame(((Games)gamesBindingSource.Current));
			whereGame.ShowDialog();
			UpdateView();
			Where.LinkVisited = false;
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			var load = new Load();
			load.ShowDialog();
		}

		private void GamesForm_Shown(object sender, EventArgs e)
		{
			UpdateView();
		}
	}
}
