﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movies.Api.Infrastructure.DbContexts;

#nullable disable

namespace Movies.Api.Migrations
{
    [DbContext(typeof(MoviesContext))]
    [Migration("20220621210553_AddedDetailsTable")]
    partial class AddedDetailsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Movies.Api.Infrastructure.Entities.FakeDbMovieDetailsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FakeDbMovieEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FakeDbMovieId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImdbID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FakeDbMovieEntityId");

                    b.ToTable("FakeDbMovieDetailsEntity");
                });

            modelBuilder.Entity("Movies.Api.Infrastructure.Entities.FakeDbMovieEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("MoviesFromFakeDb");
                });

            modelBuilder.Entity("Movies.Api.Infrastructure.Entities.OmDbMovieEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MoviesFromOmDb");
                });

            modelBuilder.Entity("Movies.Api.Infrastructure.Entities.FakeDbMovieDetailsEntity", b =>
                {
                    b.HasOne("Movies.Api.Infrastructure.Entities.FakeDbMovieEntity", null)
                        .WithMany("MovieDetails")
                        .HasForeignKey("FakeDbMovieEntityId");
                });

            modelBuilder.Entity("Movies.Api.Infrastructure.Entities.FakeDbMovieEntity", b =>
                {
                    b.Navigation("MovieDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
