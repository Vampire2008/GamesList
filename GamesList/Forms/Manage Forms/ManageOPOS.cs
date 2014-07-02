using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using GamesBase;

namespace GamesList
{
	public partial class ManageOPOS : Form
	{
		private byte typem;
		public ManageOPOS(byte type)
		{
			InitializeComponent();
			switch (type)
			{
				case 0:
					Text = "Управление онлайн-защитами";
					online_protectionsBindingSource.DataSource = Program.context.Online_protections.Local.ToBindingList();
					Program.context.Online_protections.Load();
					break;
				case 1:
					Text = "Управление платформами";
					online_protectionsBindingSource.DataSource = Program.context.Platforms.Local.ToBindingList();
					Program.context.Platforms.Load();
					break;
				case 2:
					Text = "Управление сериями";
					online_protectionsBindingSource.DataSource = Program.context.Series.Local.ToBindingList();
					Program.context.Series.Load();
					break;
			}
			typem = type;
		}

		private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
		{
			switch (typem)
			{
				case 0:
					((Online_protections)online_protectionsBindingSource.Current).ID_Protect = (Program.context.Online_protections.Any() ? Program.context.Online_protections.Max(p => p.ID_Protect) : 0) + 1;
					break;
				case 1:
					((Platforms)online_protectionsBindingSource.Current).ID_Platform = (Program.context.Platforms.Any() ? Program.context.Platforms.Max(p => p.ID_Platform) : 0) + 1;
					break;
				case 2:
					((Series)online_protectionsBindingSource.Current).ID_Ser = (Program.context.Series.Any() ? Program.context.Series.Max(p => p.ID_Ser) : 0) + 1;
					break;
			}
		}

		private void ManageOnlineProtects_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				online_protectionsBindingSource.EndEdit();
				Program.context.SaveChanges();
			}
			catch { }
		}

		private void online_protectionsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				online_protectionsBindingSource.EndEdit();
				Program.context.SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void iconPictureBox_Click(object sender, EventArgs e)
		{
			if (ChooseImage.ShowDialog() != DialogResult.OK) return;
			iconPictureBox.Image = Image.FromFile(ChooseImage.FileName);
			online_protectionsBindingSource.EndEdit();
			Program.context.SaveChanges();
		}
	}
}
