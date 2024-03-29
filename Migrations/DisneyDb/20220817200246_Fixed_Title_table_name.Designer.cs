﻿// <auto-generated />
using System;
using DisneyApi.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DisneyApi.Migrations.DisneyDb
{
    [DbContext(typeof(DisneyDbContext))]
    [Migration("20220817200246_Fixed_Title_table_name")]
    partial class Fixed_Title_table_name
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CharacterMovieSerie", b =>
                {
                    b.Property<int>("CharactersCharacter_Id")
                        .HasColumnType("int");

                    b.Property<int>("MoviesAndSeriesMovieSerie_Id")
                        .HasColumnType("int");

                    b.HasKey("CharactersCharacter_Id", "MoviesAndSeriesMovieSerie_Id");

                    b.HasIndex("MoviesAndSeriesMovieSerie_Id");

                    b.ToTable("MovieSerieCharacters", (string)null);
                });

            modelBuilder.Entity("DisneyApi.Models.Character", b =>
                {
                    b.Property<int>("Character_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Character_Id"), 1L, 1);

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("History")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Character_Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("DisneyApi.Models.Genre", b =>
                {
                    b.Property<int>("Genre_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Genre_Id"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Genre_Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("DisneyApi.Models.MovieSerie", b =>
                {
                    b.Property<int>("MovieSerie_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieSerie_Id"), 1L, 1);

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Genre_Id")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MovieSerie_Id");

                    b.HasIndex("Genre_Id");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("CharacterMovieSerie", b =>
                {
                    b.HasOne("DisneyApi.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersCharacter_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DisneyApi.Models.MovieSerie", null)
                        .WithMany()
                        .HasForeignKey("MoviesAndSeriesMovieSerie_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DisneyApi.Models.MovieSerie", b =>
                {
                    b.HasOne("DisneyApi.Models.Genre", null)
                        .WithMany("MoviesAndSeries")
                        .HasForeignKey("Genre_Id");
                });

            modelBuilder.Entity("DisneyApi.Models.Genre", b =>
                {
                    b.Navigation("MoviesAndSeries");
                });
#pragma warning restore 612, 618
        }
    }
}
