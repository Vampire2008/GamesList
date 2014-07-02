using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using GamesBase;

namespace GamesList
{
	public partial class InformationWindows : Form
	{
		public InformationWindows(byte type, object obj)
		{
			InitializeComponent();
			switch (type)
			{
				case 0:
					Text = string.Format("Разработчик - {0}", ((Developers)obj).Name);
					NameI.Text = string.Format("{0}{1}", ((Developers)obj).Name, (((Developers)obj).Is_open ?? true ? "" : " (закрыты)"));
					label1.Visible = true;
					DateOpen.Visible = true;
					DateOpen.Text = ((Developers)obj).Date_open.HasValue ? ((Developers)obj).Date_open.Value.ToString("dd.MM.yyyy") : "<отсутствует>";
					if (((Developers)obj).Is_open ?? true)
					{
						DateClose.Visible = false;
						label2.Visible = false;
					}
					else
					{
						DateClose.Visible = true;
						label2.Visible = true;
						DateClose.Text = ((Developers)obj).Date_close.HasValue ? ((Developers)obj).Date_close.Value.ToString("dd.MM.yyyy") : "<отсутствует>";
					}
					label3.Visible = false;
					Description.Text = ((Developers)obj).Description;
					if (((Developers)obj).Icon != null)
					{
						var stream = new MemoryStream(((Developers)obj).Icon);//Получаем поток данных постера из базы
						icon.Image = Image.FromStream(stream);//Создаём изображение для компоненты из потока
					}
					else
					{
						icon.Image = null;
					}
					break;
				case 1:
					Text = string.Format("Издатель - {0}", ((Publishers)obj).Name);
					NameI.Text = string.Format("{0}{1}", ((Publishers)obj).Name, (((Publishers)obj).Is_open ?? true ? "" : " (закрыты)"));
					label1.Visible = true;
					DateOpen.Visible = true;
					DateOpen.Text = ((Publishers)obj).Date_open.HasValue ? ((Publishers)obj).Date_open.Value.ToString("dd.MM.yyyy") : "<отсутствует>";
					if (((Publishers)obj).Is_open ?? false)
					{
						DateClose.Visible = false;
						label2.Visible = false;
					}
					else
					{
						DateClose.Visible = true;
						label2.Visible = true;
						DateClose.Text = ((Publishers)obj).Date_close.HasValue ? ((Publishers)obj).Date_close.Value.ToString("dd.MM.yyyy") : "<отсутствует>";
					}
					label3.Visible = false;
					Description.Text = ((Publishers)obj).Description;
					if (((Publishers)obj).Icon != null)
					{
						var stream = new MemoryStream(((Publishers)obj).Icon);//Получаем поток данных постера из базы
						icon.Image = Image.FromStream(stream);//Создаём изображение для компоненты из потока
					}
					else
					{
						icon.Image = null;
					}
					break;
				case 2:
					Text = string.Format("Издатель в России - {0}", ((RF_Distributors)obj).Name);
					NameI.Text = string.Format("{0}{1}", ((RF_Distributors)obj).Name, (((RF_Distributors)obj).Is_open ?? true ? "" : " (закрыты)"));
					label1.Visible = true;
					DateOpen.Visible = true;
					DateOpen.Text = (((RF_Distributors)obj).Date_open).HasValue ? ((RF_Distributors)obj).Date_open.Value.ToString("dd.MM.yyyy") : "<отсутствует>";
					if (((RF_Distributors)obj).Is_open ?? true)
					{
						DateClose.Visible = false;
						label2.Visible = false;
					}
					else
					{
						DateClose.Visible = true;
						label2.Visible = true;
						DateClose.Text = ((RF_Distributors)obj).Date_close.HasValue ? ((RF_Distributors)obj).Date_close.Value.ToString("dd.MM.yyyy") : "<отсутствует>";
					}
					label3.Visible = false;
					Description.Text = ((RF_Distributors)obj).Description;
					if (((RF_Distributors)obj).Icon != null)
					{
						var stream = new MemoryStream(((RF_Distributors)obj).Icon);//Получаем поток данных постера из базы
						icon.Image = Image.FromStream(stream);//Создаём изображение для компоненты из потока
					}
					else
					{
						icon.Image = null;
					}
					break;
				case 3:
					Text = string.Format("Платформа - {0}", ((Platforms)obj).Name);
					NameI.Text = ((Platforms)obj).Name;
					label1.Visible = false;
					DateOpen.Visible = false;
					DateClose.Visible = false;
					label2.Visible = false;
					label3.Visible = true;
					Description.Visible = true;
					Description.Text = ((Platforms)obj).Description;
					if (((Platforms)obj).Icon != null)
					{
						var stream = new MemoryStream(((Platforms)obj).Icon);//Получаем поток данных постера из базы
						icon.Image = Image.FromStream(stream);//Создаём изображение для компоненты из потока
					}
					else
					{
						icon.Image = null;
					}
					break;
				case 4:
					Text = string.Format("Онлайн-защита - {0}", ((Online_protections)obj).Name);
					NameI.Text = ((Online_protections)obj).Name;
					label1.Visible = false;
					DateOpen.Visible = false;
					DateClose.Visible = false;
					label2.Visible = false;
					label3.Visible = true;
					Description.Visible = true;
					Description.Text = ((Online_protections)obj).Description;
					if (((Online_protections)obj).Icon != null)
					{
						var stream = new MemoryStream(((Platforms)obj).Icon);//Получаем поток данных постера из базы
						icon.Image = Image.FromStream(stream);//Создаём изображение для компоненты из потока
					}
					else
					{
						icon.Image = null;
					}
					break;
			}
		}
	}
}
