﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mockOSApi.Data;

#nullable disable

namespace mockOSApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240406194118_ProcessNewField")]
    partial class ProcessNewField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("mockOSApi.Models.Process<mockOSApi.Controllers.TestController>", b =>
                {
                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Pid"));

                    b.Property<string>("Args")
                        .HasColumnType("longtext");

                    b.Property<int>("ExitCode")
                        .HasColumnType("int");

                    b.Property<int>("Handle")
                        .HasColumnType("int");

                    b.Property<int>("HashCode")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("ProcessCounter")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Pid");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("mockOSApi.Models.ProcessCounter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProcCounter")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProcessCounters");
                });

            modelBuilder.Entity("mockOSApi.Models.Thread", b =>
                {
                    b.Property<int>("Tid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Tid"));

                    b.Property<int>("Handle")
                        .HasColumnType("int");

                    b.Property<int>("HashCode")
                        .HasColumnType("int");

                    b.Property<int>("threadCount")
                        .HasColumnType("int");

                    b.HasKey("Tid");

                    b.ToTable("Threads");
                });
#pragma warning restore 612, 618
        }
    }
}
