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
    [Migration("20240406133537_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

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
