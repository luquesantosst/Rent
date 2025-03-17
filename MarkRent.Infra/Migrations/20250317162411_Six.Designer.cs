﻿// <auto-generated />
using System;
using MarkRent.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarkRent.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250317162411_Six")]
    partial class Six
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarkRent.Domain.Entities.DeliveryAgent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CNH_Image")
                        .HasColumnType("text");

                    b.Property<string>("CNH_Number")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<string>("CNH_Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("DeliveryAgents");
                });

            modelBuilder.Entity("MarkRent.Domain.Entities.FutureEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("FutureEvents");
                });

            modelBuilder.Entity("MarkRent.Domain.Entities.Hire", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DeliverAgentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DevolutionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EstimatedEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Plan")
                        .HasColumnType("integer");

                    b.Property<double?>("PricePerDay")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DeliverAgentId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Hires");
                });

            modelBuilder.Entity("MarkRent.Domain.Entities.PriceDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("PriceDays");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b3b9fd99-48a1-4f45-a60d-a3f9cd05d853"),
                            Day = 7,
                            Price = 30.0
                        },
                        new
                        {
                            Id = new Guid("0ce9493c-90e6-44e6-8e26-acf789b3a3c5"),
                            Day = 15,
                            Price = 28.0
                        },
                        new
                        {
                            Id = new Guid("c30607bf-848b-49bf-bb8f-ef1582a0070b"),
                            Day = 30,
                            Price = 22.0
                        },
                        new
                        {
                            Id = new Guid("2ef114de-926b-42b4-bd45-efdb5c7d34fe"),
                            Day = 45,
                            Price = 20.0
                        },
                        new
                        {
                            Id = new Guid("ec57581b-9442-4dd5-b05c-6b7f67155dfb"),
                            Day = 50,
                            Price = 18.0
                        });
                });

            modelBuilder.Entity("MarkRent.Domain.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FutureEventId")
                        .HasColumnType("uuid");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<int>("Year")
                        .HasMaxLength(4)
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("MarkRent.Domain.Entities.FutureEvent", b =>
                {
                    b.HasOne("MarkRent.Domain.Entities.Vehicle", "Vehicle")
                        .WithOne("FutureEvent")
                        .HasForeignKey("MarkRent.Domain.Entities.FutureEvent", "VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("MarkRent.Domain.Entities.Hire", b =>
                {
                    b.HasOne("MarkRent.Domain.Entities.DeliveryAgent", "DeliveryAgent")
                        .WithMany()
                        .HasForeignKey("DeliverAgentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MarkRent.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DeliveryAgent");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("MarkRent.Domain.Entities.Vehicle", b =>
                {
                    b.Navigation("FutureEvent")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
