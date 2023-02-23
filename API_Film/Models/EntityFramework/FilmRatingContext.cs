using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Runtime.Intrinsics.X86;

namespace API_Film.Models.EntityFramework
{
    public partial class FilmRatingContext : DbContext
    {
        public FilmRatingContext()
        {
        }

        public FilmRatingContext(DbContextOptions<FilmRatingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Notation> Notations { get; set; }

        public virtual DbSet<Film> Films { get; set; }

        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=FilmRating; uid=postgres; password=postgres;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notation>(entity =>
            {
                entity.HasCheckConstraint("ck_not_note", "not_note between 0 and 5");
                entity.HasKey(e => new { e.FilmId, e.UtilisateurId }).HasName("pk_notation");

                entity.HasOne(d => d.FilmNavigation).WithMany(p => p.NotationFilm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_notation_film");

                entity.HasOne(d => d.UtilisateurNaviguation).WithMany(p => p.NotationUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_notation_utilisateur");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.Filmid).HasName("pk_film");

            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.UtilisateurId).HasName("pk_utilisateur");
                entity.Property(e => e.Pays).HasDefaultValue("France");
                entity.Property(e => e.DateCreation).HasDefaultValueSql("now()");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
