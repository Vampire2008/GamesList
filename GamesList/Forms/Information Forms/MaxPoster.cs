using System.Drawing;
using System.Windows.Forms;

namespace GamesList
{
	public partial class MaxPoster : Form
	{
		public MaxPoster(Image im)
		{
			InitializeComponent();
			MaximumSize = SystemInformation.PrimaryMonitorSize;
			pictureBox1.MaximumSize = SystemInformation.PrimaryMonitorSize;
			if (im.Size.Width > SystemInformation.PrimaryMonitorSize.Width)
			{
				int q = ((SystemInformation.PrimaryMonitorSize.Width - 100) * 100) / im.Size.Width;//Получаю процент изменения изображения от изначального до ширины экрана
				q = (im.Size.Height * q) / 100;//Получаю размер высоты изменённого изобраения на полученный выше процент
				if (q > (SystemInformation.PrimaryMonitorSize.Height - 100))//Больше ли новая высота высоты экрана?
				{
					AutoSize = false;//Отключаю автоизменение
					q = ((SystemInformation.PrimaryMonitorSize.Height - 100) * 100) / im.Size.Height;
					Height = (im.Size.Height * q) / 100;
					Width = (im.Size.Width * q) / 100;
					pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
					StartPosition = FormStartPosition.Manual;
					Location = new Point(10, 10);
					FormBorderStyle = FormBorderStyle.Sizable;
				}
				else
				{
					AutoSize = false;//Отключаю автоизменение
					q = ((SystemInformation.PrimaryMonitorSize.Width - 100) * 100) / im.Size.Width;
					Height = (im.Size.Height * q) / 100;
					Width = (im.Size.Width * q) / 100;
					pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
					StartPosition = FormStartPosition.Manual;
					Location = new Point(10, 10);
					FormBorderStyle = FormBorderStyle.Sizable;
				}
			}
			else if (im.Size.Height > SystemInformation.PrimaryMonitorSize.Height)
			{
				AutoSize = false;//Отключаю автоизменение
				int q = ((SystemInformation.PrimaryMonitorSize.Height - 100) * 100) / im.Size.Height;
				Height = (im.Size.Height * q) / 100;
				Width = (im.Size.Width * q) / 100;
				pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
				StartPosition = FormStartPosition.Manual;
				Location = new Point(10, 10);
				FormBorderStyle = FormBorderStyle.Sizable;
			}
			pictureBox1.Image = im;

		}
	}
}
