using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using System.Globalization;
using GamesBase;

namespace GamesList
{
	public partial class ManagePDL : Form
	{
		private byte type;
		public ManagePDL(byte typeManage)
		{
			InitializeComponent();
			switch (typeManage)
			{
				case 0:
					Text = "Управление издателями";
					publishersBindingSource.DataSource = Program.context.Publishers.Local.ToBindingList();
					Program.context.Publishers.Load();
					break;
				case 1:
					Text = "Управление разработчиками";
					publishersBindingSource.DataSource = Program.context.Developers.Local.ToBindingList();
					Program.context.Developers.Load();
					break;
				case 2:
					Text = "Управление региональными издателями";
					publishersBindingSource.DataSource = Program.context.RF_Distributors.Local.ToBindingList();
					Program.context.RF_Distributors.Load();
					break;
			}
			type = typeManage;

		}

		private void iconPictureBox_DoubleClick(object sender, EventArgs e)
		{
			if (ChooseImage.ShowDialog() == DialogResult.OK)
			{
				iconPictureBox.Image = Image.FromFile(ChooseImage.FileName);
				publishersBindingSource.EndEdit();
				Program.context.SaveChanges();
			}
		}

		private void ManagePublisher_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				publishersBindingSource.EndEdit();
				Program.context.SaveChanges();
			}
			catch { }
		}

		private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
		{
			switch (type)
			{
				case 0:
					((Publishers)publishersBindingSource.Current).Id_Publisher = (Program.context.Publishers.Any() ? Program.context.Publishers.Max(p => p.Id_Publisher) : 0) + 1;
					((Publishers)publishersBindingSource.Current).Is_open = false;
					break;
				case 1:
					((Developers)publishersBindingSource.Current).ID_Developer = (Program.context.Developers.Any() ? Program.context.Developers.Max(p => p.ID_Developer) : 0) + 1;
					((Developers)publishersBindingSource.Current).Is_open = false;
					break;
				case 2:
					((RF_Distributors)publishersBindingSource.Current).ID_RF_Distributor = (Program.context.RF_Distributors.Any() ? Program.context.RF_Distributors.Max(p => p.ID_RF_Distributor) : 0) + 1;
					((RF_Distributors)publishersBindingSource.Current).Is_open = false;
					break;
			}
		}

		private void publishersDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				publishersBindingSource.EndEdit();
				Program.context.SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void publishersDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			if ((e.ColumnIndex == 1) || (e.ColumnIndex == 3))
				MessageBox.Show(string.Format("Введите дату в формате {0}. Или нажмите Esc, чтобы отменить ввод.",CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
				MessageBox.Show(e.Exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void publishersDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			if (e.ColumnIndex != dataGridViewTextBoxColumn4.Index && e.ColumnIndex != dataGridViewTextBoxColumn5.Index) return;
			//MessageBox.Show(publishersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
			if (publishersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
			{
				publishersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
			}
		}
	}
}
