using GamesList.Model;
using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Data.Entity;
//using GamesList.Migrations;

namespace GamesList
{
    static class Program
    {
        /// <summary>
        /// Контекст базы данных, обеспечивает связь с базой данных.
        /// </summary>
        public static GamesEntities context;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool ok;
            var m = new System.Threading.Mutex(true, "YourNameHere", out ok);//Проверяем не запущена ли ещё одна копия программы
            if (!ok)//Если запущена, то выдаём ошибку и закрываем программу.
            {
                MessageBox.Show("Другая копия программы уже запущена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            

            if (Properties.Settings.Default.FirstRun)//Проверяем установлен ли флаг 1-ого запуска программы в Settings-файле
            {
                Form Wiz = new Wizard(args);//Создаём форму Мастера настройки
                if (Wiz.ShowDialog() == DialogResult.Cancel)//Открываем диалог работы с мастером и ждём его завершения
                {
                    return;//Если диалог завершился отменой, выходим из программы
                }
                Wiz.Dispose();//Освобождаем ресурсы диалога, т.к. он нам больше не нужен
            }
            var s = new SplashScreen("Resources/gws_landing_hero.png");
            s.Show(false, true);
            try
            {
                if (args.Length>=1)
                {
                    if (args[0] == "active")
                    {
                        context = new GamesEntities(buidConStr(Properties.Settings.Default.DefaultConStr));//Создаём новый контекст базы данных, используя подключение по умолчанию из файла настроек
                    }
                    else
                    {
                        context = new GamesEntities(buidConStr(args[0]));
                    }
                }
                else
                {
                    context = new GamesEntities(buidConStr(Properties.Settings.Default.DefaultConStr));//Создаём новый контекст базы данных, используя подключение по умолчанию из файла настроек
                }
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<GamesEntities, Migrations.Configuration>());
                if (Properties.Settings.Default.NewBase)//Если стоит флаг новой базы, то
                {
                    if (File.Exists(Properties.Settings.Default.DefaultConStr))//Проверяем существует ли файл с указанным именем.
                    {
                        try
                        {
                            File.Delete(Properties.Settings.Default.DefaultConStr);//Пробуем удалить файл
                        }
                        catch
                        {
                            MessageBox.Show("Не удаётся перезаписать файл! Программа перезапустится, чтобы вы могли заного выбрать файл с играми.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Если удалить не удаётся, выводим сообщение об ошибке
                            Properties.Settings.Default.FirstRun = true;//Устанавливаем флаг первого запуска, чтобы пользователь мог повторно создать базу.
                            Properties.Settings.Default.Save();//Сохраняем файл настроек
                            Application.Restart();//Перезапускаем программу
                            return;//Выходим из текущего экземпляра программы
                        }
                    }
                    context.Database.Create();//Создаём базу данных. Данный метод создаст файл базы данных в указанном месте.
                }
                Program.context.Database.Connection.Open();//Открваем соединение с базой
            }
            catch (Exception ex1)
            {//Если соединение с базой по умолчанию не удалось установить, то выводим следующий диалог
                DialogResult DR = MessageBox.Show("База данных по умолчанию (" + Properties.Settings.Default.DefaultConStr + ") не обнаружена. Создать новую базу?\n" +
                "Или нажмите \"Нет\" чтобы открыть другой файл базы данных.\n"+ex1.ToString(), "Ошибка открытия", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                //Предлагаем пользователию либо создать файл базы данны, либо открыть другую базу, или вообще выйти из программы
                switch (DR)//Перебираем результат диалога
                {
                    case DialogResult.Yes://Если пользователь нажал Да
                        context.Database.Create();//Создаём базу данных
                        break;
                    case DialogResult.No://Если пользователь нажал Нет
                        OpenFileDialog Dial = new OpenFileDialog();//Создаём диалог открытия файла
                        Dial.Title = "Выберите файл базы данных";//Задаём заголовок диалога
                        Dial.FileName = "MyGames.gdb";//Задаём имя по умолчанию
                        Dial.Filter = "База игр (*.gdb)|*.gdb";//Задаём фильтр диалога
                        if (Dial.ShowDialog() == DialogResult.OK)//Показываем диалог и проверяем результат
                        {
                            context = new GamesEntities(buidConStr(Dial.FileName));//Создаём контекст базы данных с выбранным файлом
                            try
                            {
                                context.Database.Connection.Open();//Пробуем установить соединение
                                if (MessageBox.Show("Вы хотите, чтобы эта база стала базой по умолчанию?", "База по умолчанию", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    Properties.Settings.Default.DefaultConStr = Dial.FileName;
                                    Properties.Settings.Default.Save();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Произошла ошибка.\n" + ex);//Если соединение установить не удалось, показываем ошибку.
                                return;//Выходим из программы
                            }
                        }
                        else
                        {
                            return;//Если пользователь отказался выбирать базу, то выходим из программы
                        }
                        break;
                    case DialogResult.Cancel://Если пользователь отказался создавать или открывать базу
                        return;//то выходим из программы
                }
            }
            s.Close(new TimeSpan(0, 0, 0, 0, 500));
            Application.Run(new GamesForm());//Запускаем главную форму
            GC.KeepAlive(m);//Нужно для проверки уже запущенной копии
        }

        /// <summary>
        /// Создаёт строку подключения формата Entity Framework.
        /// </summary>
        /// <param name="Path">Путь к базе данных SQL Server CE</param>
        /// <returns>Строка поделючения</returns>
        public static string buidConStr(string Path)
        {
            string providerName = "System.Data.SqlServerCe.4.0";//Задаём провайдера базы данных
            string serverName = Path;//Задаём путь к базе, как имя сервера
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();//Создаём экземпляр строителя строки подключения
            sqlBuilder.DataSource = serverName;//Задаём источник данных
            string providerString = sqlBuilder.ToString();//Получаем строку подключения к базе
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();//Создаём экземпляр строителя строки подключения формата Entity Framework
            entityBuilder.Provider = providerName;//Задаём провайдера БД
            entityBuilder.ProviderConnectionString = providerString;//Задаём строку подключения к БД, полученную ранее
            entityBuilder.Metadata = @"res://*/Model.Model1.csdl|
                                              res://*/Model.Model1.ssdl|
                                              res://*/Model.Model1.msl";// Задаём данные о модели БД
            return entityBuilder.ToString();//Возвращаем готовую строку подключения
        }

        public static string convertToVop(string ori)
        {
            return null;
        }
    }
}
