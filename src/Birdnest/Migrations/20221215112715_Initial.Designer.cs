﻿// <auto-generated />
using Birdnest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Birdnest.Migrations
{
    [DbContext(typeof(BirdnestContext))]
    [Migration("20221215112715_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Birdnest.Models.Pilot", b =>
                {
                    b.Property<string>("PilotID")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("PilotID");

                    b.ToTable("Pilots");
                });

            modelBuilder.Entity("Birdnest.Models.Sensor", b =>
                {
                    b.Property<int>("SensorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SensorID"));

                    b.Property<float>("DetectionDistance")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("SensorLocationX")
                        .HasColumnType("real");

                    b.Property<float>("SensorLocationY")
                        .HasColumnType("real");

                    b.HasKey("SensorID");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("Birdnest.Models.Violation", b =>
                {
                    b.Property<int>("ViolationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ViolationID"));

                    b.Property<int>("Distance")
                        .HasColumnType("integer");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("PilotID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("ViolationLocationX")
                        .HasColumnType("real");

                    b.Property<float>("ViolationLocationY")
                        .HasColumnType("real");

                    b.HasKey("ViolationID");

                    b.HasIndex("PilotID")
                        .IsUnique();

                    b.ToTable("Violations");
                });

            modelBuilder.Entity("Birdnest.Models.Violation", b =>
                {
                    b.HasOne("Birdnest.Models.Pilot", "Pilot")
                        .WithOne("Violations")
                        .HasForeignKey("Birdnest.Models.Violation", "PilotID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pilot");
                });

            modelBuilder.Entity("Birdnest.Models.Pilot", b =>
                {
                    b.Navigation("Violations")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
