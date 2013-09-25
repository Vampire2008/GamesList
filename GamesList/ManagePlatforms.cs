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
    public partial class ManagePlatforms : Form
    {
        public ManagePlatforms()
        {
            InitializeComponent();
            platformsBindingSource.DataSource = Program.context.Platforms.Local.ToBindingList();
            Program.context.Platforms.Load();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            ((Platforms)platformsBindingSource.Current).ID_Platform = (Program.context.Platforms.Count() > 0 ? Program.context.Platforms.Max(p => p.ID_Platform) : 0) + 1;
        }

        private void ManagePlatforms_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*platformsBindingSource.EndEdit();
            Program.context.SaveChanges();*/
        }

        private void platformsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            platformsBindingSource.EndEdit();
            Program.context.SaveChanges();
        }
    }
}
