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

namespace GamesList
{
    public partial class ChooseGame : Form
    {
        private Games g;
        public ChooseGame(Games game)
        {
            InitializeComponent();
            g = game;
            var q = from Games in Program.context.Games
                    where (Games.ID_Content == null) && (Games.Localisation_Type != -1) && (Games.ID_Game != g.ID_Game)
                    select Games;
            gamesBindingSource.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Games gg = Program.context.Games.Find((decimal)nameComboBox.SelectedValue);
            g.Games2 = gg;
            g.ID_Content = gg.ID_Game;
            g.TypeContent = true;
            Form AddCon = new AddContent(g,0);
            DialogResult =  AddCon.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
