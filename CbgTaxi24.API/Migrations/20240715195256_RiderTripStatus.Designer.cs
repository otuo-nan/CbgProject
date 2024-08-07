﻿// <auto-generated />
using System;
using CbgTaxi24.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CbgTaxi24.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240715195256_RiderTripStatus")]
    partial class RiderTripStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CbgTaxi24.API.Models.Driver", b =>
                {
                    b.Property<Guid>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CarNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Rating")
                        .HasColumnType("tinyint");

                    b.Property<byte>("ServiceType")
                        .HasColumnType("tinyint");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("DriverId");

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Invoice", b =>
                {
                    b.Property<Guid>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(9, 2)");

                    b.Property<Guid>("RiderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("TripId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("InvoiceId");

                    b.HasIndex("RiderId");

                    b.HasIndex("TripId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(8, 6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Rider", b =>
                {
                    b.Property<Guid>("RiderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsInTrip")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OtherNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("RiderId");

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.ToTable("Riders");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Trip", b =>
                {
                    b.Property<Guid>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("FromLat")
                        .HasColumnType("decimal(8, 6)");

                    b.Property<decimal>("FromLong")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<string>("Metadata")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(9, 2)");

                    b.Property<Guid>("RiderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("bit");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<decimal>("ToLat")
                        .HasColumnType("decimal(8, 6)");

                    b.Property<decimal>("ToLong")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("TripId");

                    b.HasIndex("DriverId");

                    b.HasIndex("RiderId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Driver", b =>
                {
                    b.HasOne("CbgTaxi24.API.Models.Location", "Location")
                        .WithOne()
                        .HasForeignKey("CbgTaxi24.API.Models.Driver", "LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Invoice", b =>
                {
                    b.HasOne("CbgTaxi24.API.Models.Rider", "Rider")
                        .WithMany("Invoices")
                        .HasForeignKey("RiderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CbgTaxi24.API.Models.Trip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rider");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Rider", b =>
                {
                    b.HasOne("CbgTaxi24.API.Models.Location", "Location")
                        .WithOne()
                        .HasForeignKey("CbgTaxi24.API.Models.Rider", "LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Trip", b =>
                {
                    b.HasOne("CbgTaxi24.API.Models.Driver", "Driver")
                        .WithMany("Trips")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CbgTaxi24.API.Models.Rider", "Rider")
                        .WithMany("Trips")
                        .HasForeignKey("RiderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Rider");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Driver", b =>
                {
                    b.Navigation("Trips");
                });

            modelBuilder.Entity("CbgTaxi24.API.Models.Rider", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
