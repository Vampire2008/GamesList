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
            string N;
            foreach (Games g in ser.Games)
            {
                if (g.Poster != null)
                {
                    stream = new MemoryStream(g.Poster);//Получаем поток данных постера из базы
                    imageList1.Images.Add(Image.FromStream(stream));
                }
                else
                {
                    imageList1.Images.Add(Properties.Resources.WlanMM_dll_3131_05_256x256x32bit);
                }
                if (g.Games2 != null)
                {
                    if (g.Games2.Name.IndexOf(":") > -1)
                    {
                        N = g.Games2.Name + " - " + g.Name;
                    }
                    else
                    {
                        N = g.Games2.Name + ": " + g.Name;
                    }
                }
                else
                {
                    N = g.Name;
                }
                listView1.Items.Add(new ListViewItem(N,i));
                listView1.Items[i].Tag = Decimal.ToInt32(g.ID_Game);
                i++;
            }
        }

        public SeriesInf(Games game)
        {
            InitializeComponent();
            this.Text = "Игры сборника " + game.Name;
            SerName.Text = game.Name;
            Description.Text = game.Description;
            MemoryStream stream;
            byte i = 0;
            listView1.Items.Clear();
            imageList1.Images.Clear();
            string N;
            foreach (Games g in game.Games11)
            {
                if (g.Poster != null)
                {
                    stream = new MemoryStream(g.Poster);//Получаем поток данных постера из базы
                    imageList1.Images.Add(Image.FromStream(stream));
                }
                else
                {
                    imageList1.Images.Add(Properties.Resources.WlanMM_dll_3131_05_256x256x32bit);
                }
                if (g.Games2 != null)
                {
                    if (g.Games2.Name.IndexOf(":") > -1)
                    {
                        N = g.Games2.Name + " - " + g.Name;
                    }
                    else
                    {
                        N = g.Games2.Name + ": " + g.Name;
                    }
                }
                else
                {
                    N = g.Name;
                }
                listView1.Items.Add(new ListViewItem(N, i));
                listView1.Items[i].Tag = Decimal.ToInt32(g.ID_Game);
                i++;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((this.Owner != null) && (Program.context.Games.Find((int)listView1.SelectedItems[0].Tag).ID_Content == null))
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
            }
        }
    }
}
