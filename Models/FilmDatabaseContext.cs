using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace streaming_video_user.Models
{
    public partial class FilmDatabaseContext : DbContext
    {
        public FilmDatabaseContext()
        {
        }

        public FilmDatabaseContext(DbContextOptions<FilmDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; } = null!;
        public virtual DbSet<ActorFilm> ActorFilms { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Director> Directors { get; set; } = null!;
        public virtual DbSet<DiretorFilm> DiretorFilms { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Gerne> Gernes { get; set; } = null!;
        public virtual DbSet<GerneFilm> GerneFilms { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<LikeFilm> LikeFilms { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserSecurity> UserSecurities { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-FLFGLMO\\SQLEXPRESS;Initial Catalog=FilmDatabase;Integrated Security=True;");
                optionsBuilder.UseSqlServer("Data Source=Desktop-p6811ab; Initial Catalog = FilmDatabase; Integrated Security = True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.IdActor)
                    .IsClustered(false);

                entity.ToTable("ACTOR");

                entity.Property(e => e.IdActor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_ACTOR")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.NameActor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME_ACTOR");

                entity.Property(e => e.StatusDelete).HasColumnName("Status_Delete");

                entity.Property(e => e.UrlImg)
                    .HasColumnType("text")
                    .HasColumnName("URL_IMG");
            });

            modelBuilder.Entity<ActorFilm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACTOR_FILM");

                entity.HasIndex(e => e.IdActor, "RELATIONSHIP_5_FK");

                entity.HasIndex(e => e.IdFilm, "RELATIONSHIP_6_FK");

                entity.Property(e => e.IdActor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_ACTOR")
                    .IsFixedLength();

                entity.Property(e => e.IdFilm)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_FILM")
                    .IsFixedLength();

                entity.HasOne(d => d.IdActorNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdActor)
                    .HasConstraintName("FK_ACTOR_FI_RELATIONS_ACTOR");

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdFilm)
                    .HasConstraintName("FK_ACTOR_FI_RELATIONS_FILM");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                    .IsClustered(false);

                entity.ToTable("ADMIN");

                entity.Property(e => e.IdAdmin)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ID_ADMIN")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD")
                    .IsFixedLength();

                entity.Property(e => e.StatusDelete).HasColumnName("Status_Delete");
            });

            modelBuilder.Entity<Director>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("DIRECTOR");

                entity.Property(e => e.Id)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.StatusDelete).HasColumnName("Status_Delete");

                entity.Property(e => e.UrlImg)
                    .HasColumnType("text")
                    .HasColumnName("URL_IMG");
            });

            modelBuilder.Entity<DiretorFilm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DIRETOR_FILM");

                entity.HasIndex(e => e.IdFilm, "RELATIONSHIP_3_FK");

                entity.HasIndex(e => e.Id, "RELATIONSHIP_4_FK");

                entity.Property(e => e.Id)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ID")
                    .IsFixedLength();

                entity.Property(e => e.IdFilm)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_FILM")
                    .IsFixedLength();

                entity.HasOne(d => d.IdNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK_DIRETOR__RELATIONS_DIRECTOR");

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdFilm)
                    .HasConstraintName("FK_DIRETOR__RELATIONS_FILM");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.IdFilm)
                    .IsClustered(false);

                entity.ToTable("FILM");

                entity.HasIndex(e => e.IdAdmin, "RELATIONSHIP_8_FK");

                entity.Property(e => e.IdFilm)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_FILM")
                    .IsFixedLength();

                entity.Property(e => e.AgeLimit)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("AGE_LIMIT")
                    .IsFixedLength();

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.IdAdmin)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ID_ADMIN")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.StatusDelete).HasColumnName("Status_Delete");

                entity.Property(e => e.UrlFilm)
                    .HasColumnType("text")
                    .HasColumnName("URL_FILM");

                entity.Property(e => e.UrlImg)
                    .HasColumnType("text")
                    .HasColumnName("URL_IMG");

                entity.Property(e => e.YearPublic)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("YEAR_PUBLIC")
                    .IsFixedLength();

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.IdAdmin)
                    .HasConstraintName("FK_FILM_RELATIONS_ADMIN");
            });

            modelBuilder.Entity<Gerne>(entity =>
            {
                entity.HasKey(e => e.IdGer)
                    .IsClustered(false);

                entity.ToTable("GERNE");

                entity.Property(e => e.IdGer)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_GER")
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.StatusDelete).HasColumnName("Status_Delete");
            });

            modelBuilder.Entity<GerneFilm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GERNE_FILM");

                entity.HasIndex(e => e.IdGer, "RELATIONSHIP_1_FK");

                entity.HasIndex(e => e.IdFilm, "RELATIONSHIP_2_FK");

                entity.Property(e => e.IdFilm)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_FILM")
                    .IsFixedLength();

                entity.Property(e => e.IdGer)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_GER")
                    .IsFixedLength();

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdFilm)
                    .HasConstraintName("FK_GERNE_FI_RELATIONS_FILM");

                entity.HasOne(d => d.IdGerNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdGer)
                    .HasConstraintName("FK_GERNE_FI_RELATIONS_GERNE");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("HISTORY");

                entity.HasIndex(e => e.IdFilm, "RELATIONSHIP_10_FK");

                entity.HasIndex(e => e.IdUser, "RELATIONSHIP_9_FK");

                entity.Property(e => e.IdFilm)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_FILM")
                    .IsFixedLength();

                entity.Property(e => e.IdUser)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_USER")
                    .IsFixedLength();

                entity.Property(e => e.Time).HasColumnName("TIME");

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdFilm)
                    .HasConstraintName("FK_HISTORY_RELATIONS_FILM");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_HISTORY_RELATIONS_USER");
            });

            modelBuilder.Entity<LikeFilm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LIKE_FILM");

                entity.HasIndex(e => e.IdUser, "RELATIONSHIP_11_FK");

                entity.HasIndex(e => e.IdFilm, "RELATIONSHIP_12_FK");

                entity.Property(e => e.IdFilm)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_FILM")
                    .IsFixedLength();

                entity.Property(e => e.IdUser)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_USER")
                    .IsFixedLength();

                entity.HasOne(d => d.IdFilmNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdFilm)
                    .HasConstraintName("FK_LIKE_FIL_RELATIONS_FILM");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_LIKE_FIL_RELATIONS_USER");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .IsClustered(false);

                entity.ToTable("USER");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_USER")
                    .IsFixedLength();

                entity.Property(e => e.Age).HasColumnName("AGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.StatusDelete).HasColumnName("Status_Delete");

                entity.Property(e => e.UrlImg)
                    .HasColumnType("text")
                    .HasColumnName("URL_IMG");
            });

            modelBuilder.Entity<UserSecurity>(entity =>
            {
                entity.HasKey(e=>e.IdUser).IsClustered(false);

                entity.ToTable("USER_SECURITY");

                entity.HasIndex(e => e.IdUser, "RELATIONSHIP_7_FK");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.IdUser)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ID_USER")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD")
                    .IsFixedLength();

                entity.Property(e => e.StatusDelete).HasColumnName("Status_Delete");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_SEC_RELATIONS_USER");
            });

            OnModelCreatingPartial(modelBuilder);
         
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
