using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SupportAnalyst.Model;

namespace SupportAnalyst.Data
{
    public class ESamplesDbContext : DbContext
    {
        public DbSet<LogEntry> LogEntries { get; set; }

        public static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.AppSettings["eSamplesConnectionStringName"] != null)
                {
                    return ConfigurationManager.AppSettings["eSamplesConnectionStringName"].ToString();
                }
                return "ESamples.Development";
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LogEntry>().ToTable("ESamplesLog");            
            modelBuilder.Entity<LogEntry>().Property(c => c.TimeStamp).HasColumnName("Date");
            modelBuilder.Entity<LogEntry>().Property(c => c.LogType).HasColumnName("Level");
            modelBuilder.Entity<LogEntry>().Property(c => c.Logger).IsOptional();   
        }

        public ESamplesDbContext()
            : base(ConnectionStringName)
        {
        }
    }
}
