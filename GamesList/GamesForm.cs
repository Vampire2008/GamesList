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
        private Boolean fp, fd, frf, fop, fg, fpl, fs, fyr, fir, fda, fl;
        public GamesForm()//Точка входа в программу
        {
            InitializeComponent();
            /*var Query = from Games in Program.context.Games
                        select Games;
            gamesBindingSource.DataSource = Query.ToList();*/
            this.Text = "Список игр - " + Path.GetFileName(Properties.Settings.Default.DefaultConStr);
            label9.Text = label26.Text = "Рейтинг "+ Properties.Settings.Default.Recenzor+":";
            label1.Text = label14.Text = Properties.Settings.Default.DistrReg +":";
            numericUpDown1.Maximum = numericUpDown2.Maximum = Properties.Settings.Default.MaxYourRating;
            numericUpDown3.Maximum = numericUpDown4.Maximum = Properties.Settings.Default.MaxRecenzorRating;
            Init(); 
        }
        
        /// <summary>
        /// Инициализация главной формы. Выполняется загрузка данных из базы данных
        /// </summary>
        private void Init()
        {
            fp = fd = frf = fop = fg = fpl = fs = fyr = fir = fda = fl = true;
            //Загружаем игры из базы
            gamesBindingSource.DataSource = Program.context.Games.Local.ToBindingList();
            Program.context.Games.Load();
            //gamesBindingSource.Filter = "Name = 'DooM'";
            //Загружаем издателей из базы
            List<Publishers> LP = new List<Publishers>();
            LP.Add(new Publishers { Name = "<не важно>", Id_Publisher = 0 });
            LP.AddRange(Program.context.Publishers.ToArray());
            publishersBindingSource.DataSource = LP;
           // publishersBindingSource.DataSource = Program.context.Publishers.Local.ToBindingList();
            //Program.context.Publishers.Load();
            //Загружаем разработчиков из базы
            List<Developers> LD = new List<Developers>();
            LD.Add(new Developers {Name = "<не важно>", ID_Developer = 0});
            LD.AddRange(Program.context.Developers.ToArray());
            developersBindingSource.DataSource = LD;
            //developersBindingSource.DataSource = Program.context.Developers.Local.ToBindingList();
            //rogram.context.Developers.Load();
            //Загружаем издателей в России из базы
            List<RF_Distributors> LRF = new List<RF_Distributors>();
            LRF.Add(new RF_Distributors { Name = "<не важно>", ID_RF_Distributor = 0 });
            LRF.AddRange(Program.context.RF_Distributors.ToArray());
            rFDistributorsBindingSource.DataSource = LRF;
            //rFDistributorsBindingSource.DataSource = Program.context.RF_Distributors.Local.ToBindingList();
            //Program.context.RF_Distributors.Load();
            //Загружаем виды онлайн-защит из базы
            List<Online_protections> LOP = new List<Online_protections>();
            LOP.Add(new Online_protections { Name = "<не важно>", ID_Protect = 0 });
            LOP.AddRange(Program.context.Online_protections.ToArray());
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
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                return;
            }
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
                pictureBox2.Visible = true;
                pictureBox2.Left = SerName.Right + 5;
            }
            else
            {
                label33.Visible = false;//Или невидимым, если отсутствует
                SerName.Visible = false;
                pictureBox2.Visible = false;
            }

            if (((Games)gamesBindingSource.Current).Developers != null)//Если разработчик задан, то 
            {
                DevName.Text = ((Games)gamesBindingSource.Current).Developers.Name;//Отображаем его имя
                pictureBox3.Visible = true;
                pictureBox3.Left = DevName.Right + 5;
            }
            else
            {
                DevName.Text = "<отсутствует>";//Либо заглушку
                pictureBox3.Visible = false;
            }
            if (((Games)gamesBindingSource.Current).Publishers != null)//То же самое, что и разработчик
            {
                DistrName.Text = ((Games)gamesBindingSource.Current).Publishers.Name;
                pictureBox4.Visible = true;
                pictureBox4.Left = DistrName.Right + 5;
            }
            else
            {
                DistrName.Text = "<отсутствует>";
                pictureBox4.Visible = false;
            }
            if (((Games)gamesBindingSource.Current).RF_Distributors != null)//То же самое, что и разработчик
            {
                RFDistrName.Text = ((Games)gamesBindingSource.Current).RF_Distributors.Name;
                pictureBox5.Visible = true;
                pictureBox5.Left = RFDistrName.Right + 5;
            }
            else
            {
                RFDistrName.Text = "<отсутствует>";
                pictureBox5.Visible = false;
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
            if ((bool)((Games)gamesBindingSource.Current).Game_Type)
            {
                Pirat.Visible = false;
            }
            else
            {
                Pirat.Visible = true;
                Pirat.Left = GameName.Right + 10;
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
            
        }

        private void ChangeFilter(Boolean dis)
        {
            if ((dis) || ((fp == true) && (fd == true) && (frf == true) && (fop == true) && (fg == true) && (fpl == true) && (fs == true)&& (fyr ==true)&& (fir ==true)&& (fda ==true) && (fl == true)))
            {
                gamesBindingSource.DataSource = Program.context.Games.Local.ToBindingList();
                Program.context.Games.Load();
                ovalShape1.FillColor = Color.White;
            }
            else
            {
                double? n1 = Decimal.ToDouble(numericUpDown1.Value);
                double? n2 = Decimal.ToDouble(numericUpDown2.Value);
                double? n3 = Decimal.ToDouble(numericUpDown3.Value);
                double? n4 = Decimal.ToDouble(numericUpDown4.Value);
                var GQuery = from Games in Program.context.Games
                                                where (fp || Games.ID_Publisher == (decimal?)comboBox2.SelectedValue) &&
                                                (fd || Games.ID_Developer == (decimal?)comboBox3.SelectedValue) &&
                                                (frf || Games.ID_RF_Distributor == (decimal?)comboBox1.SelectedValue) &&
                                                (fop || Games.Online_protections.Select(o => o.ID_Protect).Contains((decimal)comboBox4.SelectedValue)) &&
                                                (fg || Games.Genres.Select(g => g.ID_Genre).Contains((decimal)comboBox5.SelectedValue)) &&
                                                (fpl || Games.Platforms.Select(p => p.ID_Platform).Contains((decimal)comboBox6.SelectedValue))  &&
                                                (fs || Games.Status_complite == (comboBox7.SelectedIndex-1)) &&
                                                (fl || Games.Localisation_Type == (comboBox10.SelectedIndex-1)) &&
                                                (fyr || (Games.Rate_person >= n1 && Games.Rate_person <= n2)) &&
                                                (fir || (Games.Rate_Igromania >= n3 && Games.Rate_Igromania <= n4)) &&
                                                (fda || (Games.Date_Release >= dateTimePicker1.Value && Games.Date_Release <= dateTimePicker2.Value))
                                                select Games;
                gamesBindingSource.DataSource = GQuery.ToList();
                ovalShape1.FillColor = Color.Green;
            }
        }

        private void managePublishersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManagePublisher = new ManagePDL(0);
            ManagePublisher.Text = "Управление издателями";
            ManagePublisher.ShowDialog();
        }

        private void GamesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void manageDevelopersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManagePublisher = new ManagePDL(1);
            ManagePublisher.Text = "Управление разработчиками";
            ManagePublisher.ShowDialog();
        }

        private void manageRFDistrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManagePublisher = new ManagePDL(2);
            ManagePublisher.Text = "Управление издателями в России";
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
                ChangeFilter(false);
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
            Form ДобавитьИгру = new AddGame(null);
            ДобавитьИгру.ShowDialog();
            UpdateView();
        }

        private void delGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить игру " + ((Games)gamesBindingSource.Current).Name + " из базы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.Yes)
            {
                Program.context.Games.Remove((Games)gamesBindingSource.Current);
                Program.context.SaveChanges();
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
            Form EditGame = new AddGame(Program.context.Games.Find(((Games)gamesBindingSource.Current).ID_Game));
            EditGame.ShowDialog();
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
                Init();
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
                }
                catch
                {
                    MessageBox.Show("Не удалось открыть файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.context = new GamesEntities(Program.buidConStr(Properties.Settings.Default.DefaultConStr));
                }
                Init();
                this.Text = "Список игр - " + Path.GetFileName(saveFileDialog1.FileName);
                this.Cursor = Cursors.Default;
            }
        }

        private void clearBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Это уничтожит все данные в текущей базе. Вы уверенны?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (MessageBox.Show("База очищена. Желаете ли добавить данные вспомогательных таблицы?", "Вспомогательные таблицы", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    MessageBox.Show("Данный функционал ещё не реализован. Пользуйтесь пустой базой.","Информация",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    Program.context.Database.Delete();
                    Program.context.Database.Create();
                    Init();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                var Que = Program.context.Games.Local.ToBindingList().Where(g => g.Name.ToLower().IndexOf(textBox1.Text.ToLower()) == 0);
                //MessageBox.Show(Program.context.Games.Local.Where(g => g.Name.ToLower().IndexOf(textBox1.Text.ToLower()) == 0).Count() == 0 ? "true" : "false");
                try
                {
                    gamesBindingSource.DataMember = "";
                    gamesBindingSource.DataSource = Que.Count() == 0 ? new Games[] {} : Que;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                gamesBindingSource.DataSource = Program.context.Games.Local.ToBindingList();
                Program.context.Games.Load();
            }
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
            if ((decimal)comboBox3.SelectedValue == 0)
            {
                fd = true;
            }
            else
            {
                fd = false;
                ovalShape1.FillColor = Color.Green;
            }
            ChangeFilter(false);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((decimal)comboBox1.SelectedValue == 0)
            {
                frf = true;
            }
            else
            {
                frf = false;
                ovalShape1.FillColor = Color.Green;
            }
            ChangeFilter(false);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((decimal)comboBox4.SelectedValue == 0)
            {
                fop = true;
            }
            else
            {
                fop = false;
                ovalShape1.FillColor = Color.Green;
            }
            ChangeFilter(false);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((decimal)comboBox5.SelectedValue == 0)
            {
                fg = true;
            }
            else
            {
                fg = false;
                ovalShape1.FillColor = Color.Green;
            }
            ChangeFilter(false);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((decimal)comboBox6.SelectedValue == 0)
            {
                fpl = true;
            }
            else
            {
                fpl = false;
                ovalShape1.FillColor = Color.Green;
            }
            ChangeFilter(false);
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
                ovalShape1.FillColor = Color.Green;
            }
            ChangeFilter(false);
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
                ovalShape1.FillColor = Color.Green;
            }
            ChangeFilter(false);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            fyr = false;
            ovalShape1.FillColor = Color.Green;
            ChangeFilter(false);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            fyr = false;
            ovalShape1.FillColor = Color.Green;
            ChangeFilter(false);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            fir = false;
            ovalShape1.FillColor = Color.Green;
            ChangeFilter(false);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            fir = false;
            ovalShape1.FillColor = Color.Green;
            ChangeFilter(false);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            fda = false;
            ovalShape1.FillColor = Color.Green;
            ChangeFilter(false);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            fda = false;
            ovalShape1.FillColor = Color.Green;
            ChangeFilter(false);
        }

        private void ovalShape1_Click(object sender, EventArgs e)
        {
            if (ovalShape1.FillColor == Color.Green)
            {
                ovalShape1.FillColor = Color.White;
                ChangeFilter(true);
            }
            else
            {
                ovalShape1.FillColor = Color.Green;
                ChangeFilter(false);
            }
        }

        private void DevName_Click(object sender, EventArgs e)
        {
            Form Infa = new InformationWindows(0,((Games)gamesBindingSource.Current).Developers);
            Infa.Show();
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
                label26.Text = "Оценка " + Properties.Settings.Default.Recenzor;
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
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form Infa = new InformationWindows(1, ((Games)gamesBindingSource.Current).Publishers);
            Infa.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form Infa = new InformationWindows(2, ((Games)gamesBindingSource.Current).RF_Distributors);
            Infa.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form Infa = new SeriesInf(((Games)gamesBindingSource.Current).Series);
            Infa.Owner = this;
            Infa.Show();
        }
    }
}
