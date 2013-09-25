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
        private Games game;
        public ContentForm(Games g)//Точка входа в программу
        {
            InitializeComponent();
            game = g;
            this.Text = "Дополнения для "+ game.Name;
            Init();
        }

        /// <summary>
        /// Инициализация главной формы. Выполняется загрузка данных из базы данных
        /// </summary>
        private void Init()
        {
            //Загружаем игры из базы
            var Query = from Games in Program.context.Games
                        where Games.ID_Content == game.ID_Game
                        orderby Games.Name
                        select Games;
            gamesBindingSource.DataSource = Query.ToList();
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
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
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
            if (((Games)gamesBindingSource.Current).Games2.Name.IndexOf(":") > 0)
            {
                GameName.Text = ((Games)gamesBindingSource.Current).Games2.Name + " - " + ((Games)gamesBindingSource.Current).Name;
            }
            else
                GameName.Text = ((Games)gamesBindingSource.Current).Games2.Name + ": " + ((Games)gamesBindingSource.Current).Name;
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
            Form AddC = new AddContent(game);
            if (AddC.ShowDialog() == DialogResult.OK)
            UpdateView();
        }

        private void delGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить дополнение " + ((Games)gamesBindingSource.Current).Name + " из базы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                DialogResult.Yes)
            {
                Program.context.Games.Remove((Games)gamesBindingSource.Current);
                Program.context.SaveChanges();
                UpdateView();
            }
        }

        private void editGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form EditGame = new AddContent(Program.context.Games.Find(((Games)gamesBindingSource.Current).ID_Game));
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
            Form Infa = new InformationWindows(0, ((Games)gamesBindingSource.Current).Developers);
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
            //Infa.Owner = this;
            Infa.Show();
        }
    }
}
