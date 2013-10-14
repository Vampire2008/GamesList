﻿using System;
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
    public partial class SaveNotSaveDialog : Form
    {
        public SaveNotSaveDialog()
        {
            InitializeComponent();
            pictureBox1.Image = SystemIcons.Warning.ToBitmap();
        }

        private void SaveNotSaveDialog_Shown(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Asterisk.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
