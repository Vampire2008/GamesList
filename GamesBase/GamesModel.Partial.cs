using System;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlServerCe;

namespace GamesBase
{
	partial class GamesEntities
	{
		public GamesEntities(string constr)
			: base(BuidConStr(constr))
		{
		}

		public void RejectChanges(object entity)
		{
			ObjectStateEntry stateEntry;
			if (!(this as IObjectContextAdapter).ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry))
				return;

			for (var t = 0; t < stateEntry.OriginalValues.FieldCount; t++)
			{
				try
				{
					stateEntry.CurrentValues.SetValue(t, stateEntry.OriginalValues[t]);
				}
				catch (InvalidOperationException)
				{
					// a workaround to skip primary key assignment, which is not allowed
				}
			}
		}

		/// <summary>
		/// Создаёт строку подключения формата Entity Framework.
		/// </summary>
		/// <param name="path">Путь к базе данных SQL Server CE</param>
		/// <returns>Строка подключения</returns>
		public static string BuidConStr(string path)
		{
			var ecsBuilder = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["GamesEntities"].ConnectionString);
			var sqlCeCsBuilder = new SqlCeConnectionStringBuilder(ecsBuilder.ProviderConnectionString)
			{
				DataSource = path,
				MaxDatabaseSize = 4091
			};
			ecsBuilder.ProviderConnectionString = sqlCeCsBuilder.ToString();
			return ecsBuilder.ToString();//Возвращаем готовую строку подключения
		}
	}
}
