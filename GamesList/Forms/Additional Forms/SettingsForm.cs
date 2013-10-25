using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesList
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.DefaultConStr;
            textBox2.Text = Properties.Settings.Default.Recenzor;
            numericUpDown1.Value = Properties.Settings.Default.MaxRecenzorRating;
            numericUpDown2.Value = Properties.Settings.Default.MaxYourRating;
            if (Properties.Settings.Default.DistrReg == "Региональный издатель")
            {
                DistrRegi.Checked = true;
                DistrIn.Checked = false;
                DistrInText.Enabled = false;
                DistrCust.Checked = false;
                DistrCustText.Enabled = false;
            }
            else if (Properties.Settings.Default.DistrReg.IndexOf("Издатель в") == 0)
            {
                DistrRegi.Checked = false;
                DistrIn.Checked = true;
                DistrInText.Enabled = true;
                DistrCust.Checked = false;
                DistrCustText.Enabled = false;
                DistrInText.Text = Properties.Settings.Default.DistrReg.Substring(11);
            }
            else
            {
                DistrRegi.Checked = false;
                DistrIn.Checked = false;
                DistrInText.Enabled = false;
                DistrCust.Checked = true;
                DistrCustText.Enabled = true;
                DistrCustText.Text = Properties.Settings.Default.DistrReg;
            }
            VisMax.Checked = Properties.Settings.Default.VisMax;
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
            if ((textBox1.Text == "") || (textBox1.Text == null) || (!File.Exists(textBox1.Text)))
            {
                MessageBox.Show("Выбранной базы не существует! Выберите существующую базу или отмените изменения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Properties.Settings.Default.Recenzor = textBox2.Text;
            Properties.Settings.Default.MaxRecenzorRating = Decimal.ToByte(numericUpDown1.Value);
            Properties.Settings.Default.MaxYourRating = Decimal.ToByte(numericUpDown2.Value);
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
            if (Properties.Settings.Default.DefaultConStr != textBox1.Text)
            {
                MessageBox.Show("Подключение к новой базе будет выполняться со следующего запуска программы.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Properties.Settings.Default.DefaultConStr = textBox1.Text;
            }
            Properties.Settings.Default.VisMax = VisMax.Checked;
            Properties.Settings.Default.Save();
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
