using System.Collections.Generic;
using GamesBase;
using System;
using System.Windows.Forms;
using System.IO;
using System.Windows;
using GamesList.Properties;

namespace GamesList
{
	static class Program
	{
		/// <summary>
		/// Контекст базы данных, обеспечивает связь с базой данных.
		/// </summary>
		public static GamesEntities context;
		public static string CurrentBase;
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var ok = true;
			//var m = new System.Threading.Mutex(true, "YourNameHere", out ok);//Проверяем не запущена ли ещё одна копия программы
			if (!ok)//Если запущена, то выдаём ошибку и закрываем программу.
			{
				MessageBox.Show("Другая копия программы уже запущена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null)
			{
				args = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
			}
			//MessageBox.Show(Environment.Version.Build.ToString());
			if (Settings.Default.FirstRun)//Проверяем установлен ли флаг 1-ого запуска программы в Settings-файле
			{
				var wiz = new Wizard(args);//Создаём форму Мастера настройки
				if (wiz.ShowDialog() == DialogResult.Cancel)//Открываем диалог работы с мастером и ждём его завершения
				{
					return;//Если диалог завершился отменой, выходим из программы
				}
				wiz.Dispose();//Освобождаем ресурсы диалога, т.к. он нам больше не нужен
			}
			var s = new SplashScreen("Resources/gws_landing_hero.png");
			s.Show(false, true);
			try
			{
				if (args.Length >= 1)
				{
					if (args[0] == "active")
					{
						context = new GamesEntities(Settings.Default.DefaultConStr);//Создаём новый контекст базы данных, используя подключение по умолчанию из файла настроек
						CurrentBase = Settings.Default.DefaultConStr;
					}
					else
					{
						context = new GamesEntities(args[0]);
						CurrentBase = args[0];
					}
				}
				else
				{
					context = new GamesEntities(Settings.Default.DefaultConStr);//Создаём новый контекст базы данных, используя подключение по умолчанию из файла настроек
					CurrentBase = Settings.Default.DefaultConStr;
				}
				//Database.SetInitializer(new MigrateDatabaseToLatestVersion<GamesEntities, Migrations.Configuration>());
				if (Settings.Default.NewBase)//Если стоит флаг новой базы, то
				{
					if (File.Exists(Settings.Default.DefaultConStr))//Проверяем существует ли файл с указанным именем.
					{
						try
						{
							File.Delete(Settings.Default.DefaultConStr);//Пробуем удалить файл
						}
						catch
						{
							MessageBox.Show("Не удаётся перезаписать файл! Программа перезапустится, чтобы вы могли заного выбрать файл с играми.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							//Если удалить не удаётся, выводим сообщение об ошибке
							Settings.Default.FirstRun = true;//Устанавливаем флаг первого запуска, чтобы пользователь мог повторно создать базу.
							Settings.Default.Save();//Сохраняем файл настроек
							Application.Restart();//Перезапускаем программу
							return;//Выходим из текущего экземпляра программы
						}
					}
					context.Database.Create();//Создаём базу данных. Данный метод создаст файл базы данных в указанном месте.
					context.Database.ExecuteSqlCommand(string.Format("INSERT INTO GLDBVersion (ID_V, Version) VALUES (1,{0});", Settings.Default.DatabaseVersion));
					Settings.Default.NewBase = false;
					Settings.Default.Save();
				}
				context.Database.Connection.Open();//Открываем соединение с базой
			}
			catch (Exception ex1)
			{//Если соединение с базой по умолчанию не удалось установить, то выводим следующий диалог
				var DR = MessageBox.Show(string.Format("Не удаётся подключиться к базе по умолчанию ({0}). {1} Попытаться создать новую базу?\nИли нажмите \"Нет\" чтобы открыть другой файл базы данных.", Settings.Default.DefaultConStr, ex1.Message), "Ошибка открытия", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
				//Предлагаем пользователию либо создать файл базы данны, либо открыть другую базу, или вообще выйти из программы
				switch (DR)//Перебираем результат диалога
				{
					case DialogResult.Yes://Если пользователь нажал Да
						if (File.Exists(Settings.Default.DefaultConStr))//Проверяем существует ли файл с указанным именем.
						{
							try
							{
								File.Delete(Settings.Default.DefaultConStr);//Пробуем удалить файл
							}
							catch
							{
								MessageBox.Show("Не удаётся перезаписать файл! Программа перезапустится, чтобы вы могли заного выбрать файл с играми.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
								//Если удалить не удаётся, выводим сообщение об ошибке
								Settings.Default.FirstRun = true;//Устанавливаем флаг первого запуска, чтобы пользователь мог повторно создать базу.
								Settings.Default.Save();//Сохраняем файл настроек
								Application.Restart();//Перезапускаем программу
								return;//Выходим из текущего экземпляра программы
							}
						}
						context.Database.Create();//Создаём базу данных
						context.Database.ExecuteSqlCommand(string.Format("INSERT INTO GLDBVersion (ID_V, Version) VALUES (1,{0});", Settings.Default.DatabaseVersion));
						break;
					case DialogResult.No://Если пользователь нажал Нет
						var dial = new OpenFileDialog
						{
							Title = "Выберите файл базы данных",
							FileName = "MyGames.gdb",
							Filter = "База игр (*.gdb)|*.gdb"
						};//Создаём диалог открытия файла
						if (dial.ShowDialog() == DialogResult.OK)//Показываем диалог и проверяем результат
						{
							context = new GamesEntities(dial.FileName);//Создаём контекст базы данных с выбранным файлом
							try
							{
								context.Database.Connection.Open();//Пробуем установить соединение
								CurrentBase = dial.FileName;
								if (MessageBox.Show("Вы хотите, чтобы эта база стала базой по умолчанию?", "База по умолчанию", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
								{
									Settings.Default.DefaultConStr = dial.FileName;
									Settings.Default.Save();
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("Произошла ошибка. Свяжитесь с автором.\n{0}", ex.Message));//Если соединение установить не удалось, показываем ошибку.
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
			try
			{
				if (!DBUpdater.checkDBVersion(context))
					return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Format("{0}{1}", ex.Message, ex.InnerException != null ? "\n" + ex.InnerException.Message : null),
					"Ошибка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}
			s.Close(new TimeSpan(0, 0, 0, 0, 500));
			Application.Run(new GamesForm());//Запускаем главную форму
			//GC.KeepAlive(m);//Нужно для проверки уже запущенной копии
		}

		public static List<T> CheckedItemsToList<T>(this CheckedListBox.CheckedItemCollection checkedItems)
		{
			var arr = new T[checkedItems.Count];
			var lst = new List<T>();
			lst.AddRange(arr);
			return lst;
		}

		public static string convertToVop(string ori)
		{
			return null;
		}
	}
}
