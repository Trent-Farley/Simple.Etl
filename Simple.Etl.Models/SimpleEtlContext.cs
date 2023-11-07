using Microsoft.EntityFrameworkCore;
using Simple.Etl.Models.Entities;

namespace Simple.Etl.Models
{
    public class SimpleEtlContext : DbContext
    {
        public string DbPath { get; }
        public SimpleEtlContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "SimpleEtl.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<Log> Logs { get; set; }
    }
}
