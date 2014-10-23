using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GamesBase;
using System.Data.Entity;
using System.IO;
using GamesList.Properties;

namespace GamesList
{
	public partial class AddContent : Form
	{
		private readonly GamesEntities _context = Program.context;

		private Games AddingGame;
		private Boolean Compl = false;

		public AddContent(Games game, byte ctc)
		{
			InitializeComponent();
			rate_IgromaniaLabel.Text = string.Format("Рейтинг {0}:", Settings.Default.Recenzor);
			label3.Text = Settings.Default.DistrReg;
			PersonRate.Maximum = Settings.Default.MaxYourRating;
			numericUpDown2.Maximum = Settings.Default.MaxRecenzorRating;

			PlatformsList.DataSource = platformsBindingSource;
			PlatformsList.DisplayMember = "Name";
			PlatformsList.ValueMember = "ID_Platform";
			PlatformsList.DataSource = _context.Platforms.ToList();

			OnlineProtectionsList.DataSource = online_protectionsBindingSource;
			OnlineProtectionsList.DisplayMember = "Name";
			OnlineProtectionsList.ValueMember = "ID_Protect";
			OnlineProtectionsList.DataSource = _context.Online_protections.ToList();


			DiskTypes.DataSource = _context.Disk_types.ToList();

			var lp = new List<Publishers> {new Publishers {Name = "<отсутствует>", Id_Publisher = 0}};
			lp.AddRange(_context.Publishers.OrderBy(p => p.Name).ToArray());
			comboBox2.DataSource = lp;

			var ld = new List<Developers> {new Developers {Name = "<отсутствует>", ID_Developer = 0}};
			ld.AddRange(_context.Developers.OrderBy(d => d.Name).ToArray());
			comboBox1.DataSource = ld;

			var lrf = new List<RF_Distributors> {new RF_Distributors {Name = "<отсутствует>", ID_RF_Distributor = 0}};
			lrf.AddRange(_context.RF_Distributors.OrderBy(r => r.Name).ToArray());
			comboBox3.DataSource = lrf;

			var le = new List<Editions> { new Editions { Name = "<отсутствует>", ID_Edition = 0 } };
			le.AddRange(_context.Editions.ToArray());
			comboBox6.DataSource = le;

			var lb = new List<Boxes> { new Boxes { Name = "<отсутствует>", ID_Box = 0 } };
			lb.AddRange(_context.Boxes.ToArray());
			comboBox7.DataSource = lb;

			if ((game.ID_Content == null) || (ctc == 1))
			{
				AddingGame = _context.Games.Create();
				AddingGame.ID_Game = (_context.Games.Any() ? _context.Games.Max(p => p.ID_Game) : 0) + 1;
				AddingGame.Game_Type = false;
				AddingGame.TypeContent = false;
				AddingGame.ID_Developer = game.ID_Developer ?? 0;

				AddingGame.ID_Publisher = game.ID_Publisher ?? 0;

				AddingGame.ID_RF_Distributor = game.ID_RF_Distributor ?? 0;

				AddingGame.ID_Box = 0;
				AddingGame.ID_Edition = 0;
				AddingGame.Localisation_Type = game.Localisation_Type;
				comboBox4.SelectedIndex = Decimal.ToInt32(AddingGame.Localisation_Type ?? 0);
				comboBox5.SelectedIndex = 0;
				AddBut.Visible = true;
				EditButton.Visible = false;
				AddingGame.Series = game.Series;
				AddingGame.Genres = game.Genres;

				AddingGame.ID_Content = game.ID_Game;
				AddingGame.OriginalGame = game;
				AddingGame.Game_disks = new Collection<Game_disks>();
				ChangeTypeContent(false);
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

				if (AddingGame.ID_Developer == null)
					AddingGame.ID_Developer = 0;

				if (AddingGame.ID_Publisher == null)
					AddingGame.ID_Publisher = 0;

				if (AddingGame.ID_RF_Distributor == null)
					AddingGame.ID_RF_Distributor = 0;

				comboBox4.SelectedIndex = Decimal.ToInt32(AddingGame.Localisation_Type ?? 0);
				comboBox5.SelectedIndex = Decimal.ToInt32(AddingGame.Status_complite);

				for (var i = 0; i < PlatformsList.Items.Count; i++)
					if (AddingGame.Platforms.Any(p => p.ID_Platform == ((Platforms) PlatformsList.Items[i]).ID_Platform))
						PlatformsList.SetItemChecked(i, true);

				for (var i = 0; i < OnlineProtectionsList.Items.Count; i++)
					if (AddingGame.Online_protections.Any(p => p.ID_Protect == ((Online_protections) OnlineProtectionsList.Items[i]).ID_Protect))
						OnlineProtectionsList.SetItemChecked(i, true);

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

				ChangeTypeContent(AddingGame.TypeContent ?? false);
				button1.Enabled = false;
				button2.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
				button5.Enabled = false;
				button8.Enabled = false;
				AcceptButton = EditButton;
			}

			SerName.Text = AddingGame.Series != null ? AddingGame.Series.Name : "<отсутствует>";

			GenLst.Text = String.Join("\n", AddingGame.Genres.Select(g => g.Name));
			GameDiskList.DataSource = AddingGame.Game_disks.ToList();
			ParentGameName.Text = AddingGame.OriginalGame.GetFullName().Contains(":")
				? string.Format("{0} -", AddingGame.OriginalGame.GetFullName())
				: string.Format("{0}:", AddingGame.OriginalGame.GetFullName());
			if (AddingGame.OriginalGame.OriginalGame != null)
			{
				StandAlone.Checked = false;
				StandAlone.Enabled = false;
			}
			nameTextBox.Left = ParentGameName.Right + 5;
			rectangleShape1.Left = ParentGameName.Right + 3;

			gamesBindingSource.DataSource = new[] { AddingGame };
		}

		private void ChangeTypeContent(bool st)
		{
			if (st)
			{
				last_versionTextBox.Enabled = true;
				numericUpDown1.Enabled = true;
				OnlineProtectionsList.Enabled = true;
				PlatformsList.Enabled = true;
				if (AddingGame.ID_Game >= 1) return;
				button6.Enabled = true;
				button7.Enabled = true;
			}
			else
			{
				AddingGame.Last_version = AddingGame.OriginalGame.Last_version;
				last_versionTextBox.Enabled = false;
				AddingGame.Kol_updates = AddingGame.OriginalGame.Kol_updates;
				numericUpDown1.Enabled = false;

				AddingGame.Online_protections = AddingGame.OriginalGame.Online_protections;
				for (var i = 0; i < OnlineProtectionsList.Items.Count; i++)
					if (AddingGame.Online_protections.Any(p => p.ID_Protect == ((Online_protections) OnlineProtectionsList.Items[i]).ID_Protect))
						OnlineProtectionsList.SetItemChecked(i, true);
				OnlineProtectionsList.Enabled = false;

				AddingGame.Platforms = AddingGame.OriginalGame.Platforms;
				for (var i = 0; i < PlatformsList.Items.Count; i++)
					if (AddingGame.Platforms.Any(p => p.ID_Platform == ((Platforms) PlatformsList.Items[i]).ID_Platform))
						PlatformsList.SetItemChecked(i, true);
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
				MessageBox.Show("Введите хотя бы имя игры!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			AddingGame.Localisation_Type = comboBox4.SelectedIndex;
			AddingGame.Status_complite = comboBox5.SelectedIndex;

			if (AddingGame.TypeContent ?? false)
			{
				if (numericUpDown1.Value == 0)
				{
					AddingGame.Kol_updates = 0;
				}
				AddingGame.Platforms = new List<Platforms>();
				foreach (Platforms p in PlatformsList.CheckedItems)
					AddingGame.Platforms.Add(_context.Platforms.Find(p.ID_Platform));

				AddingGame.Online_protections = new List<Online_protections>();
				foreach (Online_protections p in OnlineProtectionsList.CheckedItems)
					AddingGame.Online_protections.Add(_context.Online_protections.Find(p.ID_Protect));
			}

			if (AddingGame.ID_Developer == 0)
				AddingGame.ID_Developer = null;

			if (AddingGame.ID_Publisher == 0)
				AddingGame.ID_Publisher = null;

			if (AddingGame.ID_RF_Distributor == 0)
				AddingGame.ID_RF_Distributor = null;

			if (AddingGame.ID_Ser == 0)
				AddingGame.ID_Ser = null;

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
				Cursor = Cursors.Default;
				_context.Games.Remove(AddingGame);
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
				MessageBox.Show("Введите хотя бы имя игры!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			AddingGame.Localisation_Type = comboBox4.SelectedIndex;
			AddingGame.Status_complite = comboBox5.SelectedIndex;

			if (AddingGame.TypeContent ?? false)
			{
				if (numericUpDown1.Value == 0)
				{
					AddingGame.Kol_updates = 0;
				}
				//Обновление платформ
				var toAdd = PlatformsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform)
				.Except(AddingGame.Platforms.Select(p => p.ID_Platform));
				var toRemove = AddingGame.Platforms.ToList().Select(p => p.ID_Platform)
					.Except(PlatformsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform));
				foreach (var p in toAdd)
					AddingGame.Platforms.Add(_context.Platforms.Find(p));
				foreach (var p in toRemove)
					AddingGame.Platforms.Remove(_context.Platforms.Find(p));

				//Обновление защит
				toAdd = OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect)
				.Except(AddingGame.Online_protections.Select(p => p.ID_Protect));
				toRemove = AddingGame.Online_protections.ToList().Select(p => p.ID_Protect)
					.Except(OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect));
				foreach (var op in toAdd)
					AddingGame.Online_protections.Add(_context.Online_protections.Find(op));
				foreach (var op in toRemove)
					AddingGame.Online_protections.Remove(_context.Online_protections.Find(op));
			}

			if (AddingGame.ID_Developer == 0)
				AddingGame.ID_Developer = null;

			if (AddingGame.ID_Publisher == 0)
				AddingGame.ID_Publisher = null;

			if (AddingGame.ID_RF_Distributor == 0)
				AddingGame.ID_RF_Distributor = null;

			if (AddingGame.ID_Ser == 0)
				AddingGame.ID_Ser = null;

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
			/*var ID_G = AddingGame.ID_Game;
			var game = _context.Games.Find(ID_G);*/

			foreach (var g in AddingGame.Content)
			{
				g.Kol_updates = AddingGame.Kol_updates;
				g.Last_version = AddingGame.Last_version;

				var toAdd = OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect)
				.Except(g.Online_protections.Select(p => p.ID_Protect));
				var toRemove = g.Online_protections.AsQueryable().Select(p => p.ID_Protect)
					.Except(OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect));
				foreach (var op in toAdd)
					g.Online_protections.Add(_context.Online_protections.Find(op));
				foreach (var op in toRemove)
					g.Online_protections.Remove(_context.Online_protections.Find(op));

				toAdd = OnlineProtectionsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform)
					.Except(g.Platforms.Select(p => p.ID_Platform));
				toRemove = g.Platforms.AsQueryable().Select(p => p.ID_Platform)
					.Except(OnlineProtectionsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform));
				foreach (var p in toAdd)
					g.Platforms.Add(_context.Platforms.Find(p));
				foreach (var p in toRemove)
					g.Platforms.Remove(_context.Platforms.Find(p));
				_context.SaveChanges();
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

		private void StandAlone_CheckedChanged(object sender, EventArgs e)
		{
			ChangeTypeContent(StandAlone.Checked);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var manageD = new ManagePDL(1);
			manageD.ShowDialog();
			var LD = new List<Developers> {new Developers {Name = "<отсутствует>", ID_Developer = 0}};
			LD.AddRange(_context.Developers.OrderBy(d => d.Name).ToArray());
			comboBox1.DataSource = LD;
			if (manageD.publishersBindingSource.Current != null)
				((Games)gamesBindingSource.Current).ID_Developer = ((Developers)manageD.publishersBindingSource.Current).ID_Developer;
			comboBox1.SelectedValue = ((Games)gamesBindingSource.Current).ID_Developer;
			manageD.Dispose();
		}

		private void button2_Click(object sender, EventArgs e)
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

		private void button3_Click(object sender, EventArgs e)
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

		private void button4_Click(object sender, EventArgs e)
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

		private void button5_Click(object sender, EventArgs e)
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

		private void button6_Click(object sender, EventArgs e)
		{
			var manageP = new ManageOPOS(1);
			manageP.ShowDialog();
			PlatformsList.DataSource = platformsBindingSource;
			PlatformsList.DisplayMember = "Name";
			PlatformsList.ValueMember = "ID_Platform";
			PlatformsList.DataSource = _context.Platforms.Local.ToBindingList();
			_context.Platforms.Load();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			var manageP = new ManageOPOS(0);
			manageP.ShowDialog();
			OnlineProtectionsList.DataSource = online_protectionsBindingSource;
			OnlineProtectionsList.DisplayMember = "Name";
			OnlineProtectionsList.ValueMember = "ID_Protect";
			OnlineProtectionsList.DataSource = _context.Online_protections.Local.ToBindingList();
			_context.Platforms.Load();
		}

		private void comboBox1_Leave(object sender, EventArgs e)
		{
			if (comboBox1.SelectedValue != null) return;
			/*MessageBox.Show("Недопустимый разработчик!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBox1.Focus();*/
			comboBox1.SelectedIndex = 0;
			((Games)gamesBindingSource.Current).ID_Developer = 0;
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

		private void button8_Click(object sender, EventArgs e)
		{
			var manageD = new ManageDiskTypes();
			manageD.ShowDialog();
			var LDT = new List<Disk_types>();
			LDT.AddRange(_context.Disk_types.ToArray());
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
