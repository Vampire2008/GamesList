using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using GamesList.Model;
using System.Globalization;

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
            ((Disk_types)disk_typesBindingSource.Current).ID_Disk_Type = (Program.context.Disk_types.Count() > 0 ? Program.context.Disk_types.Max(p => p.ID_Disk_Type) : 0) + 1;
        }

        private void ManageDiskTypes_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*disk_typesBindingSource.EndEdit();
            Program.context.SaveChanges();*/
        }

        private void disk_typesDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.ColumnIndex == dataGridViewTextBoxColumn4.Index)
            {
                string s = Convert.ToString(disk_typesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int i = s.IndexOf(".");
                if (i > -1)
                {
                    s[i] = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
                }
                ((string)disk_typesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).Replace('.', CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0]);
            }*/
            //byte i = ((Disk_types)disk_typesBindingSource.Current).Max_Size.
            disk_typesBindingSource.EndEdit();
            Program.context.SaveChanges();
        }

        private void disk_typesDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            
        }

        private void disk_typesDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTextBoxColumn4.Index)
            {
                string s = ((string)e.FormattedValue);
                if (CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator == ",")
                {
                    if (s.IndexOf(".") > -1)
                    {
                        s = s.Replace('.', CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0]);
                        disk_typesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToDouble(s);
                    }
                }
                else
                {
                    if (s.IndexOf(",") > -1)
                    {
                        s = s.Replace('.', CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0]);
                        disk_typesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToDouble(s);
                    }
                }
            }
        }

        private void disk_typesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
