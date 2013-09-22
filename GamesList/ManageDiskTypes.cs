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
            disk_typesBindingSource.EndEdit();
            Program.context.SaveChanges();
        }

        private void disk_typesDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            disk_typesBindingSource.EndEdit();
            Program.context.SaveChanges();
        }
    }
}
