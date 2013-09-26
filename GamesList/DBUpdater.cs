using System;
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
            while (dd != 4)
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
                    }
                }
                catch (Exception ex)
                {
                   MessageBox.Show("Данная база данных несовместима.","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                   return false;
                }
            }
            return true;
        }

        public static void patch1to4()
        {
            try
            {
                Program.context.Database.ExecuteSqlCommand("ALTER TABLE Game_disks ALTER COLUMN Kol_vo float;"+
                    "ALTER TABLE Disk_types ALTER COLUMN Max_Size float;"+
                    "ALTER TABLE Games ALTER COLUMN Description Nvarchar(2000);");
                Program.context.Database.ExecuteSqlCommand("UPDATE GLDBVersion SET Version = 2;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }
    }
}
