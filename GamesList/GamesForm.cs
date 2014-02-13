using System;
using System.Windows.Forms;
using System.Linq;
using System.Data.Entity;
using System.Drawing;
using GamesList.Model;
using System.IO;
using System.Collections.Generic;


namespace GamesList
{
    public partial class GamesForm : Form
    {
        private Boolean fp, fd, frf, fop, fg, fpl, fs, fyr, fir, fda, fl, fc, ef, flp;
		private Boolean IsInit = false;
        public GamesForm()//Точка входа в программу
        {
            InitializeComponent();
            /*var Query = from Games in Program.context.Games
                        select Games;
            gamesBindingSource.DataSource = Query.ToList();*/
            this.Text = "Список игр - " + Path.GetFileName(Program.CurrentBase);
            label9.Text = label26.Text = "Рейтинг " + Properties.Settings.Default.Recenzor + ":";
            label1.Text = label14.Text = Properties.Settings.Default.DistrReg + ":";
            numericUpDown1.Maximum = numericUpDown2.Maximum = Properties.Settings.Default.MaxYourRating;
            numericUpDown3.Maximum = numericUpDown4.Maximum = Properties.Settings.Default.MaxRecenzorRating;
            this.MouseWheel += new MouseEventHandler(wheel);
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
            //gamesBindingSource.DataSource = Program.context.Games.Local.ToBindingList().Where(g => g.ID_Content==null).OrderBy(g => g.Name);
            //Program.context.Games.Load();
            /*var Query = from Games in Program.context.Games
                        where Games.ID_Content == null
                        orderby Games.Name
                        select Games;
            gamesBindingSource.DataSource = Query.ToList();*/
            
            //gamesBindingSource.Filter = "Name = 'DooM'";
            //Загружаем издателей из базы
            List<Publishers> LP = new List<Publishers>();
            LP.Add(new Publishers { Name = "<не важно>", Id_Publisher = 0 });
            LP.AddRange(Program.context.Publishers.OrderBy(p=> p.Name).ToArray());
            publishersBindingSource.DataSource = LP;
            // publishersBindingSource.DataSource = Program.context.Publishers.Local.ToBindingList();
            //Program.context.Publishers.Load();
            //Загружаем разработчиков из базы
            List<Developers> LD = new List<Developers>();
            LD.Add(new Developers { Name = "<не важно>", ID_Developer = 0 });
            LD.AddRange(Program.context.Developers.OrderBy(p=>p.Name).ToArray());
            developersBindingSource.DataSource = LD;
            //developersBindingSource.DataSource = Program.context.Developers.Local.ToBindingList();
            //rogram.context.Developers.Load();
            //Загружаем издателей в России из базы
            List<RF_Distributors> LRF = new List<RF_Distributors>();
            LRF.Add(new RF_Distributors { Name = "<не важно>", ID_RF_Distributor = 0 });
			LRF.AddRange(Program.context.RF_Distributors.OrderBy(p => p.Name).ToArray());
            rFDistributorsBindingSource.DataSource = LRF;
            //rFDistributorsBindingSource.DataSource = Program.context.RF_Distributors.Local.ToBindingList();
            //Program.context.RF_Distributors.Load();
            //Загружаем виды онлайн-защит из базы
            List<Online_protections> LOP = new List<Online_protections>();
            LOP.Add(new Online_protections { Name = "<не важно>", ID_Protect = 0 });
            LOP.AddRange(Program.context.Online_protections.OrderBy(op => op.Name).ToArray());
            onlineprotectionsBindingSource.DataSource = LOP;
            //onlineprotectionsBindingSource.DataSource = Program.context.Online_protections.Local.ToBindingList();
            //Program.context.Online_protections.Load();
            //Загружаем жанры из базы
            List<Genres> LG = new List<Genres>();
            LG.Add(new Genres { Name = "<не важно>", ID_Genre = 0 });
            LG.AddRange(Program.context.Genres.ToArray());
            genresBindingSource.DataSource = LG;
            //genresBindingSource.DataSource = Program.context.Genres.Local.ToBindingList();
            //Program.context.Genres.Load();
            //Загружаем платформы из базы
            List<Platforms> LPL = new List<Platforms>();
            LPL.Add(new Platforms { Name = "<не важно>", ID_Platform = 0 });
            LPL.AddRange(Program.context.Platforms.ToArray());
            platformsBindingSource.DataSource = LPL;
            //platformsBindingSource.DataSource = Program.context.Platforms.Local.ToBindingList();
            //Program.context.Platforms.Load();
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
                var Q = from Games in Program.context.Games
                        where Games.ID_Collect == ((Games)gamesBindingSource.Current).ID_Game
                        orderby Games.Name
                        select Games;
                //gamesBindingSource1.DataSource = Program.context.Games.Local.ToBindingList().Where(g => g.ID_Collect == ((Games)gamesBindingSource.Current).ID_Game).OrderBy(g => g.Name);
                gamesBindingSource1.DataSource = Q.ToList();
                //Program.context.Games.Load();
                viewContentToolStripMenuItem.Text = button1.Text = viewContent.Text = "Просмотр игр";
				editGameToolStripMenuItem.Text = editGameContextToolStripMenuItem.Text = "Редактировать сборник";
				delGameToolStripMenuItem.Text = delGamecontextToolStripMenuItem1.Text = "Удалить сборник";
				addContentToolStripMenuItem.Enabled=  addContentContextToolStripMenuItem.Enabled = сделатьДополнениемToolStripMenuItem.Enabled = false;
            }
            else
            {
				if (((Games)gamesBindingSource.Current).ID_Content != null)
				{
					editGameToolStripMenuItem.Text = editGameContextToolStripMenuItem.Text = "Редактировать дополнение";
					сделатьДополнениемToolStripMenuItem.Text = "Сделать обычной игрой";
					Boolean f = ((Games)gamesBindingSource.Current).TypeContent ?? false;
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
                foreach (var g in ((Games)gamesBindingSource.Current).Genres)//Перебираем жанры в текущей игре
                {
                    JenrName.Text += g.Name + ", ";//Добавляем в строку жанр и запятую
                }
                if (JenrName.Text.Length > 0)//Если строка не пуста
                {
                    JenrName.Text = JenrName.Text.Substring(0, JenrName.Text.Length - 2);//Удаляем 2 последних символа ", " из строки 
                }
                else
                {
                    JenrName.Text = "<отсутствует>";//Если строка пуста, то пишем, что жанров нет
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
                Dev.Visible = true;
                DevName.Visible = true;
                if (((Games)gamesBindingSource.Current).Developers != null)//Если разработчик задан, то 
                {
                    DevName.Text = ((Games)gamesBindingSource.Current).Developers.Name;//Отображаем его имя
                }
                else
                {
                    DevName.Text = "<отсутствует>";//Либо заглушку
                }
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
            if ((((Games)gamesBindingSource.Current).Games1.Count > 0) || (((Games)gamesBindingSource.Current).Games11.Count > 0))
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



            if (((Games)gamesBindingSource.Current).Publishers != null)//То же самое, что и разработчик
            {
                DistrName.Text = ((Games)gamesBindingSource.Current).Publishers.Name;
            }
            else
            {
                DistrName.Text = "<отсутствует>";
            }
            if (((Games)gamesBindingSource.Current).RF_Distributors != null)//То же самое, что и разработчик
            {
                RFDistrName.Text = ((Games)gamesBindingSource.Current).RF_Distributors.Name;
            }
            else
            {
                RFDistrName.Text = "<отсутствует>";
            }
            //Тоже самое, что и с жанрами, но с онлайн-защитами
            Online_Protect.Text = "";
            foreach (var op in ((Games)gamesBindingSource.Current).Online_protections)
            {
                Online_Protect.Text += op.Name + ", ";
            }
            if (Online_Protect.Text.Length > 0)
            {
                Online_Protect.Text = Online_Protect.Text.Substring(0, Online_Protect.Text.Length - 2);
            }
            else
            {
                Online_Protect.Text = "<отсутствуют>";
            }

            //Тоже самое, что и с жанрами, но с платформами
            PlatformsLabel.Text = "";
            foreach (var p in ((Games)gamesBindingSource.Current).Platforms)
            {
                PlatformsLabel.Text += p.Name + ", ";
            }
            if (PlatformsLabel.Text.Length > 0)
            {
                PlatformsLabel.Text = PlatformsLabel.Text.Substring(0, PlatformsLabel.Text.Length - 2);
            }
            else
            {
                PlatformsLabel.Text = "<отсутствуют>";
            }
            //Проверяем наличие даты релиза и выводим её в нужном формате
            if (((Games)gamesBindingSource.Current).Date_Release != null)
            {
                Release_Date.Text = ((DateTime)((Games)gamesBindingSource.Current).Date_Release).ToString("dd.MM.yyyy");
            }
            else
            {
                Release_Date.Text = "<отсутствует>";
            }

            //Тоже самое, что и с жанрами, но с дисками
            Disks.Text = "";
            foreach (var p in ((Games)gamesBindingSource.Current).Game_disks)
            {
                Disks.Text += p.Kol_vo + " " + p.Disk_types.Name + " + ";
            }
            if (Disks.Text.Length > 0)
            {
                Disks.Text = Disks.Text.Substring(0, Disks.Text.Length - 3);
            }
            else
            {
                Disks.Text = "<отсутствуют>";
            }
            if (((Games)gamesBindingSource.Current).Boxes != null)
            {
                BoxLabel.Text = ((Games)gamesBindingSource.Current).Boxes.Name;//Выставяляем значение типа коробки
            }
            else
            {
                BoxLabel.Text = "<отсутствует>";
            }
            if (((Games)gamesBindingSource.Current).Editions != null)
            {
                EditionLabel.Text = ((Games)gamesBindingSource.Current).Editions.Name;//Выставляем значение типа издания
            }
            else
            {
                EditionLabel.Text = "<отсутствует>";
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
            if (((Games)gamesBindingSource.Current).ID_Content != null)
            {

            }
            toolStripButton1.Text = "Кол-во игр: " + Program.context.Games.Where(g => g.Localisation_Type > -1 && g.Status_complite != 6).Count().ToString();
            if (Properties.Settings.Default.VisMax)
            {
                if (((Games)gamesBindingSource.Current).Rate_Igromania.ToString() != "")
                {
                    IgromaniaRate.Text = ((Games)gamesBindingSource.Current).Rate_Igromania.ToString() + "/" + Properties.Settings.Default.MaxRecenzorRating.ToString();
                }
                else
                {
                    IgromaniaRate.Text = "";
                }
                if (((Games)gamesBindingSource.Current).Rate_person.ToString() != "")
                {
                    PersonalRate.Text = ((Games)gamesBindingSource.Current).Rate_person.ToString() + "/" + Properties.Settings.Default.MaxYourRating.ToString();
                }
                else
                {
                    PersonalRate.Text = "";
                }
                IgromaniaRate.Font = new Font("Microsoft Sans Serif", 38, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                PersonalRate.Font = new Font("Microsoft Sans Serif", 38, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
            else
            {
                IgromaniaRate.Text = ((Games)gamesBindingSource.Current).Rate_Igromania.ToString();
                PersonalRate.Text = ((Games)gamesBindingSource.Current).Rate_person.ToString();
                IgromaniaRate.Font = new Font("Microsoft Sans Serif", 48, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                PersonalRate.Font = new Font("Microsoft Sans Serif", 48, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
            Stand.Visible = ((Games)gamesBindingSource.Current).TypeContent ?? false;
			GameName.Text = ((Games)gamesBindingSource.Current).getName();
			if ((bool)((Games)gamesBindingSource.Current).Game_Type)
			{
				Pirat.Visible = false;
			}
			else
			{
				Pirat.Visible = true;
			}
			if (((Games)gamesBindingSource.Current).WhereStatus != null && ((Games)gamesBindingSource.Current).WhereStatus != "")
			{
				Where.Visible = true;
			}
			else
			{
				Where.Visible = false;
			}

			//Нормализуем вывод информации
			int i = 15;
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
				if (different<=0)
					break;
				different = different / 15;
				i = i - Decimal.ToInt32(Math.Ceiling(different));
			}
        }

        public void ChangeFilter()
        {
            if ((ef == true) || ((fp == true) && (fd == true) && (frf == true) && (fop == true) && (fg == true) && (fpl == true) && (fs == true) && (fyr == true) && (fir == true) && (fda == true) && (fl == true) && (fc == false) && (flp == true)))
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
                string s = textBox1.Text.ToUpper();
				bool filterLicence = comboBox8.SelectedIndex == 1 ? true : false;
                try
                {
                    var GQuery = (from Games in Program.context.Games
								 where ((!ef && fc) || Games.ID_Content == null) &&
									   (ef || (
                                        (fp || Games.ID_Publisher == (decimal?)comboBox2.SelectedValue) &&
                                        (fd || Games.ID_Developer == (decimal?)comboBox3.SelectedValue) &&
                                        (frf || Games.ID_RF_Distributor == (decimal?)comboBox1.SelectedValue) &&
                                        (fop || Games.Online_protections.Select(o => o.ID_Protect).Contains((decimal)comboBox4.SelectedValue)) &&
                                        (fg || Games.Genres.Select(g => g.ID_Genre).Contains((decimal)comboBox5.SelectedValue)) &&
                                        (fpl || Games.Platforms.Select(p => p.ID_Platform).Contains((decimal)comboBox6.SelectedValue)) &&
                                        (fs || Games.Status_complite == (comboBox7.SelectedIndex - 1)) &&
                                        (fl || Games.Localisation_Type == (comboBox10.SelectedIndex - 1)) &&
                                        (fyr || (Games.Rate_person >= n1 && Games.Rate_person <= n2)) &&
                                        (fir || (Games.Rate_Igromania >= n3 && Games.Rate_Igromania <= n4)) &&
                                        (fda || (Games.Date_Release >= dateTimePicker1.Value && Games.Date_Release <= dateTimePicker2.Value)) &&
										(flp || Games.Game_Type == filterLicence))
                                       )
                                 orderby Games.ID_Content == null ? 
                                 Games.Name : Games.Games2.Games2 == null ?
                                 (Games.Games2.Name + (Games.Games2.Name.IndexOf(":") > -1 ? " - " : ": ") + Games.Name) :
                                  (Games.Games2.Games2.Name + (Games.Games2.Games2.Name.IndexOf(":") > -1 ? " - " : ": ") + Games.Games2.Name + " - " +Games.Name)
								  select Games).ToList()
								  .Where(g => (s == "") || (g.getName().ToUpper().Contains(s)) || ((g.Original_Name != null) && (g.Original_Name.ToUpper().Contains(s))))
								  .ToList();
                    if (GQuery.Count() > 0)
                    {
						gamesBindingSource.DataSource = GQuery;
                    }
                    else
                    {
                        gamesBindingSource.DataSource = new Games[] { };
                    }
                    //gamesBindingSource.DataSource = q.Count > 0 ? new Games[] { } : q ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void managePublishersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManagePublisher = new ManagePDL(0);
            ManagePublisher.ShowDialog();
        }

        private void GamesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void manageDevelopersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManagePublisher = new ManagePDL(1);
            ManagePublisher.ShowDialog();
        }

        private void manageRFDistrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManagePublisher = new ManagePDL(2);
            ManagePublisher.ShowDialog();
        }

        private void manageGenresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManageGenres = new ManageGEB(0);
            ManageGenres.ShowDialog();
        }

        private void manageOnlineProtectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManageOnline_Protections = new ManageOPOS(0);
            ManageOnline_Protections.ShowDialog();
        }

        private void managePatformsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManagePlatforms = new ManageOPOS(1);
            ManagePlatforms.ShowDialog();
        }

        private void manageDiskTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManageDiskTypes = new ManageDiskTypes();
            ManageDiskTypes.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {
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
            if (Program.context.Boxes.Any() && Program.context.Editions.Any())
            {
                Form ДобавитьИгру = new AddGame(null);
                ДобавитьИгру.ShowDialog();
                ChangeFilter();
                UpdateView();
            }
            else
            {
                MessageBox.Show("Добавьте сначала хотя по одному виду комплетации и издания в меню Управления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((Games)gamesBindingSource.Current != null) && (MessageBox.Show("Вы действительно хотите удалить игру " + ((Games)gamesBindingSource.Current).Name + " из базы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.Yes))
            {

                Program.context.Games.Remove((Games)gamesBindingSource.Current);
                Program.context.SaveChanges();
                /*var Query = from Games in Program.context.Games
                            where Games.ID_Content == null
                            orderby Games.Name
                            select Games;
                gamesBindingSource.DataSource = Query.ToList();*/
                ChangeFilter();
                UpdateView();
            }
        }

        private void manageEditionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManageEditions = new ManageGEB(1);
            ManageEditions.ShowDialog();
        }

        private void manageBoxesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManageBoxes = new ManageGEB(2);
            ManageBoxes.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form About = new AboutBox();
            About.Show();
        }

        private void editGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form EditGame;
            if (((Games)gamesBindingSource.Current).Localisation_Type > -1)
            {
				if (((Games)gamesBindingSource.Current).ID_Content != null)
				{
					EditGame = new AddContent(Program.context.Games.Find(((Games)gamesBindingSource.Current).ID_Game),0);
					EditGame.Text = "Редактирование дополнения " + ((Games)gamesBindingSource.Current).Name;
				}
				else
				{
					EditGame = new AddGame(Program.context.Games.Find(((Games)gamesBindingSource.Current).ID_Game));
					EditGame.Text = "Редактирование игры " + ((Games)gamesBindingSource.Current).Name;
				}
            }
            else
            {
                EditGame = new AddCollect(Program.context.Games.Find(((Games)gamesBindingSource.Current).ID_Game));
                EditGame.Text = "Редактирование сборника " + ((Games)gamesBindingSource.Current).Name;
            }
            EditGame.ShowDialog();
            /*var Query = from Games in Program.context.Games
                        where Games.ID_Content == null
                        orderby Games.Name
                        select Games;
            gamesBindingSource.DataSource = Query.ToList();*/
            ChangeFilter();
            UpdateView();
        }

        private void createBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                if (File.Exists(saveFileDialog1.FileName))
                {
                    try
                    {
                        File.Delete(saveFileDialog1.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Не удаётся перезаписать файл!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                Program.context.Dispose();
                Program.context = new GamesEntities(Program.buidConStr(saveFileDialog1.FileName));
                Program.context.Database.Create();
                Program.context.Database.ExecuteSqlCommand("INSERT INTO GLDBVersion (ID_V, Version) VALUES (1,5);");
                Init();
                Program.CurrentBase = saveFileDialog1.FileName;
                this.Text = "Список игр - " + Path.GetFileName(saveFileDialog1.FileName);
                this.Cursor = Cursors.Default;
            }
        }

        private void openBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                Program.context.Dispose();
                try
                {
                    Program.context = new GamesEntities(Program.buidConStr(openFileDialog1.FileName));
                    Program.context.Database.Connection.Open();
                    this.Text = "Список игр - " + Path.GetFileName(openFileDialog1.FileName);
					if (!DBUpdater.checkDBVersion(Program.context))
                    {
                        Program.context = new GamesEntities(Program.buidConStr(Properties.Settings.Default.DefaultConStr));
                        this.Text = "Список игр - " + Path.GetFileName(Properties.Settings.Default.DefaultConStr);
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось открыть файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.context = new GamesEntities(Program.buidConStr(Properties.Settings.Default.DefaultConStr));
                }
                Program.CurrentBase = openFileDialog1.FileName;
                Init();
                //this.Text = "Список игр - " + Path.GetFileName(openFileDialog1.FileName);
                this.Cursor = Cursors.Default;
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
                    Program.context.Database.Delete();
                    Program.context.Database.Create();
                    Program.context.Database.ExecuteSqlCommand("INSERT INTO GLDBVersion (ID_V, Version) VALUES (1,5);");
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
            Form ManageSeries = new ManageOPOS(2);
            ManageSeries.ShowDialog();
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
            if ((numericUpDown1.Value == 0) && (numericUpDown2.Value == Properties.Settings.Default.MaxYourRating))
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
            if ((numericUpDown1.Value == 0) && (numericUpDown2.Value == Properties.Settings.Default.MaxYourRating))
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
            if ((numericUpDown3.Value == 0) && (numericUpDown4.Value == Properties.Settings.Default.MaxRecenzorRating))
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
            if ((numericUpDown3.Value == 0) && (numericUpDown4.Value == Properties.Settings.Default.MaxRecenzorRating))
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
            if ((dateTimePicker1.Value < Program.context.Games.Select(g => g.Date_Release).Min()) && (dateTimePicker2.Value > Program.context.Games.Select(g => g.Date_Release).Max()))
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
            if ((dateTimePicker1.Value < Program.context.Games.Select(g => g.Date_Release).Min()) && (dateTimePicker2.Value > Program.context.Games.Select(g => g.Date_Release).Max()))
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
			if (((Games)gamesBindingSource.Current).Developers != null)
			{
				Form Infa = new InformationWindows(0, ((Games)gamesBindingSource.Current).Developers);
				Infa.Show();
			}
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Form MP = new MaxPoster(pictureBox1.Image);
                MP.Text = ((Games)gamesBindingSource.Current).Name;
                MP.Show();
            }
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Set = new SettingsForm();
            if (Set.ShowDialog() == DialogResult.OK)
            {
                label26.Text = "Оценка " + Properties.Settings.Default.Recenzor + ":";
                label1.Text = label14.Text = Properties.Settings.Default.DistrReg + ":";
                if (numericUpDown1.Value > Properties.Settings.Default.MaxYourRating)
                {
                    numericUpDown1.Value = Properties.Settings.Default.MaxYourRating;
                }
                if (numericUpDown2.Value > Properties.Settings.Default.MaxYourRating)
                {
                    numericUpDown2.Value = Properties.Settings.Default.MaxYourRating;
                }
                if (numericUpDown3.Value > Properties.Settings.Default.MaxRecenzorRating)
                {
                    numericUpDown3.Value = Properties.Settings.Default.MaxRecenzorRating;
                }
                if (numericUpDown4.Value > Properties.Settings.Default.MaxRecenzorRating)
                {
                    numericUpDown4.Value = Properties.Settings.Default.MaxRecenzorRating;
                }
                numericUpDown1.Maximum = numericUpDown2.Maximum = Properties.Settings.Default.MaxYourRating;
                numericUpDown3.Maximum = numericUpDown4.Maximum = Properties.Settings.Default.MaxRecenzorRating;
                if (Properties.Settings.Default.VisMax)
                {
                    if (((Games)gamesBindingSource.Current).Rate_Igromania.ToString() != "")
                    {
                        IgromaniaRate.Text = ((Games)gamesBindingSource.Current).Rate_Igromania.ToString() + "/" + Properties.Settings.Default.MaxRecenzorRating.ToString();
                    }
                    else
                    {
                        IgromaniaRate.Text = "";
                    }
                    if (((Games)gamesBindingSource.Current).Rate_person.ToString() != "")
                    {
                        PersonalRate.Text = ((Games)gamesBindingSource.Current).Rate_person.ToString() + "/" + Properties.Settings.Default.MaxYourRating.ToString();
                    }
                    else
                    {
                        PersonalRate.Text = "";
                    }
                    IgromaniaRate.Font = new Font("Microsoft Sans Serif", 40, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    PersonalRate.Font = new Font("Microsoft Sans Serif", 40, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                }
                else
                {
                    IgromaniaRate.Text = ((Games)gamesBindingSource.Current).Rate_Igromania.ToString();
                    PersonalRate.Text = ((Games)gamesBindingSource.Current).Rate_person.ToString();
                    IgromaniaRate.Font = new Font("Microsoft Sans Serif", 48, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    PersonalRate.Font = new Font("Microsoft Sans Serif", 48, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
			if (((Games)gamesBindingSource.Current).Publishers != null)
			{
				Form Infa = new InformationWindows(1, ((Games)gamesBindingSource.Current).Publishers);
				Infa.Show();
			}
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
			if (((Games)gamesBindingSource.Current).RF_Distributors != null)
			{
				Form Infa = new InformationWindows(2, ((Games)gamesBindingSource.Current).RF_Distributors);
				Infa.Show();
			}
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form Infa = new SeriesInf(((Games)gamesBindingSource.Current).Series);
            Infa.Owner = this;
            Infa.Show();
        }

        private void AddContent_Click(object sender, EventArgs e)
        {
            Form AddC = new AddContent((Games)gamesBindingSource.Current,0);
            if (AddC.ShowDialog() == DialogResult.OK)
                UpdateView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((Games)gamesBindingSource.Current).Games1.Count > 0)
            {
                Form Con = new ContentForm((Games)gamesBindingSource.Current);
                Con.Owner = this;
                Con.Show();
            }
            else if (((Games)gamesBindingSource.Current).Games11.Count > 0)
            {
                Form Col = new SeriesInf((Games)gamesBindingSource.Current);
                Col.Text = "Игры сборника " + ((Games)gamesBindingSource.Current).Name;
                Col.Show();
            }
        }

        private void AddCollect_Click(object sender, EventArgs e)
        {
            Form AddCol = new AddCollect(null);
            AddCol.ShowDialog();
            /*var Query = from Games in Program.context.Games
                        where Games.ID_Content == null
                        orderby Games.Name
                        select Games;
            gamesBindingSource.DataSource = Query.ToList();*/
            ChangeFilter();
            UpdateView();
        }

        private void gamesListBox_Format(object sender, ListControlConvertEventArgs e)
        {
            if (((Games)gamesBindingSource.Current).Localisation_Type == -1 && ((Games)e.ListItem).ID_Content != null)
            {
                if (((Games)e.ListItem).Games2.Name.IndexOf(":") > -1)
                {
                    e.Value = ((Games)e.ListItem).Games2.Name + " - " + ((Games)e.ListItem).Name;
                }
                else
                    e.Value = ((Games)e.ListItem).Games2.Name + ": " + ((Games)e.ListItem).Name;
            }
        }

        private void сделатьДополнениемToolStripMenuItem_Click(object sender, EventArgs e)
        {
			if (((Games)gamesBindingSource.Current).ID_Content == null)
			{
				Form CG = new ChooseGame((Games)gamesBindingSource.Current);
				if (CG.ShowDialog() == DialogResult.OK)
				{
					ChangeFilter();
					UpdateView();
				}
			}
			else
			{
				if (MessageBox.Show("Вы правда хотите " + ((Games)gamesBindingSource.Current).Name + " сделать обычной игрой?", "Уточнение", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
				{
					Games g = ((Games)gamesBindingSource.Current);
					if (g.Games2.Name.IndexOf(":") > -1)
					{
						g.Name = g.Games2.Name + " - " + g.Name;
					}
					else
						g.Name = g.Games2.Name + ": " + g.Name;
					g.TypeContent = null;
					g.Games2 = null;
					g.ID_Content = null;
					Program.context.SaveChanges();
					ChangeFilter();
				}
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
			e.Value = ((Games)dataGridView1.Rows[e.RowIndex].DataBoundItem).getName();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Всего игр (с дополнениями и свистелками): " + Program.context.Games.Where(g => g.Localisation_Type > -1).Count().ToString() + "\n" +
                "Всего игр (с дополнениями): " + Program.context.Games.Where(g => g.Localisation_Type > -1 && g.Status_complite != 6).Count().ToString() + "\n" +
                "Лицензионных игр (с учётом дополнений и свистелок): " + Program.context.Games.Where(g => g.Localisation_Type > -1 && (bool)g.Game_Type).Count().ToString() + "\n" +
                "Лицензионных игр (с учётом дополнений): " + Program.context.Games.Where(g => g.Localisation_Type > -1 && (bool)g.Game_Type && g.Status_complite != 6).Count().ToString() + "\n" +
                "Лицензионных игр (без учёта дополнений): " + Program.context.Games.Where(g => g.Localisation_Type > -1 && (bool)g.Game_Type && g.ID_Content == null).Count().ToString() + "\n" +
                "Пиратских игр (с учётом дополнений и свистелок): " + Program.context.Games.Where(g => g.Localisation_Type > -1 && !(bool)g.Game_Type).Count().ToString() + "\n" +
                "Пиратских игр (с учётом дополнений): " + Program.context.Games.Where(g => g.Localisation_Type > -1 && !(bool)g.Game_Type && g.Status_complite != 6).Count().ToString() + "\n" +
                "Пиратских игр (без учёта дополнений): " + Program.context.Games.Where(g => g.Localisation_Type > -1 && g.ID_Content == null && !(bool)g.Game_Type).Count().ToString() + "\n" +
                "Лицензионных сборников: " + Program.context.Games.Where(g => g.Localisation_Type == -1 && (bool)g.Game_Type).Count().ToString() + "\n" +
                "Пиратских сборников: " + Program.context.Games.Where(g => g.Localisation_Type == -1 && !(bool)g.Game_Type).Count().ToString(), "Количество игр");
        }

        private void wheel(object sender, MouseEventArgs e)
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
			Form Where = new Forms.Additional_Forms.WhereGame(((Games)gamesBindingSource.Current));
			Where.ShowDialog();
			UpdateView();
		}

		private void Where_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Form Where = new Forms.Additional_Forms.WhereGame(((Games)gamesBindingSource.Current));
			Where.ShowDialog();
			UpdateView();
			this.Where.LinkVisited = false;
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Form Load = new Forms.Additional_Forms.Load();
			Load.ShowDialog();
		}

		private void GamesForm_Shown(object sender, EventArgs e)
		{
			UpdateView();
		}
    }
}
