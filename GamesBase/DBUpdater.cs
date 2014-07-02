using System;
using System.Linq;

namespace GamesBase
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
							patch5to6(ctx);
							dd = 6;
							break;
						case 6:
							return true;
						default:
							throw new Exception("Неизвестная версия базы.");
					}
				}
				catch (Exception ex)
				{
					throw new Exception("Данная база данных несовместима.", ex);
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

		public static void patch5to6(GamesEntities ctx)
		{
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Games ALTER COLUMN ID_Edition numeric(18,0) NULL;");
			ctx.Database.ExecuteSqlCommand("ALTER TABLE Games ALTER COLUMN ID_Box numeric(18,0) NULL;");
			ctx.Database.ExecuteSqlCommand("UPDATE GLDBVersion SET Version = 6;");
		}
	}
}
