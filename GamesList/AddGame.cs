using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GamesList.Model;
using System.Data.Entity;
using System.IO;
using System.Drawing.Imaging;

namespace GamesList
{
    public partial class AddGame : Form
    {
        private Games AddingGame;
        private Game_disks[] Disks;
        private Platforms[] GamePlatforms;
        private Genres[] GameGenres;
        private Online_protections[] OnlineProtections;

        public AddGame(Games game)
        {
            InitializeComponent();
            rate_IgromaniaLabel.Text = "Рейтинг " + Properties.Settings.Default.Recenzor+":";
            label3.Text = Properties.Settings.Default.DistrReg;
            PersonRate.Maximum = Properties.Settings.Default.MaxYourRating;
            numericUpDown2.Maximum = Properties.Settings.Default.MaxRecenzorRating;
            PlatformsList.DataSource = platformsBindingSource;
            PlatformsList.DisplayMember = "Name";
            PlatformsList.ValueMember = "ID_Platform";
            PlatformsList.DataSource = Program.context.Platforms.Local.ToBindingList();
            Program.context.Platforms.Load();
            OnlineProtectionsList.DataSource = online_protectionsBindingSource;
            OnlineProtectionsList.DisplayMember = "Name";
            OnlineProtectionsList.ValueMember = "ID_Protect";
            OnlineProtectionsList.DataSource = Program.context.Online_protections.Local.ToBindingList();
            Program.context.Platforms.Load();
            GenresList.DataSource = genresBindingSource;
            GenresList.DisplayMember = "Name";
            GenresList.ValueMember = "ID_Genre";
            GenresList.DataSource = Program.context.Genres.Local.ToBindingList();
            Program.context.Genres.Load();
            DiskTypes.DataSource = Program.context.Disk_types.Local.ToBindingList();
            Program.context.Disk_types.Load();
            List<Publishers> LP = new List<Publishers>();
            LP.Add(new Publishers { Name = "<отсутствует>", Id_Publisher = 0 });
            LP.AddRange(Program.context.Publishers.ToArray());
            comboBox2.DataSource = LP;
            //Program.context.Publishers.Load();
            //comboBox1.DataSource = Program.context.Developers.Local.ToBindingList();
            //Program.context.Developers.Load();
            List<Developers> LD = new List<Developers>();
            LD.Add(new Developers { Name = "<отсутствует>", ID_Developer = 0 });
            LD.AddRange(Program.context.Developers.ToArray());
            comboBox1.DataSource = LD;
            //comboBox1.DisplayMember = "Name";
            //comboBox1.ValueMember = "ID_Developer";
            //comboBox1.SelectedIndex = 0;

            List<RF_Distributors> LRF = new List<RF_Distributors>();
            LRF.Add(new RF_Distributors { Name = "<отсутствует>", ID_RF_Distributor = 0 });
            LRF.AddRange(Program.context.RF_Distributors.ToArray());
            comboBox3.DataSource = LRF;
            //Program.context.RF_Distributors.Load();

            List<Series> LS = new List<Series>();
            LS.Add(new Series { Name = "<отсутствует>", ID_Ser = 0 });
            LS.AddRange(Program.context.Series.ToArray());
            comboBox8.DataSource = LS;

            comboBox6.DataSource = Program.context.Editions.Local.ToBindingList();
            Program.context.Editions.Load();

            comboBox7.DataSource = Program.context.Boxes.Local.ToBindingList();
            Program.context.Boxes.Load();
            if (game == null)
            {
                AddingGame = Program.context.Games.Create();
                AddingGame.Game_Type = false;
                AddingGame.ID_Developer = 0;
                AddingGame.ID_Publisher = 0;
                AddingGame.ID_RF_Distributor = 0;
                AddingGame.ID_Box = 1;
                AddingGame.ID_Edition = 1;
                AddingGame.ID_Ser = 0;
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
                AddBut.Visible = true;
                EditButton.Visible = false;
            }
            else
            {
                AddingGame = game;
                AddBut.Visible = false;
                EditButton.Visible = true;
                if (AddingGame.Original_Name != null)
                {
                    OriginalNameEn.Checked = true;
                    original_NameTextBox.Enabled = true;
                }
                else
                {
                    OriginalNameEn.Checked = false;
                    original_NameTextBox.Enabled = false;
                }
                if (AddingGame.ID_Developer == null)
                {
                    AddingGame.ID_Developer = 0;
                }
                if (AddingGame.ID_Publisher == null)
                {
                    AddingGame.ID_Publisher = 0;
                }
                if (AddingGame.ID_RF_Distributor == null)
                {
                    AddingGame.ID_RF_Distributor = 0;
                }
                if (AddingGame.ID_Ser == null)
                {
                    AddingGame.ID_Ser = 0;
                }
                comboBox4.SelectedIndex = Decimal.ToInt32((decimal)AddingGame.Localisation_Type);
                comboBox5.SelectedIndex = Decimal.ToInt32((decimal)AddingGame.Status_complite);
                foreach (Game_disks gd in AddingGame.Game_disks)
                {
                    GameDiskList.Items.Add(new Game_disks
                    {
                        ID_Disk_Type =gd.ID_Disk_Type,
                        ID_Game = gd.ID_Game,
                        ID_Game_disk = gd.ID_Game_disk,
                        Disk_types = gd.Disk_types,
                        Games = gd.Games,
                        Kol_vo = gd.Kol_vo
                    });
                }
                Disks = new Game_disks[GameDiskList.Items.Count];
                GameDiskList.Items.CopyTo(Disks, 0);
                for (int i = 0; i < PlatformsList.Items.Count; i++)
                {
                    if (AddingGame.Platforms.SingleOrDefault(p => p.ID_Platform == ((Platforms)PlatformsList.Items[i]).ID_Platform) != null)
                    {
                        PlatformsList.SetItemChecked(i, true);
                    }
                }
                GamePlatforms = new Platforms[PlatformsList.CheckedItems.Count];
                PlatformsList.CheckedItems.CopyTo(GamePlatforms, 0);
                for (int i = 0; i < GenresList.Items.Count; i++)
                {
                    if (AddingGame.Genres.SingleOrDefault(p => p.ID_Genre == ((Genres)GenresList.Items[i]).ID_Genre) != null)
                    {
                        GenresList.SetItemChecked(i, true);
                    }
                }
                GameGenres = new Genres[GenresList.CheckedItems.Count];
                GenresList.CheckedItems.CopyTo(GameGenres, 0);
                for (int i = 0; i < OnlineProtectionsList.Items.Count; i++)
                {
                    if (AddingGame.Online_protections.SingleOrDefault(p => p.ID_Protect == ((Online_protections)OnlineProtectionsList.Items[i]).ID_Protect) != null)
                    {
                        OnlineProtectionsList.SetItemChecked(i, true);
                    }
                }
                OnlineProtections = new Online_protections[OnlineProtectionsList.CheckedItems.Count];
                OnlineProtectionsList.CheckedItems.CopyTo(OnlineProtections, 0);
                if (AddingGame.Poster != null)
                {
                    var stream = new MemoryStream(AddingGame.Poster);
                    posterPictureBox.Image = Image.FromStream(stream);
                }
                else
                {
                    //pictureBox1.Image.Dispose();
                    posterPictureBox.Image = null;
                }
            }
            gamesBindingSource.DataSource = new[] { AddingGame };
            
        }

        private void OriginalNameEn_CheckedChanged(object sender, EventArgs e)
        {
            original_NameTextBox.Enabled = OriginalNameEn.Checked;
        }

        private void Platforms_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((Platforms)e.Value).Name;
        }

        private void GenresList_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((Genres)e.Value).Name;
        }

        private void OnlineProtectionsList_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((Online_protections)e.Value).Name;
        }

        private void listBox1_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.Value != null)
            e.Value = ((Game_disks)e.Value).Kol_vo + " " + ((Game_disks)e.Value).Disk_types.Name;
        }

        private void AddDisk_Click(object sender, EventArgs e)
        {
            if (ColDisks.Value == 0) { return; }
            for (int i = 0; i < GameDiskList.Items.Count; i++)
                if (((Game_disks)GameDiskList.Items[i]).ID_Disk_Type == (decimal)DiskTypes.SelectedValue)
                {
                    ((Game_disks)GameDiskList.Items[i]).Kol_vo = ColDisks.Value;
                    if (AddingGame.ID_Game > 0)
                    {
                        ((Game_disks)GameDiskList.Items[i]).ID_Disk_Type = 0;
                    }
                    Game_disks[] l = new Game_disks[GameDiskList.Items.Count];
                    GameDiskList.Items.CopyTo(l,0);
                    GameDiskList.Items.Clear();
                    GameDiskList.Items.AddRange(l);
                    return;
                }
            
            GameDiskList.Items.Add(new Game_disks()
            {
                ID_Game_disk = 0,
                ID_Disk_Type = (decimal)DiskTypes.SelectedValue,
                Disk_types = Program.context.Disk_types.Find(DiskTypes.SelectedValue),
                Kol_vo = ColDisks.Value
            });
        }

        private void DiskTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RemoveDisk_Click(object sender, EventArgs e)
        {
            GameDiskList.Items.Remove(GameDiskList.SelectedItem);
        }

        private void GameDiskList_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(((Game_disks)GameDiskList.SelectedItem).Kol_vo.ToString(), "111");
        }

        private void AddGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.context.RejectChanges(AddingGame);
        }

        private void AddBut_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                rectangleShape1.Visible = true;
                MessageBox.Show("Введите хотя бы имя игры!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                if (!OriginalNameEn.Checked)
                {
                    AddingGame.Original_Name = null;
                }
                progressBar1.Value = 10;
                if (numericUpDown1.Value == 0)
                {
                    AddingGame.Kol_updates = 0;
                }
                progressBar1.Value = 20;
                if (posterPictureBox.Image != null)
                {
                    var stream = new MemoryStream();
                    posterPictureBox.Image.Save(stream, posterPictureBox.Image.RawFormat);
                    AddingGame.Poster = stream.GetBuffer();
                }
                progressBar1.Value = 30;
                AddingGame.Localisation_Type = comboBox4.SelectedIndex;
                progressBar1.Value = 40;
                AddingGame.Status_complite = comboBox5.SelectedIndex;
                progressBar1.Value = 50;
                AddingGame.ID_Game = (Program.context.Games.Count() > 0 ? Program.context.Games.Max(p => p.ID_Game) : 0) + 1;
                progressBar1.Value = 60;
                //Game_platforms gp;
                AddingGame.Platforms = new List<Platforms>();
                foreach (Platforms p in PlatformsList.CheckedItems)
                {
                   /* gp = Program.context.Game_platforms.Create();
                    gp.Games = game;
                    gp.ID_Game = (decimal)game.ID_Game;
                    gp.Platforms = Program.context.Platforms.Find(p.ID_Platform);
                    gp.ID_Platform = p.ID_Platform;
                    gp.ID_Game_Platform = (Program.context.Game_platforms.Count() > 0 ? Program.context.Game_platforms.Max(pp => pp.ID_Game_Platform) : 0) + 1;
                    Program.context.Game_platforms.Add(gp);
                    Program.context.SaveChanges();
                    gp = null;*/
                    AddingGame.Platforms.Add(Program.context.Platforms.Find(p.ID_Platform));
                }
                progressBar1.Value = 70;
                AddingGame.Genres = new List<Genres>();
                //Game_Genres gg;
                foreach (Genres p in GenresList.CheckedItems)
                {
                   /* gg = Program.context.Game_Genres.Create();
                    gg.Games = game;
                    gg.ID_Game = (decimal)game.ID_Game;
                    gg.Genres = Program.context.Genres.Find(p.ID_Genre);
                    gg.ID_Genre = p.ID_Genre;
                    gg.ID_Game_Genres = (Program.context.Game_Genres.Count() > 0 ? Program.context.Game_Genres.Max(pp => pp.ID_Game_Genres) : 0) + 1;
                    Program.context.Game_Genres.Add(gg);
                    Program.context.SaveChanges();
                    gg = null;*/
                    AddingGame.Genres.Add(Program.context.Genres.Find(p.ID_Genre));
                }
                progressBar1.Value = 80;
                //Game_protections gop;
                AddingGame.Online_protections = new List<Online_protections>();
                foreach (Online_protections p in OnlineProtectionsList.CheckedItems)
                {
                   /* gop = Program.context.Game_protections.Create();
                    gop.Games = game;
                    gop.ID_Game = (decimal)game.ID_Game;
                    gop.Online_protections = Program.context.Online_protections.Find(p.ID_Protect);
                    gop.ID_Protect = p.ID_Protect;
                    gop.ID_Game_protect = (Program.context.Game_protections.Count() > 0 ? Program.context.Game_protections.Max(pp => pp.ID_Game_protect) : 0) + 1;
                    Program.context.Game_protections.Add(gop);
                    Program.context.SaveChanges();
                    gop = null;*/
                    AddingGame.Online_protections.Add(Program.context.Online_protections.Find(p.ID_Protect));
                }
                progressBar1.Value = 90;
                Program.context.Games.Add(AddingGame);
                if (AddingGame.ID_Developer == 0)
                {
                    AddingGame.ID_Developer = null;
                }
                if (AddingGame.ID_Publisher == 0)
                {
                    AddingGame.ID_Publisher = null;
                }
                if (AddingGame.ID_RF_Distributor == 0)
                {
                    AddingGame.ID_RF_Distributor = null;
                }
                if (AddingGame.ID_Ser == 0)
                {
                    AddingGame.ID_Ser = null;
                }
                Program.context.SaveChanges();
                var ID_G = Program.context.Games.Max(p => p.ID_Game);
                var game = Program.context.Games.Find(ID_G);
                progressBar1.Value = 95;
                Game_disks gd;
                foreach (Game_disks p in GameDiskList.Items)
                {
                    gd = Program.context.Game_disks.Create();
                    gd.Games = game;
                    gd.ID_Game = (decimal)game.ID_Game;
                    gd.Disk_types = p.Disk_types;
                    gd.ID_Disk_Type = p.ID_Disk_Type;
                    gd.ID_Game_disk = (Program.context.Game_disks.Count() > 0 ? Program.context.Game_disks.Max(pp => pp.ID_Game_disk) : 0) + 1;
                    gd.Kol_vo = p.Kol_vo;
                    Program.context.Game_disks.Add(gd);
                    Program.context.SaveChanges();
                    gd = null;
                }
                progressBar1.Value = 100;
            DialogResult = DialogResult.OK;
        }

        private void posterPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (ChooseImage.ShowDialog() == DialogResult.OK)
            {
                posterPictureBox.Image = Image.FromFile(ChooseImage.FileName);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                rectangleShape1.Visible = true;
                MessageBox.Show("Введите хотя бы имя игры!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!OriginalNameEn.Checked)
            {
                AddingGame.Original_Name = null;
            }
            progressBar1.Value = 10;
            if (numericUpDown1.Value == 0)
            {
                AddingGame.Kol_updates = 0;
            }
            progressBar1.Value = 20;
            if (posterPictureBox.Image != null)
            {
                var stream = new MemoryStream();
                posterPictureBox.Image.Save(stream, posterPictureBox.Image.RawFormat);
                AddingGame.Poster = stream.GetBuffer();
            }
            progressBar1.Value = 30;
            AddingGame.Localisation_Type = comboBox4.SelectedIndex;
            AddingGame.Status_complite = comboBox5.SelectedIndex;
            progressBar1.Value = 40;
            //Обновление платформ
            Platforms[] gpp = new Platforms[PlatformsList.CheckedItems.Count];
            PlatformsList.CheckedItems.CopyTo(gpp, 0);
            List<Platforms> glp = GamePlatforms.ToList();
            Platforms[] PlfArr = new Platforms[PlatformsList.CheckedItems.Count];
            PlatformsList.CheckedItems.CopyTo(PlfArr, 0);
            List<Platforms> PlfLst = PlfArr.ToList();
            foreach (Platforms u in gpp)
            {
                if (PlfLst.Contains(u) && glp.Contains(u))
                {
                    PlfLst.Remove(u);
                    glp.Remove(u);
                }
            }
            //Game_platforms gp;
            foreach (Platforms p in PlfLst)
            {
               /* gp = Program.context.Game_platforms.Create();
                gp.Games = game;
                gp.ID_Game = (decimal)game.ID_Game;
                gp.Platforms = Program.context.Platforms.Find(p.ID_Platform);
                gp.ID_Platform = p.ID_Platform;
                gp.ID_Game_Platform = (Program.context.Game_platforms.Count() > 0 ? Program.context.Game_platforms.Max(pp => pp.ID_Game_Platform) : 0) + 1;
                Program.context.Game_platforms.Add(gp);
                Program.context.SaveChanges();
                gp = null;*/
                AddingGame.Platforms.Add(Program.context.Platforms.Find(p.ID_Platform));
            }
            foreach (Platforms p in glp)
            {
                /*Program.context.Platforms.Remove(Program.context.Platforms.Find(p.ID_Platform));
                Program.context.SaveChanges();*/
                AddingGame.Platforms.Remove(Program.context.Platforms.Find(p.ID_Platform));
            }
            progressBar1.Value = 50;
            //Обновление жанров
            Genres[] ggg = new Genres[GenresList.CheckedItems.Count];
            GenresList.CheckedItems.CopyTo(ggg, 0);
            List<Genres> glg = GameGenres.ToList();
            Genres[] GnrArr = new Genres[GenresList.CheckedItems.Count];
            GenresList.CheckedItems.CopyTo(GnrArr, 0);
            List<Genres> GnrLst = GnrArr.ToList();
            foreach (Genres u in ggg)
            {
                if (GnrLst.Contains(u) && glg.Contains(u))
                {
                    GnrLst.Remove(u);
                    glg.Remove(u);
                }
            }
            //Game_Genres gg;
            foreach (Genres p in GnrLst)
            {
              /*  gg = Program.context.Game_Genres.Create();
                gg.Games = game;
                gg.ID_Game = (decimal)game.ID_Game;
                gg.Genres = Program.context.Genres.Find(p.ID_Genre);
                gg.ID_Genre = p.ID_Genre;
                gg.ID_Game_Genres = (Program.context.Game_Genres.Count() > 0 ? Program.context.Game_Genres.Max(pp => pp.ID_Game_Genres) : 0) + 1;
                Program.context.Game_Genres.Add(gg);
                Program.context.SaveChanges();
                gg = null;*/
                AddingGame.Genres.Add(Program.context.Genres.Find(p.ID_Genre));
            }
            foreach (Genres p in glg)
            {
                /*Program.context.Genres.Remove(Program.context.Genres.Find(p.ID_Genre));
                Program.context.SaveChanges();*/
                AddingGame.Genres.Remove(Program.context.Genres.Find(p.ID_Genre));
            }
            progressBar1.Value = 60;
            //Обновление защит
            Online_protections[] gopp = new Online_protections[OnlineProtectionsList.CheckedItems.Count];
            OnlineProtectionsList.CheckedItems.CopyTo(gopp, 0);
            List<Online_protections> glop = OnlineProtections.ToList();
            Online_protections[] OprArr = new Online_protections[OnlineProtectionsList.CheckedItems.Count];
            OnlineProtectionsList.CheckedItems.CopyTo(OprArr, 0);
            List<Online_protections> OprLst = OprArr.ToList();
            foreach (Online_protections u in gopp)
            {
                if (OprLst.Contains(u) && glop.Contains(u))
                {
                    OprLst.Remove(u);
                    glop.Remove(u);
                }
            }
            //Game_protections gop;
            foreach (Online_protections p in OprLst)
            {
                /*gop = Program.context.Game_protections.Create();
                gop.Games = game;
                gop.ID_Game = (decimal)game.ID_Game;
                gop.Online_protections = Program.context.Online_protections.Find(p.ID_Protect);
                gop.ID_Protect = p.ID_Protect;
                gop.ID_Game_protect = (Program.context.Game_protections.Count() > 0 ? Program.context.Game_protections.Max(pp => pp.ID_Game_protect) : 0) + 1;
                Program.context.Game_protections.Add(gop);
                Program.context.SaveChanges();
                gop = null;*/
                AddingGame.Online_protections.Add(Program.context.Online_protections.Find(p.ID_Protect));
            }
            foreach (Online_protections p in glop)
            {
               /* Program.context.Online_protections.Remove(Program.context.Online_protections.Find(p.ID_Protect));
                Program.context.SaveChanges();*/
                AddingGame.Online_protections.Remove(Program.context.Online_protections.Find(p.ID_Protect));
            }
            if (AddingGame.ID_Developer == 0)
            {
                AddingGame.ID_Developer = null;
            }
            if (AddingGame.ID_Publisher == 0)
            {
                AddingGame.ID_Publisher = null;
            }
            if (AddingGame.ID_RF_Distributor == 0)
            {
                AddingGame.ID_RF_Distributor = null;
            }
            if (AddingGame.ID_Ser == 0)
            {
                AddingGame.ID_Ser = null;
            }
            try
            {
                Program.context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка:\n" + ex, "Ошибка", MessageBoxButtons.OK);
            }
            progressBar1.Value = 70;
            var ID_G = AddingGame.ID_Game;
            var game = Program.context.Games.Find(ID_G);
            progressBar1.Value = 80;
            //Обновление дисков
            Game_disks[] gdd = new Game_disks[GameDiskList.Items.Count];
            GameDiskList.Items.CopyTo(gdd, 0);
            List<Game_disks> gld = Disks.ToList();
            foreach (Game_disks u in gdd)
            {
                if (GameDiskList.Items.Contains(u) && gld.Contains(u))
                {
                    if (u.ID_Disk_Type == 0)
                    {
                        gld.Remove(u);
                    }
                    else
                    {
                        GameDiskList.Items.Remove(u);
                        gld.Remove(u);
                    }
                }
            }
            Game_disks gd;
            foreach (Game_disks p in GameDiskList.Items)
            {
                if (p.ID_Game_disk == 0)
                {
                    gd = Program.context.Game_disks.Create();
                    gd.Games = game;
                    gd.ID_Game = (decimal)game.ID_Game;
                    gd.Disk_types = p.Disk_types;
                    gd.ID_Disk_Type = p.ID_Disk_Type;
                    gd.ID_Game_disk = (Program.context.Game_disks.Count() > 0 ? Program.context.Game_disks.Max(pp => pp.ID_Game_disk) : 0) + 1;
                    gd.Kol_vo = p.Kol_vo;
                    Program.context.Game_disks.Add(gd);
                    Program.context.SaveChanges();
                    gd = null;
                }
                else if (p.ID_Disk_Type == 0)
                {
                    gd = Program.context.Game_disks.Find(p.ID_Game_disk);
                    gd.Kol_vo = p.Kol_vo;
                    Program.context.SaveChanges();
                }
            }
            foreach (Game_disks p in gld)
            {
                Program.context.Game_disks.Remove(Program.context.Game_disks.Find(p.ID_Game_disk));
                Program.context.SaveChanges();
            }
            progressBar1.Value = 100;
            DialogResult = DialogResult.OK;
        }

        private void убратьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            posterPictureBox.Image = null;
        }
    }
    
}
