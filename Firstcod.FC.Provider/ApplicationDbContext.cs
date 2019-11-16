using Firstcod.FC.Provider.Models;
using Microsoft.EntityFrameworkCore;

namespace Firstcod.FC.Provider
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Transaction> Transactions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Transaction>().ToTable($"App{nameof(this.Transactions)}");
            builder.Entity<Transaction>(entity => { entity.HasIndex(s => s.TransactionHash).IsUnique(); });
        }
    }
}
