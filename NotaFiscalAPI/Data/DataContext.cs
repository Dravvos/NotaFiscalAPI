using CRUD_nota_fiscal.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_nota_fiscal.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<NotaFiscal> NotaFiscal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotaFiscal>().HasKey(nf => new { nf.NotaFiscalId });

        }
    }
}
