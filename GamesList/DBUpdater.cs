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
            var q = Program.context.Database.SqlQuery(typeof(decimal),"select Version from GLDBVersion");
            foreach (decimal d in q)
            {
                if (d == 1)
                    patch1to2();
                    return true;
            }
            return false;
        }

        public static void patch1to2()
        {
            try
            {
                Program.context.Database.ExecuteSqlCommand("ALTER TABLE Games ADD Temp NVARCHAR(100) NULL;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Program.context.Database.ExecuteSqlCommand("UPDATE GLDBVersion SET Version = 2;");
        }
    }
}
