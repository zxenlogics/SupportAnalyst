using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using SupportAnalyst.Model;

namespace SupportAnalyst.Repository
{
    public class PACDbContext : DbContext
    {
                public DbSet<LogEntry> LogEntries { get; set; }

        public static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.AppSettings["pacConnectionStringName"] != null)
                {
                    return ConfigurationManager.AppSettings["pacConnectionStringName"].ToString();
                }
                return "Pac.Development";
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LogEntry>().ToTable("Log");
            modelBuilder.Entity<LogEntry>().Property(c => c.Id).HasColumnName("LogId");
            modelBuilder.Entity<LogEntry>().Property(c => c.LogType).HasColumnName("Severity");
            modelBuilder.Entity<LogEntry>().Property(c => c.Logger).IsOptional();            
        }

        public PACDbContext()
            :base(ConnectionStringName)
        {}

    }
}
