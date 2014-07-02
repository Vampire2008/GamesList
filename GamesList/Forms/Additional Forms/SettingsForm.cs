using System;
using System.IO;
using System.Windows.Forms;
using GamesList.Properties;

namespace GamesList
{
	public partial class SettingsForm : Form
	{
		public SettingsForm()
		{
			InitializeComponent();
			textBox1.Text = Settings.Default.DefaultConStr;
			textBox2.Text = Settings.Default.Recenzor;
			numericUpDown1.Value = Settings.Default.MaxRecenzorRating;
			numericUpDown2.Value = Settings.Default.MaxYourRating;
			if (Settings.Default.DistrReg == "Региональный издатель")
			{
				DistrRegi.Checked = true;
				DistrIn.Checked = false;
				DistrInText.Enabled = false;
				DistrCust.Checked = false;
				DistrCustText.Enabled = false;
			}
			else if (Settings.Default.DistrReg.IndexOf("Издатель в") == 0)
			{
				DistrRegi.Checked = false;
				DistrIn.Checked = true;
				DistrInText.Enabled = true;
				DistrCust.Checked = false;
				DistrCustText.Enabled = false;
				DistrInText.Text = Settings.Default.DistrReg.Substring(11);
			}
			else
			{
				DistrRegi.Checked = false;
				DistrIn.Checked = false;
				DistrInText.Enabled = false;
				DistrCust.Checked = true;
				DistrCustText.Enabled = true;
				DistrCustText.Text = Settings.Default.DistrReg;
			}
			VisMax.Checked = Settings.Default.VisMax;
			CurrentBase.Text = Program.CurrentBase;
			if (CurrentBase.Text != textBox1.Text)
			{
				MakeCurrent.Enabled = true;
			}
			else
			{
				MakeCurrent.Enabled = false;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if ((string.IsNullOrEmpty(textBox1.Text)) || (!File.Exists(textBox1.Text)))
			{
				MessageBox.Show("Выбранной базы не существует! Выберите существующую базу или отмените изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Settings.Default.Recenzor = textBox2.Text;
			Settings.Default.MaxRecenzorRating = Decimal.ToByte(numericUpDown1.Value);
			Settings.Default.MaxYourRating = Decimal.ToByte(numericUpDown2.Value);
			if (DistrRegi.Checked)
			{
				Settings.Default.DistrReg = "Региональный издатель";
			}
			else if (DistrIn.Checked)
			{
				Settings.Default.DistrReg = "Издатель в " + DistrInText.Text;
			}
			else
			{
				Settings.Default.DistrReg = DistrCustText.Text;
			}
			if (Settings.Default.DefaultConStr != textBox1.Text)
			{
				MessageBox.Show("Подключение к новой базе будет выполняться со следующего запуска программы.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Settings.Default.DefaultConStr = textBox1.Text;
			}
			Settings.Default.VisMax = VisMax.Checked;
			Settings.Default.Save();
			DialogResult = DialogResult.OK;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text = openFileDialog1.FileName;
			}
		}

		private void MakeCurrent_Click(object sender, EventArgs e)
		{
			textBox1.Text = CurrentBase.Text;
			MakeCurrent.Enabled = false;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (textBox1.Text != CurrentBase.Text)
			{
				MakeCurrent.Enabled = true;
			}
			else
				MakeCurrent.Enabled = false;
		}
	}
}
