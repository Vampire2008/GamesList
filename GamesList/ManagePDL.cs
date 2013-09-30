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
using System.Data.Objects;
using GamesList.Model;
using System.Globalization;

namespace GamesList
{
    public partial class ManagePDL : Form
    {
        private byte type;
        public ManagePDL(byte typeManage)
        {
            InitializeComponent();
            switch (typeManage){
                case 0:
                    publishersBindingSource.DataSource = Program.context.Publishers.Local.ToBindingList();
                    Program.context.Publishers.Load();
                    //publishersBindingSource.DataSource = Program.context.Publishers.Execute(MergeOption.AppendOnly);
                    break;
                case 1:
                    publishersBindingSource.DataSource = Program.context.Developers.Local.ToBindingList();
                    Program.context.Developers.Load();
                    break;
                case 2:
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
          /*  try
            {
                publishersBindingSource.EndEdit();
                Program.context.SaveChanges();
            }
            catch { }*/
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            switch (type){
                case 0:
                ((Publishers)publishersBindingSource.Current).Id_Publisher = (Program.context.Publishers.Count() >0 ? Program.context.Publishers.Max(p => p.Id_Publisher) : 0) + 1;
                ((Publishers)publishersBindingSource.Current).Is_open = false;
                break;
                case 1:
                ((Developers)publishersBindingSource.Current).ID_Developer = (Program.context.Developers.Count() > 0 ? Program.context.Developers.Max(p => p.ID_Developer) : 0) + 1;
                ((Developers)publishersBindingSource.Current).Is_open = false;
                break;
                case 2:
                ((RF_Distributors)publishersBindingSource.Current).ID_RF_Distributor = (Program.context.RF_Distributors.Count() > 0 ? Program.context.RF_Distributors.Max(p => p.ID_RF_Distributor) : 0) + 1;
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
            {
                MessageBox.Show("Введите дату в формате "+CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern+". Или нажмите Esc, чтобы отменить ввод.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(e.Exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
    }
}
