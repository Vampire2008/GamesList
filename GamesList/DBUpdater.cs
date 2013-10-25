﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesList
{
    public static class DBUpdater
    {
        public static bool checkDBVersion()
        {
            decimal dd = 0;
            while (dd != 5)
            {
                try
                {
                    var q = Program.context.Database.SqlQuery(typeof(decimal), "select Version from GLDBVersion");
                    foreach (decimal d in q)
                    {
                        dd = d;
                        if (d == 1)
                        {
                            patch1to4();
                            dd = 4;
                        }
						if (d == 4)
						{
							patch4to5();
							dd = 5;
						}
                    }
                }
                catch 
                {
                   MessageBox.Show("Данная база данных несовместима.","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                   return true;
                }
            }
            return true;
        }

        public static void patch1to4()
        {
                Program.context.Database.ExecuteSqlCommand("ALTER TABLE Game_disks ALTER COLUMN Kol_vo float;");
                Program.context.Database.ExecuteSqlCommand("ALTER TABLE Disk_types ALTER COLUMN Max_Size float;");
                Program.context.Database.ExecuteSqlCommand("ALTER TABLE Games ALTER COLUMN Description Nvarchar(2000);");
                Program.context.Database.ExecuteSqlCommand("UPDATE GLDBVersion SET Version = 4;");         
        }

		public static void patch4to5()
		{
			Program.context.Database.ExecuteSqlCommand("ALTER TABLE Developers ADD Description NVARCHAR(2000);");
			Program.context.Database.ExecuteSqlCommand("ALTER TABLE Publishers ADD Description NVARCHAR(2000);");
			Program.context.Database.ExecuteSqlCommand("ALTER TABLE RF_Distributors ADD Description NVARCHAR(2000);");
			Program.context.Database.ExecuteSqlCommand("ALTER TABLE Platforms ALTER COLUMN Description NVARCHAR(2000);");
			Program.context.Database.ExecuteSqlCommand("ALTER TABLE Series ALTER COLUMN Description NVARCHAR(2000);");
			Program.context.Database.ExecuteSqlCommand("ALTER TABLE Online_protections ALTER COLUMN Description NVARCHAR(2000);");
			Program.context.Database.ExecuteSqlCommand("ALTER TABLE Disk_types ALTER COLUMN Description NVARCHAR(2000);");
			Program.context.Database.ExecuteSqlCommand("ALTER TABLE Games DROP COLUMN WherePhoto;");
			Program.context.Database.ExecuteSqlCommand("ALTER TABLE Games ADD WherePhoto Image;");
			Program.context.Database.ExecuteSqlCommand("DROP TABLE GLDBVersion;");
			Program.context.Database.ExecuteSqlCommand("CREATE TABLE GLDBVersion (ID_V numeric(18,0) PRIMARY KEY,Version numeric(18,0));");
			Program.context.Database.ExecuteSqlCommand("INSERT INTO GLDBVersion (ID_V, Version) VALUES (1,5);");
		}
    }
}
