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
    public partial class AddContent : Form
    {
        private Games AddingGame;
        private Game_disks[] Disks;
        private Platforms[] GamePlatforms;
        private Online_protections[] OnlineProtections;
        private Boolean Compl = false;

        public AddContent(Games game, byte ctc)
        {
            InitializeComponent();
            rate_IgromaniaLabel.Text = "Рейтинг " + Properties.Settings.Default.Recenzor + ":";
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
            /*GenresList.DataSource = genresBindingSource;
            GenresList.DisplayMember = "Name";
            GenresList.ValueMember = "ID_Genre";
            GenresList.DataSource = Program.context.Genres.Local.ToBindingList();*/
            Program.context.Genres.Load();
            DiskTypes.DataSource = Program.context.Disk_types.Local.ToBindingList();
            Program.context.Disk_types.Load();
            List<Publishers> LP = new List<Publishers>();
            LP.Add(new Publishers { Name = "<отсутствует>", Id_Publisher = 0 });
            LP.AddRange(Program.context.Publishers.OrderBy(p => p.Name).ToArray());
            comboBox2.DataSource = LP;
            //Program.context.Publishers.Load();
            //comboBox1.DataSource = Program.context.Developers.Local.ToBindingList();
            //Program.context.Developers.Load();
            List<Developers> LD = new List<Developers>();
            LD.Add(new Developers { Name = "<отсутствует>", ID_Developer = 0 });
            LD.AddRange(Program.context.Developers.OrderBy(d => d.Name).ToArray());
            comboBox1.DataSource = LD;
            //comboBox1.DisplayMember = "Name";
            //comboBox1.ValueMember = "ID_Developer";
            //comboBox1.SelectedIndex = 0;

            List<RF_Distributors> LRF = new List<RF_Distributors>();
            LRF.Add(new RF_Distributors { Name = "<отсутствует>", ID_RF_Distributor = 0 });
            LRF.AddRange(Program.context.RF_Distributors.OrderBy(r => r.Name).ToArray());
            comboBox3.DataSource = LRF;
            //Program.context.RF_Distributors.Load();

            /*List<Series> LS = new List<Series>();
            LS.Add(new Series { Name = "<отсутствует>", ID_Ser = 0 });
            LS.AddRange(Program.context.Series.ToArray());
            comboBox8.DataSource = LS;*/

            comboBox6.DataSource = Program.context.Editions.Local.ToBindingList();
            Program.context.Editions.Load();

            comboBox7.DataSource = Program.context.Boxes.Local.ToBindingList();
            Program.context.Boxes.Load();
            if ((game.ID_Content == null)||(ctc==1))
            {
                AddingGame = Program.context.Games.Create();
                AddingGame.Game_Type = false;
                AddingGame.TypeContent = false;
                /*AddingGame.Last_version = game.Last_version;
                last_versionTextBox.Enabled=false;
                AddingGame.Kol_updates = game.Kol_updates;
                numericUpDown1.Enabled = false;*/
                AddingGame.ID_Developer = game.ID_Developer;
                AddingGame.Developers = game.Developers;
                if (AddingGame.ID_Developer == null)
                {
                    AddingGame.ID_Developer = 0;
                }
                AddingGame.ID_Publisher = game.ID_Publisher;
                AddingGame.Publishers = game.Publishers;
                if (AddingGame.ID_Publisher == null)
                    AddingGame.ID_Publisher = 0;
                AddingGame.ID_RF_Distributor = game.ID_RF_Distributor;
                AddingGame.RF_Distributors = game.RF_Distributors;
                if (AddingGame.ID_RF_Distributor == null)
                    AddingGame.ID_RF_Distributor = 0;
                AddingGame.ID_Box = 1;
                AddingGame.ID_Edition = 1;
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
                AddBut.Visible = true;
                EditButton.Visible = false;
                /*AddingGame.Online_protections = game.Online_protections;
                for (int i = 0; i < OnlineProtectionsList.Items.Count; i++)
                {
                    if (AddingGame.Online_protections.SingleOrDefault(p => p.ID_Protect == ((Online_protections)OnlineProtectionsList.Items[i]).ID_Protect) != null)
                    {
                        OnlineProtectionsList.SetItemChecked(i, true);
                    }
                }
                OnlineProtectionsList.Enabled = false;
                AddingGame.Platforms = game.Platforms;
                for (int i = 0; i < PlatformsList.Items.Count; i++)
                {
                    if (AddingGame.Platforms.SingleOrDefault(p => p.ID_Platform == ((Platforms)PlatformsList.Items[i]).ID_Platform) != null)
                    {
                        PlatformsList.SetItemChecked(i, true);
                    }
                }
                PlatformsList.Enabled = false;*/
                AddingGame.ID_Ser = game.ID_Ser;
                AddingGame.Series = game.Series;
                AddingGame.Genres = game.Genres;

                AddingGame.ID_Content = game.ID_Game;
                AddingGame.Games2 = game;
                changeTypeContent(false);
                this.AcceptButton = AddBut;
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
                /*if (AddingGame.ID_Ser == null)
                {
                    AddingGame.ID_Ser = 0;
                }*/
                comboBox4.SelectedIndex = Decimal.ToInt32((decimal)AddingGame.Localisation_Type);
                comboBox5.SelectedIndex = Decimal.ToInt32((decimal)AddingGame.Status_complite);
                foreach (Game_disks gd in AddingGame.Game_disks)
                {
                    GameDiskList.Items.Add(new Game_disks
                    {
                        ID_Disk_Type = gd.ID_Disk_Type,
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
                /*for (int i = 0; i < GenresList.Items.Count; i++)
                {
                    if (AddingGame.Genres.SingleOrDefault(p => p.ID_Genre == ((Genres)GenresList.Items[i]).ID_Genre) != null)
                    {
                        GenresList.SetItemChecked(i, true);
                    }
                }
                GameGenres = new Genres[GenresList.CheckedItems.Count];
                GenresList.CheckedItems.CopyTo(GameGenres, 0);*/
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
                    label13.Visible = false;
                }
                else
                {
                    //pictureBox1.Image.Dispose();
                    posterPictureBox.Image = null;
                }
                changeTypeContent((bool)AddingGame.TypeContent);
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button8.Enabled = false;
                this.AcceptButton = EditButton;
            }

            if (AddingGame.ID_Ser != null)
            {
                SerName.Text = AddingGame.Series.Name;
            }
            else
            {
                SerName.Text = "<отсутствует>";
            }

            GenLst.Text = "";
            foreach (Genres g in AddingGame.Genres)
            {
                GenLst.Text = GenLst.Text + g.Name + "\n";
            }
            if (AddingGame.Games2.Games2 != null)
            {
                ParentGameName.Text = AddingGame.Games2.Games2.Name;
                if (ParentGameName.Text.IndexOf(":") > 0)
                {
                    ParentGameName.Text += " - ";
                }
                else
                {
                    ParentGameName.Text += ": ";
                }
                ParentGameName.Text += AddingGame.Games2.Name + " - ";
				StandAlone.Checked = false;
				StandAlone.Enabled = false;
            }
            else
            {
                ParentGameName.Text = AddingGame.Games2.Name;
                if (ParentGameName.Text.IndexOf(":") > 0)
                {
                    ParentGameName.Text += " - ";
                }
                else
                {
                    ParentGameName.Text += ": ";
                }
            }
            nameTextBox.Left = ParentGameName.Right + 5;
            rectangleShape1.Left = ParentGameName.Right + 3;
            
            gamesBindingSource.DataSource = new[] { AddingGame };
        }

        private void changeTypeContent(bool st)
        {
            if (st)
            {
                last_versionTextBox.Enabled = true;
                numericUpDown1.Enabled = true;
                OnlineProtectionsList.Enabled = true;
                PlatformsList.Enabled = true;
                if (AddingGame.ID_Game < 1)
                {
                    button6.Enabled = true;
                    button7.Enabled = true;
                }
            }
            else
            {
                AddingGame.Last_version = AddingGame.Games2.Last_version;
                last_versionTextBox.Enabled = false;
                AddingGame.Kol_updates = AddingGame.Games2.Kol_updates;
                numericUpDown1.Enabled = false;
                AddingGame.Online_protections = AddingGame.Games2.Online_protections;
                for (int i = 0; i < OnlineProtectionsList.Items.Count; i++)
                {
                    if (AddingGame.Online_protections.SingleOrDefault(p => p.ID_Protect == ((Online_protections)OnlineProtectionsList.Items[i]).ID_Protect) != null)
                    {
                        OnlineProtectionsList.SetItemChecked(i, true);
                    }
                }
                OnlineProtectionsList.Enabled = false;
                AddingGame.Platforms = AddingGame.Games2.Platforms;
                for (int i = 0; i < PlatformsList.Items.Count; i++)
                {
                    if (AddingGame.Platforms.SingleOrDefault(p => p.ID_Platform == ((Platforms)PlatformsList.Items[i]).ID_Platform) != null)
                    {
                        PlatformsList.SetItemChecked(i, true);
                    }
                }
                PlatformsList.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
            }
        }

        private void OriginalNameEn_CheckedChanged(object sender, EventArgs e)
        {
            original_NameTextBox.Enabled = OriginalNameEn.Checked;
        }

        private void Platforms_Format(object sender, ListControlConvertEventArgs e)
        {
            try
            {
                e.Value = ((Platforms)e.Value).Name;
            }
            catch { }
        }

        private void OnlineProtectionsList_Format(object sender, ListControlConvertEventArgs e)
        {
            try
            {
                e.Value = ((Online_protections)e.Value).Name;
            }
            catch { }
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
                    ((Game_disks)GameDiskList.Items[i]).Kol_vo = (double)Decimal.ToDouble(ColDisks.Value);
                    if (AddingGame.ID_Game > 0)
                    {
                        ((Game_disks)GameDiskList.Items[i]).ID_Disk_Type = 0;
                    }
                    Game_disks[] l = new Game_disks[GameDiskList.Items.Count];
                    GameDiskList.Items.CopyTo(l, 0);
                    GameDiskList.Items.Clear();
                    GameDiskList.Items.AddRange(l);
                    return;
                }

            GameDiskList.Items.Add(new Game_disks()
            {
                ID_Game_disk = 0,
                ID_Disk_Type = (decimal)DiskTypes.SelectedValue,
                Disk_types = Program.context.Disk_types.Find(DiskTypes.SelectedValue),
                Kol_vo = (double)Decimal.ToDouble(ColDisks.Value)
            });
        }

        /*private void DiskTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }*/

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
            if ((nameTextBox.Text != "") && (nameTextBox.Text != null) && (!Compl))
            {
                Form SV = new SaveNotSaveDialog();
                DialogResult DR = SV.ShowDialog();
                switch (DR)
                {
                    case DialogResult.OK:
                        if (AddingGame.ID_Game > 0)
                        {
                            EditButton.PerformClick();
                        }
                        else
                        {
                            AddBut.PerformClick();
                        }
                        break;
                    case DialogResult.No:
                        Program.context.RejectChanges(AddingGame);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                Program.context.RejectChanges(AddingGame);
            }
        }

        private void AddBut_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                rectangleShape1.Visible = true;
                MessageBox.Show("Введите хотя бы имя игры!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            if (!OriginalNameEn.Checked)
            {
                AddingGame.Original_Name = null;
            }
            if (posterPictureBox.Image != null)
            {
                var stream = new MemoryStream();
                posterPictureBox.Image.Save(stream, posterPictureBox.Image.RawFormat);
                AddingGame.Poster = stream.GetBuffer();
            }
            else
            {
                AddingGame.Poster = null;
            }
            AddingGame.Localisation_Type = comboBox4.SelectedIndex;
            AddingGame.Status_complite = comboBox5.SelectedIndex;
            AddingGame.ID_Game = (Program.context.Games.Count() > 0 ? Program.context.Games.Max(p => p.ID_Game) : 0) + 1;
            if ((bool)AddingGame.TypeContent)
            {
                if (numericUpDown1.Value == 0)
                {
                    AddingGame.Kol_updates = 0;
                }
                AddingGame.Platforms = new List<Platforms>();
                foreach (Platforms p in PlatformsList.CheckedItems)
                {
                    AddingGame.Platforms.Add(Program.context.Platforms.Find(p.ID_Platform));
                }
                AddingGame.Online_protections = new List<Online_protections>();
                foreach (Online_protections p in OnlineProtectionsList.CheckedItems)
                {
                    AddingGame.Online_protections.Add(Program.context.Online_protections.Find(p.ID_Protect));
                }
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
            Program.context.Games.Add(AddingGame);
            Program.context.SaveChanges();
            var ID_G = Program.context.Games.Max(p => p.ID_Game);
            var game = Program.context.Games.Find(ID_G);
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
            this.Cursor = Cursors.Default;
            Compl = true;
            DialogResult = DialogResult.OK;
        }

        private void posterPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (ChooseImage.ShowDialog() == DialogResult.OK)
            {
                posterPictureBox.Image = Image.FromFile(ChooseImage.FileName);
                label13.Visible = false;
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
            this.Cursor = Cursors.WaitCursor;
            if (!OriginalNameEn.Checked)
            {
                AddingGame.Original_Name = null;
            }
            if (posterPictureBox.Image != null)
            {
                var stream = new MemoryStream();
                posterPictureBox.Image.Save(stream, posterPictureBox.Image.RawFormat);
                AddingGame.Poster = stream.GetBuffer();
            }
            else
            {
                AddingGame.Poster = null;
            }
            AddingGame.Localisation_Type = comboBox4.SelectedIndex;
            AddingGame.Status_complite = comboBox5.SelectedIndex;

            if ((bool)AddingGame.TypeContent)
            {
                if (numericUpDown1.Value == 0)
                {
                    AddingGame.Kol_updates = 0;
                }
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
                foreach (Platforms p in PlfLst)
                {
                    AddingGame.Platforms.Add(Program.context.Platforms.Find(p.ID_Platform));
                }
                foreach (Platforms p in glp)
                {
                    AddingGame.Platforms.Remove(Program.context.Platforms.Find(p.ID_Platform));
                }
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
                foreach (Online_protections p in OprLst)
                {
                    AddingGame.Online_protections.Add(Program.context.Online_protections.Find(p.ID_Protect));
                }
                foreach (Online_protections p in glop)
                {
                    AddingGame.Online_protections.Remove(Program.context.Online_protections.Find(p.ID_Protect));
                }
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
				this.Cursor = Cursors.Default;
            }
            var ID_G = AddingGame.ID_Game;
            var game = Program.context.Games.Find(ID_G);
			if (game.Games1 != null)
			{
				foreach (Games g in game.Games1)
				{
					if (!(bool)g.TypeContent)
					{
						g.Kol_updates = game.Kol_updates;
						g.Last_version = game.Last_version;

						Online_protections[] gopp = new Online_protections[OnlineProtectionsList.CheckedItems.Count];
						OnlineProtectionsList.CheckedItems.CopyTo(gopp, 0);
						List<Online_protections> glop = g.Online_protections.ToList();
						Online_protections[]  OprArr = new Online_protections[OnlineProtectionsList.CheckedItems.Count];
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
						foreach (Online_protections p in OprLst)
						{
							g.Online_protections.Add(Program.context.Online_protections.Find(p.ID_Protect));
						}
						foreach (Online_protections p in glop)
						{
							g.Online_protections.Remove(Program.context.Online_protections.Find(p.ID_Protect));
						}

						Platforms[] gpp = new Platforms[PlatformsList.CheckedItems.Count];
						PlatformsList.CheckedItems.CopyTo(gpp, 0);
						List<Platforms> glp = g.Platforms.ToList();
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
						foreach (Platforms p in PlfLst)
						{
							g.Platforms.Add(Program.context.Platforms.Find(p.ID_Platform));
						}
						foreach (Platforms p in glp)
						{
							g.Platforms.Remove(Program.context.Platforms.Find(p.ID_Platform));
						}
						Program.context.SaveChanges();
					}
				}
			}
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
            this.Cursor = Cursors.Default;
            Compl = true;
            DialogResult = DialogResult.OK;
        }

        private void убратьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            posterPictureBox.Image = null;
            label13.Visible = true;
        }

        private void StandAlone_CheckedChanged(object sender, EventArgs e)
        {
            changeTypeContent(StandAlone.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form ManageD = new ManagePDL(1);
            ManageD.ShowDialog();
            List<Developers> LD = new List<Developers>();
            LD.Add(new Developers { Name = "<отсутствует>", ID_Developer = 0 });
            LD.AddRange(Program.context.Developers.OrderBy(d => d.Name).ToArray());
            comboBox1.DataSource = LD;
            if (((ManagePDL)ManageD).publishersBindingSource.Current != null)
            ((Games)gamesBindingSource.Current).ID_Developer = ((Developers)((ManagePDL)ManageD).publishersBindingSource.Current).ID_Developer;
            comboBox1.SelectedValue = ((Games)gamesBindingSource.Current).ID_Developer;
            ManageD.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form ManageP = new ManagePDL(0);
            ManageP.ShowDialog();
            List<Publishers> LP = new List<Publishers>();
            LP.Add(new Publishers { Name = "<отсутствует>", Id_Publisher = 0 });
            LP.AddRange(Program.context.Publishers.OrderBy(p => p.Name).ToArray());
            comboBox2.DataSource = LP;
            if (((ManagePDL)ManageP).publishersBindingSource.Current != null)
            ((Games)gamesBindingSource.Current).ID_Publisher = ((Publishers)((ManagePDL)ManageP).publishersBindingSource.Current).Id_Publisher;
            comboBox2.SelectedValue = ((Games)gamesBindingSource.Current).ID_Publisher;
            ManageP.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form ManageRF = new ManagePDL(2);
            ManageRF.ShowDialog();
            List<RF_Distributors> LRF = new List<RF_Distributors>();
            LRF.Add(new RF_Distributors { Name = "<отсутствует>", ID_RF_Distributor = 0 });
            LRF.AddRange(Program.context.RF_Distributors.OrderBy(r => r.Name).ToArray());
            comboBox3.DataSource = LRF;
            if (((ManagePDL)ManageRF).publishersBindingSource.Current != null)
            ((Games)gamesBindingSource.Current).ID_RF_Distributor = ((RF_Distributors)((ManagePDL)ManageRF).publishersBindingSource.Current).ID_RF_Distributor;
            comboBox3.SelectedValue = ((Games)gamesBindingSource.Current).ID_RF_Distributor;
            ManageRF.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form ManageE = new ManageGEB(1);
            ManageE.ShowDialog();
            List<Editions> LE = new List<Editions>();
            LE.AddRange(Program.context.Editions.ToArray());
            comboBox6.DataSource = LE;
            if (((ManageGEB)ManageE).genresBindingSource.Current != null)
            ((Games)gamesBindingSource.Current).ID_Edition = ((Editions)((ManageGEB)ManageE).genresBindingSource.Current).ID_Edition;
            comboBox6.SelectedValue = ((Games)gamesBindingSource.Current).ID_Edition;
            ManageE.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form ManageB = new ManageGEB(2);
            ManageB.ShowDialog();
            //comboBox7.DataSource = Program.context.Boxes.Local.ToBindingList();
            //Program.context.Boxes.Load();
            List<Boxes> LB = new List<Boxes>();
            LB.AddRange(Program.context.Boxes.ToArray());
            comboBox7.DataSource = LB;
            if (((ManageGEB)ManageB).genresBindingSource.Current != null)
            ((Games)gamesBindingSource.Current).ID_Box = ((Boxes)((ManageGEB)ManageB).genresBindingSource.Current).ID_Box;
            comboBox7.SelectedValue = ((Games)gamesBindingSource.Current).ID_Box;
            ManageB.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form ManageP = new ManageOPOS(1);
            ManageP.ShowDialog();
            PlatformsList.DataSource = platformsBindingSource;
            PlatformsList.DisplayMember = "Name";
            PlatformsList.ValueMember = "ID_Platform";
            PlatformsList.DataSource = Program.context.Platforms.Local.ToBindingList();
            Program.context.Platforms.Load();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form ManageP = new ManageOPOS(0);
            ManageP.ShowDialog();
            OnlineProtectionsList.DataSource = online_protectionsBindingSource;
            OnlineProtectionsList.DisplayMember = "Name";
            OnlineProtectionsList.ValueMember = "ID_Protect";
            OnlineProtectionsList.DataSource = Program.context.Online_protections.Local.ToBindingList();
            Program.context.Platforms.Load();
        }

		private void comboBox1_Leave(object sender, EventArgs e)
		{
			if (comboBox1.SelectedValue == null)
			{
				/*MessageBox.Show("Недопустимый разработчик!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBox1.Focus();*/
				comboBox1.SelectedIndex = 0;
				((Games)gamesBindingSource.Current).ID_Developer = 0;
			}
		}

		private void comboBox2_Leave(object sender, EventArgs e)
		{
			if (comboBox2.SelectedValue == null)
			{
				/*MessageBox.Show("Недопустимый издатель!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBox2.Focus();*/
				comboBox2.SelectedIndex = 0;
				((Games)gamesBindingSource.Current).ID_Publisher = 0;
			}
		}

		private void comboBox3_Leave(object sender, EventArgs e)
		{
			if (comboBox3.SelectedValue == null)
			{
				/*MessageBox.Show("Недопустимый региональный издатель!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBox3.Focus();*/
				comboBox3.SelectedIndex = 0;
				((Games)gamesBindingSource.Current).ID_RF_Distributor = 0;
			}
		}

        private void button8_Click(object sender, EventArgs e)
        {
            Form ManageD = new ManageDiskTypes();
            ManageD.ShowDialog();
            List<Disk_types> LDT = new List<Disk_types>();
            LDT.AddRange(Program.context.Disk_types.ToArray());
            DiskTypes.DataSource = LDT;
        }

        private void numericUpDown1_MouseDown(object sender, MouseEventArgs e)
        {
            numericUpDown1.Select(0, 1);
        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            numericUpDown1.Select(0, 1);
        }

        private void PersonRate_MouseDown(object sender, MouseEventArgs e)
        {
            PersonRate.Select(0, 3);
        }

        private void PersonRate_Enter(object sender, EventArgs e)
        {
            PersonRate.Select(0, 3);
        }

        private void numericUpDown2_MouseDown(object sender, MouseEventArgs e)
        {
            numericUpDown2.Select(0, 3);
        }

        private void numericUpDown2_Enter(object sender, EventArgs e)
        {
            numericUpDown2.Select(0, 3);
        }

        private void ColDisks_MouseDown(object sender, MouseEventArgs e)
        {
            ColDisks.Select(0, 5);
        }

        private void ColDisks_Enter(object sender, EventArgs e)
        {
            ColDisks.Select(0, 5);
        }
    }

}
