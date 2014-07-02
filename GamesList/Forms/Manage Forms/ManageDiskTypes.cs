using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using System.Globalization;
using GamesBase;

namespace GamesList
{
	public partial class ManageDiskTypes : Form
	{
		public ManageDiskTypes()
		{
			InitializeComponent();
			disk_typesBindingSource.DataSource = Program.context.Disk_types.Local.ToBindingList();
			Program.context.Disk_types.Load();
		}

		private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
		{
			((Disk_types)disk_typesBindingSource.Current).ID_Disk_Type = (Program.context.Disk_types.Any() ? Program.context.Disk_types.Max(p => p.ID_Disk_Type) : 0) + 1;
		}

		private void ManageDiskTypes_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				disk_typesBindingSource.EndEdit();
				Program.context.SaveChanges();
			}
			catch { }
		}

		private void disk_typesDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			disk_typesBindingSource.EndEdit();
			Program.context.SaveChanges();
		}

		private void disk_typesDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (e.ColumnIndex != dataGridViewTextBoxColumn4.Index) return;
			var s = ((string)e.FormattedValue);
			if (CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator == ",")
			{
				if (s.IndexOf(".") <= -1) return;
				s = s.Replace('.', CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0]);
				disk_typesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToDouble(s);
			}
			else
			{
				if (s.IndexOf(",") <= -1) return;
				s = s.Replace('.', CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0]);
				disk_typesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToDouble(s);
			}
		}

		private void disk_typesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			MessageBox.Show(e.Exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
