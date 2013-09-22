using GamesList.Model;
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
                    genresBindingSource.DataSource = Program.context.Genres.Local.ToBindingList();
                    Program.context.Genres.Load();
                    dataGridViewTextBoxColumn2.HeaderText = "Жанр";
                    break;
                case 1:
                    genresBindingSource.DataSource = Program.context.Editions.Local.ToBindingList();
                    Program.context.Editions.Load();
                    dataGridViewTextBoxColumn2.HeaderText = "Издание";
                    break;
                case 2:
                    genresBindingSource.DataSource = Program.context.Boxes.Local.ToBindingList();
                    Program.context.Boxes.Load();
                    dataGridViewTextBoxColumn2.HeaderText = "Коробка";
                    break;
            }
            type = typeManage;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            switch (type)
            {
                case 0:
                    ((Genres)genresBindingSource.Current).ID_Genre = (Program.context.Genres.Count() > 0 ? Program.context.Genres.Max(p => p.ID_Genre) : 0) + 1;
                    break;
                case 1:
                    ((Editions)genresBindingSource.Current).ID_Edition = (Program.context.Editions.Count() > 0 ? Program.context.Editions.Max(p => p.ID_Edition) : 0) + 1;
                    break;
                case 2:
                    ((Boxes)genresBindingSource.Current).ID_Box = (Program.context.Boxes.Count() > 0 ? Program.context.Boxes.Max(p => p.ID_Box) : 0) + 1;
                    break;
            }
            
        }

        private void ManageGenres_Load(object sender, EventArgs e)
        {
        }

        private void ManageGenres_FormClosing(object sender, FormClosingEventArgs e)
        {
            genresBindingSource.EndEdit();
            Program.context.SaveChanges();
        }

        private void genresDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            genresBindingSource.EndEdit();
            Program.context.SaveChanges();
        }
    }
}
