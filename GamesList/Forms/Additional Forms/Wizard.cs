using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace GamesList
{
	public partial class Wizard : Form
	{
		public Wizard()
		{
			InitializeComponent();
		}

		public Wizard(string[] args)
		{
			InitializeComponent();
			if ((args.Length < 1) || ((args[0] == null) && (args[0] == ""))) return;
			OpenPath.Text = args[0];
			radioButton1.Checked = false;
			radioButton2.Checked = true;
			SavePath.Enabled = false;
			SaveButton.Enabled = false;
			checkBox1.Enabled = false;
			args = new string[] { "active" };
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				SavePath.Text = saveFileDialog1.FileName;
			}
		}

		private void OpenButton_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				OpenPath.Text = openFileDialog1.FileName;
			}
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			if (!radioButton1.Checked) return;
			checkBox1.Enabled = true;
			SavePath.Enabled = true;
			SaveButton.Enabled = true;
			OpenPath.Enabled = false;
			OpenButton.Enabled = false;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			if (!radioButton2.Checked) return;
			checkBox1.Enabled = false;
			SavePath.Enabled = false;
			SaveButton.Enabled = false;
			OpenPath.Enabled = true;
			OpenButton.Enabled = true;
		}

		private void wizardControl1_Finished(object sender, EventArgs e)
		{
			Properties.Settings.Default.FirstRun = false;
			Properties.Settings.Default.DefaultConStr = radioButton1.Checked ? SavePath.Text : OpenPath.Text;
			Properties.Settings.Default.NewBase = radioButton1.Checked;
			Properties.Settings.Default.Recenzor = Recenzor.Text;
			Properties.Settings.Default.MaxRecenzorRating = Decimal.ToByte(MaxRating.Value);
			Properties.Settings.Default.MaxYourRating = Decimal.ToByte(MaxYourRating.Value);
			if (DistrRegi.Checked)
			{
				Properties.Settings.Default.DistrReg = "Региональный издатель";
			}
			else if (DistrIn.Checked)
			{
				Properties.Settings.Default.DistrReg = "Издатель в " + DistrInText.Text;
			}
			else
			{
				Properties.Settings.Default.DistrReg = DistrCustText.Text;
			}
			Properties.Settings.Default.VisMax = VisMax.Checked;
			Properties.Settings.Default.Save();
			DialogResult = DialogResult.OK;
		}

		private void BasePage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			if (radioButton1.Checked)
			{
				if ((!string.IsNullOrEmpty(SavePath.Text)) && (Directory.Exists(Path.GetDirectoryName(SavePath.Text)))) return;
				//SavePath.Text = Path.GetDirectoryName(SavePath.Text);
				label6.Visible = true;
				pictureBox1.Image = SystemIcons.Error.ToBitmap();
				pictureBox1.Visible = true;
				e.Cancel = true;
			}
			else
			{
				if ((!string.IsNullOrEmpty(OpenPath.Text)) && (File.Exists(OpenPath.Text))) return;
				label6.Visible = true;
				pictureBox1.Image = SystemIcons.Error.ToBitmap();
				pictureBox1.Visible = true;
				e.Cancel = true;
			}
		}

		private void DistrRegi_CheckedChanged(object sender, EventArgs e)
		{
			if (!DistrRegi.Checked) return;
			DistrInText.Enabled = false;
			DistrCustText.Enabled = false;
		}

		private void DistrIn_CheckedChanged(object sender, EventArgs e)
		{
			if (!DistrIn.Checked) return;
			DistrInText.Enabled = true;
			DistrCustText.Enabled = false;
		}

		private void DistrCust_CheckedChanged(object sender, EventArgs e)
		{
			if (!DistrCust.Checked) return;
			DistrInText.Enabled = true;
			DistrCustText.Enabled = true;
		}
	}
}
