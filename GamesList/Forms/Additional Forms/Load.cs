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

namespace GamesList.Forms.Additional_Forms
{
	public partial class Load : Form
	{
		GamesEntities SecondBase;
		List<Games> GamesToAdd = new List<Games>();
		public Load()
		{
			InitializeComponent();
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				/*try
				{*/
					SecondBase = new GamesEntities(Program.buidConStr(openFileDialog1.FileName));
					SecondBase.Database.Connection.Open();
					if (!DBUpdater.checkDBVersion(SecondBase))
					{
						return;
					}
					List<string> MyGamesNames = new List<string>();
					foreach (var g in Program.context.Games)
					{
						MyGamesNames.Add(g.getName().ToUpper());
					}
					foreach (var g in SecondBase.Games)
					{
						if (!MyGamesNames.Contains(Program.getName(g).ToUpper()))
						{
							GamesToAdd.Add(g);
						}
					}
					GamesToAdd = GamesToAdd.OrderBy(g => g.Name).ToList();
					TreeNode TN, TN1;
					foreach (var g in GamesToAdd)
					{
						if (g.Name == "Rebellion")
						{
							g.Name.ToString();
						}
						if (g.Games2 != null)
						{
							if (g.Games2.Games2 != null)
							{
								TN = TreeNodeFinderRecursiv(win7StyleTreeView1.Nodes, g.Games2.Games2);
								if (TN == null)
								{
									TN = win7StyleTreeView1.Nodes.Add(g.Games2.Games2.Name);
									TN.Tag = g.Games2.Games2;
									if (Program.context.Games.FirstOrDefault(g1 => g1.Name == g.Games2.Games2.Name)!= null)
									{
										TN.Checked = true;
									}
								}
								TN1 = TreeNodeFinderRecursiv(TN.Nodes, g.Games2);
								if (TN1 == null)
								{
									TN1 = TN.Nodes.Add(g.Games2.Name);
									TN1.Tag = g.Games2;
									if (Program.context.Games.FirstOrDefault(g1 => g1.Name == g.Games2.Name) != null)
									{
										TN1.Checked = true;
									}
								}
								TN = TN1;
							}
							else
							{
								TN = TreeNodeFinderRecursiv(win7StyleTreeView1.Nodes, g.Games2);
								if (TN == null)
								{
									TN = win7StyleTreeView1.Nodes.Add(g.Games2.Name);
									TN.Tag = g.Games2;
									if (Program.context.Games.FirstOrDefault(g1 => g1.Name == g.Games2.Name) != null)
									{
										TN.Checked = true;
									}
								}
							}
							if (TreeNodeFinderRecursiv(TN.Nodes, g) == null)
							{
								TN = TN.Nodes.Add(g.Name);
								TN.Tag = g;
							}
						}
						else
						{
							if (TreeNodeFinderRecursiv(win7StyleTreeView1.Nodes, g) == null)
							{
								TN = win7StyleTreeView1.Nodes.Add(g.Name);
								TN.Tag = g;
							}
						}
					}
					GamesToAdd = new List<Games>();
				/*}
				catch(Exception ex)
				{
					MessageBox.Show("Не удалось открыть файл.\n"+ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}*/
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{

		}

		private TreeNode TreeNodeFinderRecursiv(TreeNodeCollection Nodes, Games game)
		{
			foreach (TreeNode TN in Nodes)
			{
				if (((Games)TN.Tag).ID_Game == game.ID_Game)
					return TN;
				if (TN.Nodes.Count > 0)
				{
					TreeNode TN1 = TreeNodeFinderRecursiv(TN.Nodes, game);
					if (TN1 != null)
					{
						return TN1;
					}
				}
			}
			return null;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (GamesToAdd.Count <= 0)
			{
				MessageBox.Show("Вы не выбрали ни одной игры для перекачки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			button1.Enabled = win7StyleTreeView1.Enabled = false;
			progressBar1.Maximum = GamesToAdd.Count;
			List<Games> ProblemGames = new List<Games>();
			foreach (var g in GamesToAdd)
			{
			/*	if ((Program.context.Games.SingleOrDefault(s => s.Developers.Name.ToUpper() == g.Developers.Name.ToUpper()) == null)||
					Program.context.Games.SingleOrDefault)
				{

				}*/
			}
		}

		private void win7StyleTreeView1_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Checked)
			{
				GamesToAdd.Add((Games)e.Node.Tag);
				if (e.Node.Parent != null)
				{
					if (e.Node.Parent.Parent != null)
					{
						e.Node.Parent.Parent.Checked = true;
					}
					e.Node.Parent.Checked = true;
				}
			}
			else
			{
				GamesToAdd.Remove((Games)e.Node.Tag);
				foreach (TreeNode TN in e.Node.Nodes)
				{
					TN.Checked = false;
					foreach (TreeNode TN1 in TN.Nodes)
					{
						TN1.Checked = false;
					}
				}
			}
		}

		private void win7StyleTreeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
		{
			if (Program.context.Games.FirstOrDefault(g1 => g1.Name == ((Games)e.Node.Tag).Name) != null)
			{
				e.Cancel = true;
			}
		}
	}
}
