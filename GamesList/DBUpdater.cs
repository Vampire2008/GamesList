using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GamesList.Model;

namespace GamesList
{
	public static class DBUpdater
	{
		public static bool checkDBVersion(GamesEntities ctx)
		{
			decimal dd = 0;
			while (dd != 5)
			{
				try
				{
					var q = ctx.Database.SqlQuery<Decimal>("select Version from GLDBVersion").First();
					dd = q;
					switch (Decimal.ToInt32(q))
					{
						case 1:
							patch1to4(ctx);
							dd = 4;
							break;
						case 4:
							patch4to5(ctx);
							dd = 5;
							break;
						case 5:
							return true;
						default:
							MessageBox.Show("Неизвестная версия базы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return false;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Данная база данных несовместима.\n"+ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}
			return true;
		}

		public static void patch1to4(GamesEntities ctx)
		{
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Game_disks ALTER COLUMN Kol_vo float;");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Disk_types ALTER COLUMN Max_Size float;");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Games ALTER COLUMN Description Nvarchar(2000);");
			ctx.Database.ExecuteSqlCommand("UPDATE GLDBVersion SET Version = 4;");
		}

		public static void patch4to5(GamesEntities ctx)
		{
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Developers ADD Description NVARCHAR(2000);");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Publishers ADD Description NVARCHAR(2000);");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE RF_Distributors ADD Description NVARCHAR(2000);");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Platforms ALTER COLUMN Description NVARCHAR(2000);");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Series ALTER COLUMN Description NVARCHAR(2000);");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Online_protections ALTER COLUMN Description NVARCHAR(2000);");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Disk_types ALTER COLUMN Description NVARCHAR(2000);");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Games DROP COLUMN WherePhoto;");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Games ADD WherePhoto Image;");
			ctx.Database.ExecuteSqlCommand("DROP TABLE GLDBVersion;");
			ctx.Database.ExecuteSqlCommand("CREATE TABLE GLDBVersion (ID_V numeric(18,0) PRIMARY KEY,Version numeric(18,0));");
			ctx.Database.ExecuteSqlCommand("INSERT INTO GLDBVersion (ID_V, Version) VALUES (1,5);");
		}
	}
}
