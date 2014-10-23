using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using System.IO;
using GamesBase;
using GamesList.Properties;

namespace GamesList
{
	public partial class AddCollect : Form
	{
		private readonly GamesEntities _context = Program.context;

		private Games AddingGame;
		private Boolean Compl;

		public AddCollect(Games game)
		{
			InitializeComponent();
			gamesBindingSource1.DataSource = _context.Games.ToList().OrderBy(g => g.GetFullName());

			label3.Text = Settings.Default.DistrReg;

			PlatformsList.DataSource = platformsBindingSource;
			PlatformsList.DisplayMember = "Name";
			PlatformsList.ValueMember = "ID_Platform";
			PlatformsList.DataSource = _context.Platforms.ToList();

			OnlineProtectionsList.DataSource = online_protectionsBindingSource;
			OnlineProtectionsList.DisplayMember = "Name";
			OnlineProtectionsList.ValueMember = "ID_Protect";
			OnlineProtectionsList.DataSource = _context.Online_protections.ToList();

			DiskTypes.DataSource = _context.Disk_types.ToList();

			var LP = new List<Publishers> {new Publishers {Name = "<отсутствует>", Id_Publisher = 0}};
			LP.AddRange(_context.Publishers.OrderBy(p => p.Name).ToArray());
			comboBox2.DataSource = LP;

			var LRF = new List<RF_Distributors> {new RF_Distributors {Name = "<отсутствует>", ID_RF_Distributor = 0}};
			LRF.AddRange(_context.RF_Distributors.OrderBy(r => r.Name).ToArray());
			comboBox3.DataSource = LRF;

			var le = new List<Editions> { new Editions { Name = "<отсутствует>", ID_Edition = 0 } };
			le.AddRange(_context.Editions.ToArray());
			comboBox6.DataSource = le;

			var lb = new List<Boxes> { new Boxes { Name = "<отсутствует>", ID_Box = 0 } };
			lb.AddRange(_context.Boxes.ToArray());
			comboBox7.DataSource = lb;

			if (game == null)
			{
				AddingGame = _context.Games.Create();
				AddingGame.ID_Game = (_context.Games.Any() ? _context.Games.Max(p => p.ID_Game) : 0) + 1;
				AddingGame.Game_disks = new Collection<Game_disks>();
				AddingGame.Game_Type = false;
				AddingGame.ID_Publisher = 0;
				AddingGame.ID_RF_Distributor = 0;
				AddingGame.ID_Box = 0;
				AddingGame.ID_Edition = 0;
				AddBut.Visible = true;
				EditButton.Visible = false;
				AcceptButton = AddBut;
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
					AddingGame.ID_Publisher = 0;

				if (AddingGame.ID_RF_Distributor == null)
					AddingGame.ID_RF_Distributor = 0;


				for (var i = 0; i < PlatformsList.Items.Count; i++)
					if (AddingGame.Platforms.Any(p => p.ID_Platform == ((Platforms) PlatformsList.Items[i]).ID_Platform))
						PlatformsList.SetItemChecked(i, true);

				for (var i = 0; i < OnlineProtectionsList.Items.Count; i++)
					if (AddingGame.Online_protections.Any(p => p.ID_Protect == ((Online_protections) OnlineProtectionsList.Items[i]).ID_Protect))
						OnlineProtectionsList.SetItemChecked(i, true);

				listBox1.Items.AddRange(AddingGame.GamesInCollect.ToArray());

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
				AcceptButton = EditButton;
			}
			GameDiskList.DataSource = AddingGame.Game_disks.ToList();
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
				e.Value = string.Format("{0} {1}", ((Game_disks)e.Value).Kol_vo, ((Game_disks)e.Value).Disk_types.Name);
		}

		private void AddDisk_Click(object sender, EventArgs e)
		{
			if (ColDisks.Value == 0) { return; }

			var j = AddingGame.Game_disks.FirstOrDefault(gd => gd.ID_Disk_Type == (decimal)DiskTypes.SelectedValue);
			if (j != null)
			{
				j.Kol_vo = Decimal.ToDouble(ColDisks.Value);
			}
			else
			{
				AddingGame.Game_disks.Add(new Game_disks
				{
					ID_Game_disk = (_context.Game_disks.Any() ? _context.Game_disks.Max(pp => pp.ID_Game_disk) : 0) + 1,
					ID_Disk_Type = (decimal)DiskTypes.SelectedValue,
					Disk_types = _context.Disk_types.Find(DiskTypes.SelectedValue),
					Kol_vo = Decimal.ToDouble(ColDisks.Value),
					ID_Game = AddingGame.ID_Game
				});
			}
			GameDiskList.DataSource = AddingGame.Game_disks.ToList();
		}

		private void RemoveDisk_Click(object sender, EventArgs e)
		{
			AddingGame.Game_disks.Remove((Game_disks)GameDiskList.SelectedItem);
			GameDiskList.DataSource = AddingGame.Game_disks.ToList();

		}

		private void GameDiskList_SelectedIndexChanged(object sender, EventArgs e)
		{
			// MessageBox.Show(((Game_disks)GameDiskList.SelectedItem).Kol_vo.ToString(), "111");
		}

		private void AddGame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if ((!string.IsNullOrEmpty(nameTextBox.Text)) && (!Compl))
			{
				var SV = new SaveNotSaveDialog();
				switch (SV.ShowDialog())
				{
					case DialogResult.OK:
						if (AddingGame.ID_Game > 0)
							EditButton.PerformClick();
						else
							AddBut.PerformClick();
						break;
					case DialogResult.No:
						_context.RejectChanges(AddingGame);
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
			else
			{
				_context.RejectChanges(AddingGame);
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

			Cursor = Cursors.WaitCursor;
			AddingGame.Name = AddingGame.Name.Trim();
			AddingGame.Original_Name = !OriginalNameEn.Checked ? null : AddingGame.Original_Name.Trim();

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

			AddingGame.Platforms = new List<Platforms>();
			foreach (Platforms p in PlatformsList.CheckedItems)
				AddingGame.Platforms.Add(p);

			AddingGame.Online_protections = new List<Online_protections>();
			foreach (Online_protections p in OnlineProtectionsList.CheckedItems)
				AddingGame.Online_protections.Add(p);

			if (AddingGame.ID_Publisher == 0)
				AddingGame.ID_Publisher = null;

			if (AddingGame.ID_RF_Distributor == 0)
				AddingGame.ID_RF_Distributor = null;

			AddingGame.GamesInCollect = new List<Games>();
			foreach (Games g in listBox1.Items)
				AddingGame.GamesInCollect.Add(g);

			_context.Games.Add(AddingGame);
			try
			{
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Произошла ошибка:\n{0}\n{1}", ex.Message, (ex.InnerException != null ? ex.InnerException.Message : ""))
					   , "Ошибка"
					   , MessageBoxButtons.OK
					   , MessageBoxIcon.Error);
				_context.Games.Remove(AddingGame);
				Cursor = Cursors.Default;
				return;
			}
			Cursor = Cursors.Default;
			Compl = true;
			DialogResult = DialogResult.OK;
		}

		private void posterPictureBox_DoubleClick(object sender, EventArgs e)
		{
			if (ChooseImage.ShowDialog() != DialogResult.OK) return;
			posterPictureBox.Image = Image.FromFile(ChooseImage.FileName);
			label13.Visible = false;
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

			Cursor = Cursors.WaitCursor;
			AddingGame.Name = AddingGame.Name.Trim();
			AddingGame.Original_Name = !OriginalNameEn.Checked ? null : AddingGame.Original_Name.Trim();

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
			var toAdd = PlatformsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform)
				.Except(AddingGame.Platforms.Select(p => p.ID_Platform));
			var toRemove = AddingGame.Platforms.Select(p => p.ID_Platform)
				.Except(PlatformsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform));
			foreach (var p in toAdd)
				AddingGame.Platforms.Add(_context.Platforms.Find(p));
			foreach (var p in toRemove)
				AddingGame.Platforms.Remove(_context.Platforms.Find(p));

			//Обновление игр
			toAdd = listBox1.Items.Cast<Games>().Select(p => p.ID_Game)
				.Except(AddingGame.GamesInCollect.Select(p => p.ID_Game));
			toRemove = AddingGame.GamesInCollect.Select(p => p.ID_Game)
				.Except(listBox1.Items.Cast<Games>().Select(p => p.ID_Game));
			foreach (var g in toAdd)
				AddingGame.GamesInCollect.Add(_context.Games.Find(g));
			foreach (var g in toRemove)
				AddingGame.GamesInCollect.Remove(_context.Games.Find(g));

			//Обновление защит
			toAdd = OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect)
				.Except(AddingGame.Online_protections.Select(p => p.ID_Protect));
			toRemove = AddingGame.Online_protections.Select(p => p.ID_Protect)
				.Except(OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect));
			foreach (var op in toAdd)
				AddingGame.Online_protections.Add(_context.Online_protections.Find(op));
			foreach (var op in toRemove)
				AddingGame.Online_protections.Remove(_context.Online_protections.Find(op));

			if (AddingGame.ID_Publisher == 0)
				AddingGame.ID_Publisher = null;

			if (AddingGame.ID_RF_Distributor == 0)
				AddingGame.ID_RF_Distributor = null;

			AddingGame.Localisation_Type = -1;
			try
			{
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Произошла ошибка:\n{0}\n{1}", ex.Message, (ex.InnerException != null ? ex.InnerException.Message : ""))
					   , "Ошибка"
					   , MessageBoxButtons.OK
					   , MessageBoxIcon.Error);
				Cursor = Cursors.Default;
				return;
			}
			Cursor = Cursors.Default;
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
			e.Value = ((Games) e.ListItem).GetFullName();
		}

		private void AddGame_Click(object sender, EventArgs e)
		{
			if (comboBox1.SelectedValue != null)
			{
				var g = _context.Games.Find(comboBox1.SelectedValue);
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
				listBox1.Items.Remove(listBox1.SelectedItem);
		}

		private void comboBox1_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = ((Games)e.ListItem).GetFullName();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var manageP = new ManagePDL(0);
			manageP.ShowDialog();
			var LP = new List<Publishers> {new Publishers {Name = "<отсутствует>", Id_Publisher = 0}};
			LP.AddRange(_context.Publishers.OrderBy(p => p.Name).ToArray());
			comboBox2.DataSource = LP;
			if (manageP.publishersBindingSource.Current != null)
				((Games)gamesBindingSource.Current).ID_Publisher = ((Publishers)manageP.publishersBindingSource.Current).Id_Publisher;
			comboBox2.SelectedValue = ((Games)gamesBindingSource.Current).ID_Publisher;
			manageP.Dispose();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var manageRf = new ManagePDL(2);
			manageRf.ShowDialog();
			var LRF = new List<RF_Distributors> {new RF_Distributors {Name = "<отсутствует>", ID_RF_Distributor = 0}};
			LRF.AddRange(_context.RF_Distributors.OrderBy(r => r.Name).ToArray());
			comboBox3.DataSource = LRF;
			if (manageRf.publishersBindingSource.Current != null)
				((Games)gamesBindingSource.Current).ID_RF_Distributor = ((RF_Distributors)manageRf.publishersBindingSource.Current).ID_RF_Distributor;
			comboBox3.SelectedValue = ((Games)gamesBindingSource.Current).ID_RF_Distributor;
			manageRf.Dispose();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			var manageE = new ManageGEB(1);
			manageE.ShowDialog();
			var LE = new List<Editions> { new Editions { ID_Edition = 0, Name = "<отсутствует>" } };
			LE.AddRange(_context.Editions.ToArray());
			comboBox6.DataSource = LE;
			if (manageE.genresBindingSource.Current != null)
				((Games)gamesBindingSource.Current).ID_Edition = ((Editions)manageE.genresBindingSource.Current).ID_Edition;
			comboBox6.SelectedValue = ((Games)gamesBindingSource.Current).ID_Edition;
			manageE.Dispose();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			var manageB = new ManageGEB(2);
			manageB.ShowDialog();
			var LB = new List<Boxes> { new Boxes { ID_Box = 0, Name = "<отсутствует>" } };
			LB.AddRange(_context.Boxes.ToArray());
			comboBox7.DataSource = LB;
			if (manageB.genresBindingSource.Current != null)
				((Games)gamesBindingSource.Current).ID_Box = ((Boxes)manageB.genresBindingSource.Current).ID_Box;
			comboBox7.SelectedValue = ((Games)gamesBindingSource.Current).ID_Box;
			manageB.Dispose();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			var manageP = new ManageOPOS(1);
			manageP.ShowDialog();
			PlatformsList.DataSource = platformsBindingSource;
			PlatformsList.DisplayMember = "Name";
			PlatformsList.ValueMember = "ID_Platform";
			PlatformsList.DataSource = _context.Platforms.ToList();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			var manageP = new ManageOPOS(0);
			manageP.ShowDialog();
			OnlineProtectionsList.DataSource = online_protectionsBindingSource;
			OnlineProtectionsList.DisplayMember = "Name";
			OnlineProtectionsList.ValueMember = "ID_Protect";
			OnlineProtectionsList.DataSource = _context.Online_protections.ToList();
		}

		private void comboBox2_Leave(object sender, EventArgs e)
		{
			if (comboBox2.SelectedValue != null) return;
			/*MessageBox.Show("Недопустимый издатель!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBox2.Focus();*/
			comboBox2.SelectedIndex = 0;
			((Games)gamesBindingSource.Current).ID_Publisher = 0;
		}

		private void comboBox3_Leave(object sender, EventArgs e)
		{
			if (comboBox3.SelectedValue != null) return;
			/*MessageBox.Show("Недопустимый региональный издатель!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBox3.Focus();*/
			comboBox3.SelectedIndex = 0;
			((Games)gamesBindingSource.Current).ID_RF_Distributor = 0;
		}

		private void button7_Click(object sender, EventArgs e)
		{
			var manageD = new ManageDiskTypes();
			manageD.ShowDialog();
			var LDT = new List<Disk_types>();
			LDT.AddRange(_context.Disk_types.ToArray());
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
