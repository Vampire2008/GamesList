using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using GamesBase;
using GamesList.Properties;

namespace GamesList
{
	public partial class SeriesInf : Form
	{
		public SeriesInf(Series ser)
		{
			InitializeComponent();
			Text = string.Format("Информация о серии {0}", ser.Name);
			SerName.Text = ser.Name;
			Description.Text = ser.Description;
			byte i = 0;
			listView1.Items.Clear();
			imageList1.Images.Clear();
			foreach (var g in ser.Games)
			{
				if (g.Poster != null)
				{
					var stream = new MemoryStream(g.Poster);
					imageList1.Images.Add(Image.FromStream(stream));
				}
				else
				{
					imageList1.Images.Add(Resources.WlanMM_dll_3131_05_256x256x32bit);
				}
				var n = g.GetFullName();
				listView1.Items.Add(new ListViewItem(n, i));
				listView1.Items[i].Tag = Decimal.ToInt32(g.ID_Game);
				i++;
			}
		}

		public SeriesInf(Games game)
		{
			InitializeComponent();
			Text = string.Format("Игры сборника {0}", game.Name);
			SerName.Text = game.Name;
			Description.Text = game.Description;
			byte i = 0;
			listView1.Items.Clear();
			imageList1.Images.Clear();
			foreach (var g in game.GamesInCollect)
			{
				if (g.Poster != null)
				{
					var stream = new MemoryStream(g.Poster);
					imageList1.Images.Add(Image.FromStream(stream));
				}
				else
				{
					imageList1.Images.Add(Resources.WlanMM_dll_3131_05_256x256x32bit);
				}
				var n = g.GetFullName();
				listView1.Items.Add(new ListViewItem(n, i));
				listView1.Items[i].Tag = Decimal.ToInt32(g.ID_Game);
				i++;
			}
		}

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if ((Owner == null) || (Program.context.Games.Find((int) listView1.SelectedItems[0].Tag).ID_Content != null)) return;
			((GamesForm)Owner).gamesBindingSource.Position = 0;
			while (((GamesForm)Owner).gamesBindingSource.Position + 1 < ((GamesForm)Owner).gamesBindingSource.Count)
			{
				if (((Games)((GamesForm)Owner).gamesBindingSource.Current).ID_Game == (int)listView1.SelectedItems[0].Tag)
				{
					break;
				}
				else
				{
					((GamesForm)Owner).gamesBindingSource.MoveNext();
				}
			}
		}
	}
}
