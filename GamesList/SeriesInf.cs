using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GamesList.Model;
using System.IO;

namespace GamesList
{
    public partial class SeriesInf : Form
    {
        public SeriesInf(Series ser)
        {
            InitializeComponent();
            this.Text = "Информация о серии " + ser.Name;
            SerName.Text = ser.Name;
            Description.Text = ser.Description;
            MemoryStream stream;
            byte i=0;
            listView1.Items.Clear();
            imageList1.Images.Clear();
            foreach (Games g in ser.Games)
            {
                stream = new MemoryStream(g.Poster);//Получаем поток данных постера из базы
                imageList1.Images.Add(Image.FromStream(stream));
                listView1.Items.Add(new ListViewItem(g.Name,i));
                listView1.Items[i].Tag = Decimal.ToInt32(g.ID_Game);
                i++;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ((GamesForm)this.Owner).gamesBindingSource.Position = 0;
            while (((GamesForm)this.Owner).gamesBindingSource.Position + 1 < ((GamesForm)this.Owner).gamesBindingSource.Count)
            {
                if (((Games)((GamesForm)this.Owner).gamesBindingSource.Current).ID_Game == (int)listView1.SelectedItems[0].Tag)
                {
                    break;
                }
                else
                {
                    ((GamesForm)this.Owner).gamesBindingSource.MoveNext();
                }
            }
                //var i = ((GamesForm)this.Owner).gamesBindingSource.IndexOf((int)listView1.SelectedItems[0].Tag);
                //((GamesForm)this.Owner).gamesBindingSource.Position = i;
        }
    }
}
