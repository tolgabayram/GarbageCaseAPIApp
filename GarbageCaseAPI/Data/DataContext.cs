using GarbageCaseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GarbageCaseAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<WasteRecord> WasteRecords { get; set; }
        public DbSet<WasteType> WasteTypes{ get; set; }

        public DbSet<Store> Stores { get; set; }
    }
}
