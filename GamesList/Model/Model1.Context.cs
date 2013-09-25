﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GamesList.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    
    public partial class GamesEntities : DbContext
    {
        public GamesEntities()
            : base("name=GamesEntities")
        {
        }

        public GamesEntities(string ConStr)
            : base(ConStr)
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Boxes> Boxes { get; set; }
        public DbSet<Developers> Developers { get; set; }
        public DbSet<Disk_types> Disk_types { get; set; }
        public DbSet<Editions> Editions { get; set; }
        public DbSet<Game_disks> Game_disks { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Online_protections> Online_protections { get; set; }
        public DbSet<Platforms> Platforms { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<RF_Distributors> RF_Distributors { get; set; }
        public DbSet<Series> Series { get; set; }

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
    }
}
