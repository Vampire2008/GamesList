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
    public partial class MaxPoster : Form
    {
        public MaxPoster(Image im)
        {
            InitializeComponent();
            this.MaximumSize = SystemInformation.PrimaryMonitorSize;
            pictureBox1.MaximumSize = SystemInformation.PrimaryMonitorSize;
            if (im.Size.Width > SystemInformation.PrimaryMonitorSize.Width)
            {
                int q = ((SystemInformation.PrimaryMonitorSize.Width-100) * 100) / im.Size.Width;//Получаю процент изменения изображения от изначального до ширины экрана
                q = (im.Size.Height * q) / 100;//Получаю размер высоты изменённого изобраения на полученный выше процент
                if (q > (SystemInformation.PrimaryMonitorSize.Height-100))//Больше ли новая высота высоты экрана?
                {
                    this.AutoSize = false;//Отключаю автоизменение
                    q = ((SystemInformation.PrimaryMonitorSize.Height - 100) * 100) / im.Size.Height;
                    this.Height = (im.Size.Height * q)/100;
                    this.Width = (im.Size.Width * q) / 100;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(10, 10);
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                }
                else
                {
                    this.AutoSize = false;//Отключаю автоизменение
                    q = ((SystemInformation.PrimaryMonitorSize.Width - 100) * 100) / im.Size.Width;
                    this.Height = (im.Size.Height * q) / 100;
                    this.Width = (im.Size.Width * q) / 100;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(10, 10);
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                }
            }
            else if (im.Size.Height > SystemInformation.PrimaryMonitorSize.Height)
            {
                    this.AutoSize = false;//Отключаю автоизменение
                    int q = ((SystemInformation.PrimaryMonitorSize.Height - 100) * 100) / im.Size.Height;
                    this.Height = (im.Size.Height * q) / 100;
                    this.Width = (im.Size.Width * q) / 100;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(10, 10);
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
            pictureBox1.Image = im;
            
        }
    }
}
