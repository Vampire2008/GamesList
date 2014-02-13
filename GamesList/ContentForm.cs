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
    public partial class ContentForm : Form
    {
        public Boolean fc,IsInit;
        private Games game;
        public ContentForm(Games g)//Точка входа в программу
        {
            InitializeComponent();
            game = g;
            this.Text = "Дополнения для "+ game.Name;
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
            //Загружаем игры из базы
            /*var Query = from Games in Program.context.Games
                        where Games.ID_Content == game.ID_Game
                        orderby Games.Name
                        select Games;
            gamesBindingSource.DataSource = Query.ToList();*/
            fc = false;
            changefilter();
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
            JenrName.Text = "";//Очищаем строку
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

            if (((Games)gamesBindingSource.Current).Developers != null)//Если разработчик задан, то 
            {
                DevName.Text = ((Games)gamesBindingSource.Current).Developers.Name;//Отображаем его имя
            }
            else
            {
                DevName.Text = "<отсутствует>";//Либо заглушку
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
			GameName.Text = ((Games)gamesBindingSource.Current).getName();
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
            this.AddContent.Enabled = добавитьДополнениеToolStripMenuItem.Enabled = Stand.Visible=(bool)((Games)gamesBindingSource.Current).TypeContent;
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
            button1.Visible = ((Games)gamesBindingSource.Current).Games1.Count >0;

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
				if (different <= 0)
					break;
				different = different / 15;
				i = i - Decimal.ToInt32(Math.Ceiling(different));
			}
        }

        protected void changefilter()
        {
            var Query = from Games in Program.context.Games
                        where ((Games.ID_Content == game.ID_Game) || (Games.Games2.ID_Content != null && (Games.Games2.ID_Content == game.ID_Game))) &&
                        (fc || Games.Games2.Games2 == null)
                        orderby (Games.Games2.Games2 != null ? Games.Games2.Name + " - " + Games.Name : Games.Name)
                        select Games;
            gamesBindingSource.DataSource = Query.ToList();
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
            Form AddC = new AddContent(game,0);
            if (AddC.ShowDialog() == DialogResult.OK)
            {
                changefilter();
                UpdateView();
            }
        }

        private void delGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить дополнение " + ((Games)gamesBindingSource.Current).Name + " из базы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.Yes)
            {
                Program.context.Games.Remove((Games)gamesBindingSource.Current);
                Program.context.SaveChanges();
                changefilter();
                UpdateView();
            }
        }

        private void editGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form EditGame = new AddContent(Program.context.Games.Find(((Games)gamesBindingSource.Current).ID_Game),0);
            EditGame.ShowDialog();
            UpdateView();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                var Que = Program.context.Games.Local.ToBindingList().Where(g => (g.Name.ToLower().IndexOf(textBox1.Text.ToLower()) == 0) && (g.ID_Content == game.ID_Game));
                //MessageBox.Show(Program.context.Games.Local.Where(g => g.Name.ToLower().IndexOf(textBox1.Text.ToLower()) == 0).Count() == 0 ? "true" : "false");
                try
                {
                    gamesBindingSource.DataMember = "";
                    gamesBindingSource.DataSource = Que.Count() == 0 ? new Games[] { } : Que;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                var Query = from Games in Program.context.Games
                            where Games.ID_Content == game.ID_Game
                            orderby Games.Name
                            select Games;
                gamesBindingSource.DataSource = Query.ToList();
            }
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
            //Infa.Owner = this;
            Infa.Show();
        }

        private void сделатьОбычнойИгройToolStripMenuItem_Click(object sender, EventArgs e)
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
				changefilter();
				((GamesForm)this.Owner).ChangeFilter();
            }
        }

        private void wheel(object sender, MouseEventArgs e)
        {
            if ((e.X >= dataGridView1.Left) && (e.X <= dataGridView1.Right) && (e.Y <= dataGridView1.Bottom) && (e.Y >= dataGridView1.Top))
                dataGridView1.Focus();
        }

        private void AddContent_Click(object sender, EventArgs e)
        {
			Form AddC = new AddContent(((Games)gamesBindingSource.Current), 1);
            if (AddC.ShowDialog() == DialogResult.OK)
            {
                changefilter();
                UpdateView();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (((Games)dataGridView1.Rows[e.RowIndex].DataBoundItem).Games2.Games2 != null)
            {
                    e.Value = ((Games)dataGridView1.Rows[e.RowIndex].DataBoundItem).Games2.Name + " - " + e.Value;
            }
        }

        private void toolStripButton1_CheckedChanged(object sender, EventArgs e)
        {
            fc = toolStripButton1.Checked;
            changefilter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((Games)gamesBindingSource.Current).Games1.Count > 0)
            {
                Form Con = new ContentForm((Games)gamesBindingSource.Current);
                Con.Owner = this;
                ((ContentForm)Con).fc = true;
                ((ContentForm)Con).toolStripButton1.Enabled = false;
                ((ContentForm)Con).changefilter();
                Con.Show();
            }
        }

		private void ContentForm_Shown(object sender, EventArgs e)
		{
			UpdateView();
		}
    }
}
