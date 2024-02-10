﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movie.PoC.Api.Database;

#nullable disable

namespace Movie.PoC.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240210233538_InitialCreate2")]
    partial class InitialCreate2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Movie.PoC.Api.Entities.FilmDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Metascore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Production")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rated")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Released")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("Runtime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("imdbID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("imdbRating")
                        .HasColumnType("REAL");

                    b.Property<int>("imdbVotes")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("FilmDatas");
                });

            modelBuilder.Entity("Movie.PoC.Api.Entities.FilmModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FilmDataId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FilmDataId")
                        .IsUnique();

                    b.ToTable("Films");
                });

            modelBuilder.Entity("Movie.PoC.Api.Entities.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Movie.PoC.Api.Entities.FilmModel", b =>
                {
                    b.HasOne("Movie.PoC.Api.Entities.FilmDataModel", "AssociatedFilmData")
                        .WithOne("AssociatedFilm")
                        .HasForeignKey("Movie.PoC.Api.Entities.FilmModel", "FilmDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssociatedFilmData");
                });

            modelBuilder.Entity("Movie.PoC.Api.Entities.FilmDataModel", b =>
                {
                    b.Navigation("AssociatedFilm")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
