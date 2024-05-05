﻿// <auto-generated />
using System;
using Book_Movie_Tickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Book_Movie_Tickets.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Book_Movie_Tickets.Models.Bookings", b =>
                {
                    b.Property<int>("booking_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("booking_id"));

                    b.Property<int>("customer_id")
                        .HasColumnType("int");

                    b.Property<int>("number_of_tickets")
                        .HasColumnType("int");

                    b.Property<int>("screening_id")
                        .HasColumnType("int");

                    b.HasKey("booking_id");

                    b.HasIndex("customer_id");

                    b.HasIndex("screening_id");

                    b.ToTable("bookings");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Customers", b =>
                {
                    b.Property<int>("customer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customer_id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customer_id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Movies", b =>
                {
                    b.Property<int>("movies_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("movies_id"));

                    b.Property<string>("Duration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("release_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("movies_id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Screenings", b =>
                {
                    b.Property<int>("screening_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("screening_id"));

                    b.Property<DateTime>("end_time")
                        .HasColumnType("datetime2");

                    b.Property<int>("movie_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("start_time")
                        .HasColumnType("datetime2");

                    b.Property<int>("theater_id")
                        .HasColumnType("int");

                    b.HasKey("screening_id");

                    b.HasIndex("movie_id");

                    b.HasIndex("theater_id");

                    b.ToTable("Screenings");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Theaters", b =>
                {
                    b.Property<int>("theater_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("theater_id"));

                    b.Property<int>("capacity")
                        .HasColumnType("int");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("theater_id");

                    b.ToTable("Theaters");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Bookings", b =>
                {
                    b.HasOne("Book_Movie_Tickets.Models.Customers", "Customer")
                        .WithMany("bookings")
                        .HasForeignKey("customer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Book_Movie_Tickets.Models.Screenings", "Screening")
                        .WithMany("bookings")
                        .HasForeignKey("screening_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Screening");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Screenings", b =>
                {
                    b.HasOne("Book_Movie_Tickets.Models.Movies", "Movie")
                        .WithMany("screenings")
                        .HasForeignKey("movie_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Book_Movie_Tickets.Models.Theaters", "Theater")
                        .WithMany("screenings")
                        .HasForeignKey("theater_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Customers", b =>
                {
                    b.Navigation("bookings");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Movies", b =>
                {
                    b.Navigation("screenings");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Screenings", b =>
                {
                    b.Navigation("bookings");
                });

            modelBuilder.Entity("Book_Movie_Tickets.Models.Theaters", b =>
                {
                    b.Navigation("screenings");
                });
#pragma warning restore 612, 618
        }
    }
}
