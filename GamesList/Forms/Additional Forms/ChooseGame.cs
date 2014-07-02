using System;
using System.Linq;
using System.Windows.Forms;
using GamesBase;

namespace GamesList
{
	public partial class ChooseGame : Form
	{
		private Games g;
		public ChooseGame(Games game)
		{
			InitializeComponent();
			g = game;
			var q = Program.context.Games.Where(
					gm => (gm.ID_Content == null) && (gm.Localisation_Type != -1) && (gm.ID_Game != g.ID_Game));
			gamesBindingSource.DataSource = q.ToList();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var gg = Program.context.Games.Find((decimal)nameComboBox.SelectedValue);
			g.OriginalGame = gg;
			g.ID_Content = gg.ID_Game;
			g.TypeContent = true;
			var addCon = new AddContent(g, 0);
			DialogResult = addCon.ShowDialog();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
