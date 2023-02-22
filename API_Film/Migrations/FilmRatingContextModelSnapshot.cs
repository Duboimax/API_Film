﻿// <auto-generated />
using System;
using API_Film.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_Film.Migrations
{
    [DbContext(typeof(FilmRatingContext))]
    partial class FilmRatingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API_Film.Models.EntityFramework.Film", b =>
                {
                    b.Property<int>("Filmid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("flm_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Filmid"));

                    b.Property<DateTime?>("DateSortie")
                        .HasColumnType("date")
                        .HasColumnName("flm_datesortie");

                    b.Property<decimal?>("Duree")
                        .HasColumnType("numeric")
                        .HasColumnName("flm_duree");

                    b.Property<string>("Genre")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("flm_genre");

                    b.Property<string>("Resume")
                        .HasColumnType("text")
                        .HasColumnName("flm_resume");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("flm_titre");

                    b.HasKey("Filmid")
                        .HasName("pk_film");

                    b.ToTable("t_e_film_flm");
                });

            modelBuilder.Entity("API_Film.Models.EntityFramework.Notation", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("integer")
                        .HasColumnName("flm_id");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("integer")
                        .HasColumnName("utl_id");

                    b.Property<int>("Note")
                        .HasColumnType("integer")
                        .HasColumnName("not_note");

                    b.HasKey("FilmId", "UtilisateurId")
                        .HasName("pk_notation");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("t_j_notation_not");

                    b.HasCheckConstraint("ck_not_note", "not_note between 0 and 5");
                });

            modelBuilder.Entity("API_Film.Models.EntityFramework.Utilisateur", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("utl_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UtilisateurId"));

                    b.Property<string>("CodePostal")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("utl_cp");

                    b.Property<DateTime>("DateCreation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("utl_datecreation")
                        .HasDefaultValueSql("now()");

                    b.Property<float?>("Latitude")
                        .HasColumnType("real")
                        .HasColumnName("utl_latitude");

                    b.Property<float?>("Longitude")
                        .HasColumnType("real")
                        .HasColumnName("utl_longitude");

                    b.Property<string>("Mail")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("utl_mail");

                    b.Property<string>("Mobile")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("utl_mobile");

                    b.Property<string>("Nom")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("utl_nom");

                    b.Property<string>("Pays")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("France")
                        .HasColumnName("utl_pays");

                    b.Property<string>("Prenom")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("utl_prenom");

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("utl_pwd");

                    b.Property<string>("Rue")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("utl_rue");

                    b.Property<string>("Ville")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("utl_ville");

                    b.HasKey("UtilisateurId")
                        .HasName("pk_utilisateur");

                    b.HasIndex("Mail")
                        .IsUnique();

                    b.ToTable("t_e_utilisateur_utl");
                });

            modelBuilder.Entity("API_Film.Models.EntityFramework.Notation", b =>
                {
                    b.HasOne("API_Film.Models.EntityFramework.Film", "FilmNavigation")
                        .WithMany("NotationFilm")
                        .HasForeignKey("FilmId")
                        .IsRequired()
                        .HasConstraintName("fk_notation_film");

                    b.HasOne("API_Film.Models.EntityFramework.Utilisateur", "UtilisateurNaviguation")
                        .WithMany("NotationUtilisateur")
                        .HasForeignKey("UtilisateurId")
                        .IsRequired()
                        .HasConstraintName("fk_notation_utilisateur");

                    b.Navigation("FilmNavigation");

                    b.Navigation("UtilisateurNaviguation");
                });

            modelBuilder.Entity("API_Film.Models.EntityFramework.Film", b =>
                {
                    b.Navigation("NotationFilm");
                });

            modelBuilder.Entity("API_Film.Models.EntityFramework.Utilisateur", b =>
                {
                    b.Navigation("NotationUtilisateur");
                });
#pragma warning restore 612, 618
        }
    }
}
