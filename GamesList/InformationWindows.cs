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
using System.IO;

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
                    this.Text = "Разработчик - " + ((Developers)obj).Name;
                    NameI.Text = ((Developers)obj).Name + ((bool)((Developers)obj).Is_open ? "" : " (закрыты)");
                    label1.Visible = true;
                    DateOpen.Visible = true;
                    DateOpen.Text = ((DateTime)((Developers)obj).Date_open).ToString("dd.MM.yyyy");
                    if ((bool)((Developers)obj).Is_open)
                    {
                        DateClose.Visible = false;
                        label2.Visible = false;
                    }
                    else
                    {
                        DateClose.Visible = true;
                        label2.Visible = true;
                        DateClose.Text = ((DateTime)((Developers)obj).Date_close).ToString("dd.MM.yyyy");
                    }
                    label3.Visible = false;
                    Description.Visible = false;
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
                    this.Text = "Издатель - " + ((Publishers)obj).Name;
                    NameI.Text = ((Publishers)obj).Name + ((bool)((Publishers)obj).Is_open ? "" : " (закрыты)");
                    label1.Visible = true;
                    DateOpen.Visible = true;
                    DateOpen.Text = ((DateTime)((Publishers)obj).Date_open).ToString("dd.MM.yyyy");
                    if ((bool)((Publishers)obj).Is_open)
                    {
                        DateClose.Visible = false;
                        label2.Visible = false;
                    }
                    else
                    {
                        DateClose.Visible = true;
                        label2.Visible = true;
                        DateClose.Text = ((DateTime)((Publishers)obj).Date_close).ToString("dd.MM.yyyy");
                    }
                    label3.Visible = false;
                    Description.Visible = false;
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
                    this.Text = "Издатель в России - " + ((RF_Distributors)obj).Name;
                    NameI.Text = ((RF_Distributors)obj).Name + ((bool)((RF_Distributors)obj).Is_open ? "" : " (закрыты)");
                    label1.Visible = true;
                    DateOpen.Visible = true;
                    if ((((RF_Distributors)obj).Date_open) != null)
                    {
                        DateOpen.Text = ((DateTime)((RF_Distributors)obj).Date_open).ToString("dd.MM.yyyy");
                    }
                    else
                    {
                        DateOpen.Text = "<отсутствует>";
                    }
                    if ((bool)((RF_Distributors)obj).Is_open)
                    {
                        DateClose.Visible = false;
                        label2.Visible = false;
                    }
                    else
                    {
                        DateClose.Visible = true;
                        label2.Visible = true;
                        DateClose.Text = ((DateTime)((RF_Distributors)obj).Date_close).ToString("dd.MM.yyyy");
                    }
                    label3.Visible = false;
                    Description.Visible = false;
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
                    this.Text = "Платформа - " + ((Platforms)obj).Name;
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
                    this.Text = "Онлайн-защита - " + ((Online_protections)obj).Name;
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
