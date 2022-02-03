using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Pepper.Models
{
    public partial class PepperDBContext : DbContext
    {
        public PepperDBContext()
        {
        }

        public PepperDBContext(DbContextOptions<PepperDBContext> options)
            : base(options)
        {
        }

        public DbSet<Promocje> Promocje { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UM3FNKK\\SQLEXPRESS;Database=PepperDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Promocje>(entity =>
            {
                entity.HasKey(e => e.Idpromocji);

                entity.ToTable("Promocje");

                entity.Property(e => e.Idpromocji)
                    .HasColumnName("IDpromocji");

                entity.Property(e => e.DataDodania)
                    //.HasColumnType("datetime")
                    .HasColumnName("Data_dodania");

                entity.Property(e => e.Link).IsRequired();

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Opis).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
