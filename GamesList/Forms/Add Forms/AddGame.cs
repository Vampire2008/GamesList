using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using GamesBase;
using System.Data.Entity;
using System.IO;
using GamesList.Properties;

namespace GamesList
{
	public partial class AddGame : Form
	{
		private readonly GamesEntities _context = Program.context;

		private Games AddingGame;
		private Boolean Compl;

		public AddGame(Games game)
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

			GenresList.DataSource = genresBindingSource;
			GenresList.DisplayMember = "Name";
			GenresList.ValueMember = "ID_Genre";
			GenresList.DataSource = _context.Genres.ToList();

			DiskTypes.DataSource = _context.Disk_types.ToList();

			var LP = new List<Publishers> {new Publishers {Name = "<отсутствует>", Id_Publisher = 0}};
			LP.AddRange(_context.Publishers.OrderBy(p => p.Name).ToArray());
			comboBox2.DataSource = LP;

			var LD = new List<Developers> {new Developers {Name = "<отсутствует>", ID_Developer = 0}};
			LD.AddRange(_context.Developers.OrderBy(d => d.Name).ToArray());
			comboBox1.DataSource = LD;

			var LRF = new List<RF_Distributors> {new RF_Distributors {Name = "<отсутствует>", ID_RF_Distributor = 0}};
			LRF.AddRange(_context.RF_Distributors.OrderBy(r => r.Name).ToArray());
			comboBox3.DataSource = LRF;

			var LS = new List<Series> {new Series {Name = "<отсутствует>", ID_Ser = 0}};
			LS.AddRange(_context.Series.OrderBy(s => s.Name).ToArray());
			comboBox8.DataSource = LS;

			var le = new List<Editions> {new Editions {Name = "<отсутствует>", ID_Edition = 0}};
			le.AddRange(_context.Editions.ToArray());
			comboBox6.DataSource = le;

			var lb = new List<Boxes> {new Boxes {Name = "<отсутствует>", ID_Box = 0}};
			lb.AddRange(_context.Boxes.ToArray());
			comboBox7.DataSource = lb;

			if (game == null)
			{
				AddingGame = _context.Games.Create();
				AddingGame.Game_Type = false;
				AddingGame.ID_Game = (_context.Games.Any() ? _context.Games.Max(p => p.ID_Game) : 0) + 1;
				AddingGame.Game_disks = new Collection<Game_disks>();
				AddingGame.ID_Developer = 0;
				AddingGame.ID_Publisher = 0;
				AddingGame.ID_RF_Distributor = 0;
				AddingGame.ID_Box = 0;
				AddingGame.ID_Edition = 0;
				AddingGame.ID_Ser = 0;
				comboBox4.SelectedIndex = 0;
				comboBox5.SelectedIndex = 0;
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

				if (AddingGame.ID_Developer == null)
					AddingGame.ID_Developer = 0;

				if (AddingGame.ID_Publisher == null)
					AddingGame.ID_Publisher = 0;

				if (AddingGame.ID_RF_Distributor == null)
					AddingGame.ID_RF_Distributor = 0;

				if (AddingGame.ID_Ser == null)
					AddingGame.ID_Ser = 0;

				if (AddingGame.ID_Box == null)
					AddingGame.ID_Box = 0;

				if (AddingGame.ID_Edition == null)
					AddingGame.ID_Edition = 0;


				comboBox4.SelectedIndex = Decimal.ToInt32(AddingGame.Localisation_Type ?? 0);
				comboBox5.SelectedIndex = Decimal.ToInt32(AddingGame.Status_complite);


				for (var i = 0; i < PlatformsList.Items.Count; i++)
					if (AddingGame.Platforms.Any(p => p.ID_Platform == ((Platforms) PlatformsList.Items[i]).ID_Platform))
						PlatformsList.SetItemChecked(i, true);

				for (var i = 0; i < GenresList.Items.Count; i++)
					if (AddingGame.Genres.Any(p => p.ID_Genre == ((Genres) GenresList.Items[i]).ID_Genre))
						GenresList.SetItemChecked(i, true);

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
				button1.Enabled = false;
				button2.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
				button5.Enabled = false;
				button6.Enabled = false;
				button7.Enabled = false;
				button8.Enabled = false;
				button9.Enabled = false;
				button10.Enabled = false;
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

		private void GenresList_Format(object sender, ListControlConvertEventArgs e)
		{
			try
			{
				e.Value = ((Genres)e.Value).Name;
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

		private void DiskTypes_SelectedIndexChanged(object sender, EventArgs e)
		{

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
			if (!OriginalNameEn.Checked)
				AddingGame.Original_Name = null;

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

			//Game_platforms gp;
			AddingGame.Platforms = new List<Platforms>();
			foreach (Platforms p in PlatformsList.CheckedItems)
				AddingGame.Platforms.Add(p);

			AddingGame.Genres = new List<Genres>();
			//Game_Genres gg;
			foreach (Genres p in GenresList.CheckedItems)
				AddingGame.Genres.Add(p);

			//Game_protections gop;
			AddingGame.Online_protections = new List<Online_protections>();
			foreach (Online_protections p in OnlineProtectionsList.CheckedItems)
				AddingGame.Online_protections.Add(p);

			if (AddingGame.ID_Developer == 0)
				AddingGame.ID_Developer = null;

			if (AddingGame.ID_Publisher == 0)
				AddingGame.ID_Publisher = null;

			if (AddingGame.ID_RF_Distributor == 0)
				AddingGame.ID_RF_Distributor = null;

			if (AddingGame.ID_Ser == 0)
				AddingGame.ID_Ser = null;

			if (AddingGame.ID_Box == 0)
				AddingGame.ID_Box = null;

			if (AddingGame.ID_Edition == 0)
				AddingGame.ID_Edition = null;

			_context.Games.Add(AddingGame);
			try
			{
				_context.SaveChanges();
			}
			catch (Exception ex1)
			{
				MessageBox.Show(string.Format("Произошла ошибка:\n{0}\n{1}", ex1.Message, (ex1.InnerException != null ? ex1.InnerException.Message : ""))
					,"Ошибка"
					,MessageBoxButtons.OK
					,MessageBoxIcon.Error);
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
			Cursor = Cursors.WaitCursor;

			if (!OriginalNameEn.Checked)
				AddingGame.Original_Name = null;

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
			
			//Обновление платформ
			var toAdd = PlatformsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform)
				.Except(AddingGame.Platforms.Select(p => p.ID_Platform));
			var toRemove = AddingGame.Platforms.ToList().Select(p => p.ID_Platform)
				.Except(PlatformsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform));
			foreach (var p in toAdd)
				AddingGame.Platforms.Add(_context.Platforms.Find(p));
			foreach (var p in toRemove)
				AddingGame.Platforms.Remove(_context.Platforms.Find(p));

			//Обновление жанров
			toAdd = GenresList.CheckedItems.Cast<Genres>().Select(p => p.ID_Genre)
				.Except(AddingGame.Genres.Select(p => p.ID_Genre));
			toRemove = AddingGame.Genres.ToList().Select(p => p.ID_Genre)
				.Except(GenresList.CheckedItems.Cast<Genres>().Select(p => p.ID_Genre));
			foreach (var g in toAdd)
				AddingGame.Genres.Add(_context.Genres.Find(g));
			foreach (var g in toRemove)
				AddingGame.Genres.Remove(_context.Genres.Find(g));

			//Обновление защит
			toAdd = OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect)
				.Except(AddingGame.Online_protections.Select(p => p.ID_Protect));
			toRemove = AddingGame.Online_protections.ToList().Select(p => p.ID_Protect)
				.Except(OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect));
			foreach (var op in toAdd)
				AddingGame.Online_protections.Add(_context.Online_protections.Find(op));
			foreach (var op in toRemove)
				AddingGame.Online_protections.Remove(_context.Online_protections.Find(op));

			if (AddingGame.ID_Developer == 0)
				AddingGame.ID_Developer = null;

			if (AddingGame.ID_Publisher == 0)
				AddingGame.ID_Publisher = null;

			if (AddingGame.ID_RF_Distributor == 0)
				AddingGame.ID_RF_Distributor = null;

			if (AddingGame.ID_Ser == 0)
				AddingGame.ID_Ser = null;

			if (AddingGame.ID_Box == 0)
				AddingGame.ID_Box = null;

			if (AddingGame.ID_Edition == 0)
				AddingGame.ID_Edition = null;

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
				g.ID_Ser = AddingGame.ID_Ser;
				g.Series = AddingGame.Series;
				if (g.TypeContent ?? false) continue;
				g.Kol_updates = AddingGame.Kol_updates;
				g.Last_version = AddingGame.Last_version;

				toAdd = OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect)
					.Except(g.Online_protections.Select(p => p.ID_Protect));
				toRemove = g.Online_protections.AsQueryable().ToList().Select(p => p.ID_Protect)
					.Except(OnlineProtectionsList.CheckedItems.Cast<Online_protections>().Select(p => p.ID_Protect));
				foreach (var op in toAdd)
					g.Online_protections.Add(_context.Online_protections.Find(op));
				foreach (var op in toRemove)
					g.Online_protections.Remove(_context.Online_protections.Find(op));

				toAdd = PlatformsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform)
					.Except(g.Platforms.Select(p => p.ID_Platform));
				toRemove = g.Platforms.AsQueryable().ToList().Select(p => p.ID_Platform)
					.Except(PlatformsList.CheckedItems.Cast<Platforms>().Select(p => p.ID_Platform));
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
			var manageS = new ManageOPOS(2);
			manageS.ShowDialog();
			var LS = new List<Series> {new Series {Name = "<отсутствует>", ID_Ser = 0}};
			LS.AddRange(_context.Series.OrderBy(s => s.Name).ToArray());
			comboBox8.DataSource = LS;
			if (manageS.online_protectionsBindingSource.Current != null)
				((Games)gamesBindingSource.Current).ID_Ser = ((Series)manageS.online_protectionsBindingSource.Current).ID_Ser;
			comboBox8.SelectedValue = ((Games)gamesBindingSource.Current).ID_Ser;
			manageS.Dispose();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			var manageE = new ManageGEB(1);
			manageE.ShowDialog();
			var LE = new List<Editions> {new Editions {ID_Edition = 0, Name = "<отсутствует>"}};
			LE.AddRange(_context.Editions.ToArray());
			comboBox6.DataSource = LE;
			if (manageE.genresBindingSource.Current != null)
				((Games)gamesBindingSource.Current).ID_Edition = ((Editions)manageE.genresBindingSource.Current).ID_Edition;
			comboBox6.SelectedValue = ((Games)gamesBindingSource.Current).ID_Edition;
			manageE.Dispose();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			var manageB = new ManageGEB(2);
			manageB.ShowDialog();
			var LB = new List<Boxes>{new Boxes{ ID_Box = 0, Name = "<отсутствует>"}};
			LB.AddRange(_context.Boxes.ToArray());
			comboBox7.DataSource = LB;
			if (manageB.genresBindingSource.Current != null)
				((Games)gamesBindingSource.Current).ID_Box = ((Boxes)manageB.genresBindingSource.Current).ID_Box;
			comboBox7.SelectedValue = ((Games)gamesBindingSource.Current).ID_Box;
			manageB.Dispose();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			var manageP = new ManageOPOS(1);
			manageP.ShowDialog();
			PlatformsList.DataSource = platformsBindingSource;
			PlatformsList.DisplayMember = "Name";
			PlatformsList.ValueMember = "ID_Platform";
			PlatformsList.DataSource = _context.Platforms.ToList();
		}

		private void button8_Click(object sender, EventArgs e)
		{
			var manageP = new ManageOPOS(0);
			manageP.ShowDialog();
			OnlineProtectionsList.DataSource = online_protectionsBindingSource;
			OnlineProtectionsList.DisplayMember = "Name";
			OnlineProtectionsList.ValueMember = "ID_Protect";
			OnlineProtectionsList.DataSource = _context.Online_protections.ToList();
		}

		private void button9_Click(object sender, EventArgs e)
		{
			var manageP = new ManageGEB(0);
			manageP.ShowDialog();
			GenresList.DataSource = genresBindingSource;
			GenresList.DisplayMember = "Name";
			GenresList.ValueMember = "ID_Genre";
			GenresList.DataSource = _context.Genres.ToList();
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

		private void comboBox8_Leave(object sender, EventArgs e)
		{
			if (comboBox8.SelectedValue != null) return;
			/*MessageBox.Show("Недопустимая серия!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBox8.Focus();*/
			comboBox8.SelectedIndex = 0;
			((Games)gamesBindingSource.Current).ID_Ser = 0;
		}

		private void button10_Click(object sender, EventArgs e)
		{
			var manageD = new ManageDiskTypes();
			manageD.ShowDialog();
			var LDT = new List<Disk_types>();
			LDT.AddRange(_context.Disk_types.ToArray());
			DiskTypes.DataSource = LDT;
		}

		private void numericUpDown1_Enter(object sender, EventArgs e)
		{
			numericUpDown1.Select(0, 1);
		}

		private void PersonRate_Enter(object sender, EventArgs e)
		{
			PersonRate.Select(0, 3);
		}

		private void PersonRate_MouseDown(object sender, MouseEventArgs e)
		{
			PersonRate.Select(0, 3);
		}

		private void numericUpDown1_MouseDown(object sender, MouseEventArgs e)
		{
			numericUpDown1.Select(0, 1);
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
