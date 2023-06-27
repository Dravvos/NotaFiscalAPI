﻿// <auto-generated />
using System;
using CRUD_nota_fiscal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRUD_nota_fiscal.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CRUD_nota_fiscal.Models.NotaFiscal", b =>
                {
                    b.Property<Guid>("NotaFiscalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CnpjDestinatarioNf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CnpjEmissorNf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNf")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumeroNf")
                        .HasColumnType("int");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("float");

                    b.HasKey("NotaFiscalId");

                    b.ToTable("NotaFiscal");
                });
#pragma warning restore 612, 618
        }
    }
}
