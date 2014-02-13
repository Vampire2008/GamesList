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
    public partial class AddCollect : Form
    {
        private Games AddingGame;
        private Game_disks[] Disks;
        private Platforms[] GamePlatforms;
        private Online_protections[] OnlineProtections;
        private Games[] CollectedGames;
        private Boolean Compl = false;

        public AddCollect(Games game)
        {
            InitializeComponent();
            var Query = from Games in Program.context.Games
                        orderby Games.Games2.Games2 == null ?
                        Games.ID_Content == null ? Games.Name : (Games.Games2.Name + (Games.Games2.Name.IndexOf(":") > -1 ? " - " : ": ") + Games.Name)
                        : (Games.Games2.Games2.Name + (Games.Games2.Games2.Name.IndexOf(":") > -1 ? " - " : ": ") + Games.Games2.Name + " - " + Games.Name)
                        select Games;
            gamesBindingSource1.DataSource = Query.ToList();
            //gamesBindingSource1.DataSource = Program.context.Games.Local.ToBindingList();
            //Program.context.Games.Load();
            label3.Text = Properties.Settings.Default.DistrReg;
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
            DiskTypes.DataSource = Program.context.Disk_types.Local.ToBindingList();
            Program.context.Disk_types.Load();
            List<Publishers> LP = new List<Publishers>();
            LP.Add(new Publishers { Name = "<отсутствует>", Id_Publisher = 0 });
            LP.AddRange(Program.context.Publishers.OrderBy(p => p.Name).ToArray());
            comboBox2.DataSource = LP;

            List<RF_Distributors> LRF = new List<RF_Distributors>();
            LRF.Add(new RF_Distributors { Name = "<отсутствует>", ID_RF_Distributor = 0 });
            LRF.AddRange(Program.context.RF_Distributors.OrderBy(r=> r.Name).ToArray());
            comboBox3.DataSource = LRF;

            comboBox6.DataSource = Program.context.Editions.Local.ToBindingList();
            Program.context.Editions.Load();

            comboBox7.DataSource = Program.context.Boxes.Local.ToBindingList();
            Program.context.Boxes.Load();
            if (game == null)
            {
                AddingGame = Program.context.Games.Create();
                AddingGame.Game_Type = false;
                AddingGame.ID_Publisher = 0;
                AddingGame.ID_RF_Distributor = 0;
                AddingGame.ID_Box = 1;
                AddingGame.ID_Edition = 1;
                AddBut.Visible = true;
                EditButton.Visible = false;
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
                if (AddingGame.ID_Publisher == null)
                {
                    AddingGame.ID_Publisher = 0;
                }
                if (AddingGame.ID_RF_Distributor == null)
                {
                    AddingGame.ID_RF_Distributor = 0;
                }
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
                for (int i = 0; i < OnlineProtectionsList.Items.Count; i++)
                {
                    if (AddingGame.Online_protections.SingleOrDefault(p => p.ID_Protect == ((Online_protections)OnlineProtectionsList.Items[i]).ID_Protect) != null)
                    {
                        OnlineProtectionsList.SetItemChecked(i, true);
                    }
                }
                OnlineProtections = new Online_protections[OnlineProtectionsList.CheckedItems.Count];
                OnlineProtectionsList.CheckedItems.CopyTo(OnlineProtections, 0);
                listBox1.Items.AddRange(AddingGame.Games11.ToArray());
                CollectedGames = new Games[listBox1.Items.Count];
                listBox1.Items.CopyTo(CollectedGames, 0);
                if (AddingGame.Poster != null)
                {
                    var stream = new MemoryStream(AddingGame.Poster);
                    posterPictureBox.Image = Image.FromStream(stream);
                    label13.Visible = false;
                }
                else
                {
                    posterPictureBox.Image = null;
                }
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                this.AcceptButton = EditButton;
            }
            gamesBindingSource.DataSource = new[] { AddingGame };
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
                MessageBox.Show("Введите имя сборника!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (listBox1.Items.Count <= 0)
            {
                MessageBox.Show("Не имеет смысла добавлять сборник без игр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            AddingGame.Localisation_Type = -1;
            AddingGame.ID_Game = (Program.context.Games.Count() > 0 ? Program.context.Games.Max(p => p.ID_Game) : 0) + 1;
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
            if (AddingGame.ID_Publisher == 0)
            {
                AddingGame.ID_Publisher = null;
            }
            if (AddingGame.ID_RF_Distributor == 0)
            {
                AddingGame.ID_RF_Distributor = null;
            }
            AddingGame.Games11 = new List<Games>();
            foreach (Games g in listBox1.Items)
            {
                AddingGame.Games11.Add(Program.context.Games.Find(g.ID_Game));
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
                MessageBox.Show("Введите имя сборника!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (listBox1.Items.Count <= 0)
            {
                MessageBox.Show("Нельзя оставить сборник без игр.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //Обновление игр
            Games[] gg = new Games[listBox1.Items.Count];
            listBox1.Items.CopyTo(gg, 0);
            List<Games> glg = CollectedGames.ToList();
            Games[] GamArr = new Games[listBox1.Items.Count];
            listBox1.Items.CopyTo(GamArr,0);
            List<Games> GamLst = GamArr.ToList();
            foreach (Games g in gg)
            {
                if (GamLst.Contains(g) && glg.Contains(g))
                {
                    GamLst.Remove(g);
                    glg.Remove(g);
                }
            }
            foreach (Games g in GamLst)
            {
                AddingGame.Games11.Add(Program.context.Games.Find(g.ID_Game));
            }
            foreach (Games g in glg)
            {
                AddingGame.Games11.Remove(Program.context.Games.Find(g.ID_Game));
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
            if (AddingGame.ID_Publisher == 0)
            {
                AddingGame.ID_Publisher = null;
            }
            if (AddingGame.ID_RF_Distributor == 0)
            {
                AddingGame.ID_RF_Distributor = null;
            }
            AddingGame.Localisation_Type = -1;
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

        private void listBox1_Format_1(object sender, ListControlConvertEventArgs e)
        {
            if (((Games)e.ListItem).ID_Content != null)
            {
                if (((Games)e.ListItem).Games2.Name.IndexOf(":") > -1)
                {
                    e.Value = ((Games)e.ListItem).Games2.Name + " - " + ((Games)e.ListItem).Name;
                }
                else
                    e.Value = ((Games)e.ListItem).Games2.Name + ": " + ((Games)e.ListItem).Name;
            }
            else
            {
                e.Value = ((Games)e.Value).Name;
            }
        }

        private void AddGame_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                Games g = Program.context.Games.Find(comboBox1.SelectedValue);
                if ((g != null) && (!listBox1.Items.Contains(g)))
                    listBox1.Items.Add(g);
                comboBox1.Focus();
            }
            else
            {
                MessageBox.Show("Введена неверная игра!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveGame_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void comboBox1_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = Program.getName((Games)e.ListItem);
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            Form ManageE = new ManageGEB(1);
            ManageE.ShowDialog();
            //Program.context.Editions.Load();
            //comboBox6.DataSource = Program.context.Editions.Local.ToBindingList();
            List<Editions> LE = new List<Editions>();
            LE.AddRange(Program.context.Editions.ToArray());
            comboBox6.DataSource = LE;
            //Program.context.Editions.Load();
            if (((ManageGEB)ManageE).genresBindingSource.Current != null)
            ((Games)gamesBindingSource.Current).ID_Edition = ((Editions)((ManageGEB)ManageE).genresBindingSource.Current).ID_Edition;
            comboBox6.SelectedValue = ((Games)gamesBindingSource.Current).ID_Edition;
            ManageE.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            Form ManageP = new ManageOPOS(1);
            ManageP.ShowDialog();
            PlatformsList.DataSource = platformsBindingSource;
            PlatformsList.DisplayMember = "Name";
            PlatformsList.ValueMember = "ID_Platform";
            PlatformsList.DataSource = Program.context.Platforms.Local.ToBindingList();
            Program.context.Platforms.Load();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form ManageP = new ManageOPOS(0);
            ManageP.ShowDialog();
            OnlineProtectionsList.DataSource = online_protectionsBindingSource;
            OnlineProtectionsList.DisplayMember = "Name";
            OnlineProtectionsList.ValueMember = "ID_Protect";
            OnlineProtectionsList.DataSource = Program.context.Online_protections.Local.ToBindingList();
            Program.context.Platforms.Load();
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

        private void button7_Click(object sender, EventArgs e)
        {
            Form ManageD = new ManageDiskTypes();
            ManageD.ShowDialog();
            List<Disk_types> LDT = new List<Disk_types>();
            LDT.AddRange(Program.context.Disk_types.ToArray());
            DiskTypes.DataSource = LDT;
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
