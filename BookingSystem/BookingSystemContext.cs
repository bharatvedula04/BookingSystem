using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookingSystem
{
    public class BookingSystemContext : DbContext
    {
        public string DbPath { get; }
        public BookingSystemContext() 
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "BookingSystemV1.db");
            this.ChangeTracker.StateChanged += UpdateBaseEntity;
            this.ChangeTracker.Tracked += UpdateBaseEntity;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={DbPath}");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<BookingSystemModel>()
                  .HasIndex(entity => new { entity.BookingSlots }).IsUnique();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        public DbSet<BookingSystemModel> Booking { get; set; }

        private void UpdateBaseEntity(object sender, EntityEntryEventArgs e)
        {
            string userName = Environment.UserName;
            if (e.Entry.Entity is BaseEntity baseEntity)
            {
                if (e.Entry.State == EntityState.Added)
                {
                    // Only set values if not set manually
                    baseEntity.CreatedBy ??= userName;
                    baseEntity.DateModified ??= DateTime.UtcNow;
                    baseEntity.ModifiedBy ??= userName;
                }
                else if (e.Entry.State == EntityState.Modified)
                {
                    // Only change date modified if not manually set
                    if (baseEntity.DateModified == e.Entry.OriginalValues
                .GetValue<DateTime?>(nameof(BaseEntity.DateModified)))
                    {
                        baseEntity.DateModified = DateTime.UtcNow;
                    }

                    // Only change modified by if not manually set
                    if (baseEntity.ModifiedBy == e.Entry.OriginalValues
                .GetValue<string>(nameof(BaseEntity.ModifiedBy)))
                    {
                        baseEntity.ModifiedBy = userName;
                    }
                }
                else
                {
                    // Do nothing
                }
            }
        }
    }


}
