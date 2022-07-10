﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wasm.Server.Persistence;

#nullable disable

namespace Wasm.Server.Persistence.Data.Migrations
{
    [DbContext(typeof(BlazingTrailsContext))]
    partial class BlazingTrailsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Wasm.Server.Persistence.Entities.RouteInstruction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Stage")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrailId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrailId");

                    b.ToTable("RouteInstructions");
                });

            modelBuilder.Entity("Wasm.Server.Persistence.Entities.Trail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<int>("Length")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TimeInMinutes")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Trails");
                });

            modelBuilder.Entity("Wasm.Server.Persistence.Entities.RouteInstruction", b =>
                {
                    b.HasOne("Wasm.Server.Persistence.Entities.Trail", "Trail")
                        .WithMany("Route")
                        .HasForeignKey("TrailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trail");
                });

            modelBuilder.Entity("Wasm.Server.Persistence.Entities.Trail", b =>
                {
                    b.Navigation("Route");
                });
#pragma warning restore 612, 618
        }
    }
}
