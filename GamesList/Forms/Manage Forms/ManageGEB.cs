using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using GamesBase;

namespace GamesList
{
	public partial class ManageGEB : Form
	{
		private byte type;
		public ManageGEB(byte typeManage)
		{
			InitializeComponent();
			switch (typeManage)
			{
				case 0:
					Text = "Управление жанрами";
					genresBindingSource.DataSource = Program.context.Genres.Local.ToBindingList();
					Program.context.Genres.Load();
					dataGridViewTextBoxColumn2.HeaderText = "Жанр";
					break;
				case 1:
					Text = "Управление видами изданий";
					genresBindingSource.DataSource = Program.context.Editions.Local.ToBindingList();
					Program.context.Editions.Load();
					dataGridViewTextBoxColumn2.HeaderText = "Издание";
					break;
				case 2:
					Text = "Управление комплектациями";
					genresBindingSource.DataSource = Program.context.Boxes.Local.ToBindingList();
					Program.context.Boxes.Load();
					dataGridViewTextBoxColumn2.HeaderText = "Вид комплектации";
					break;
			}
			type = typeManage;
		}

		private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
		{
			switch (type)
			{
				case 0:
					((Genres)genresBindingSource.Current).ID_Genre = (Program.context.Genres.Any() ? Program.context.Genres.Max(p => p.ID_Genre) : 0) + 1;
					break;
				case 1:
					((Editions)genresBindingSource.Current).ID_Edition = (Program.context.Editions.Any() ? Program.context.Editions.Max(p => p.ID_Edition) : 0) + 1;
					break;
				case 2:
					((Boxes)genresBindingSource.Current).ID_Box = (Program.context.Boxes.Any() ? Program.context.Boxes.Max(p => p.ID_Box) : 0) + 1;
					break;
			}

		}

		private void ManageGenres_Load(object sender, EventArgs e)
		{
		}

		private void ManageGenres_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				genresBindingSource.EndEdit();
				Program.context.SaveChanges();
			}
			catch { }
		}

		private void genresDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				genresBindingSource.EndEdit();
				Program.context.SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
	}
}
