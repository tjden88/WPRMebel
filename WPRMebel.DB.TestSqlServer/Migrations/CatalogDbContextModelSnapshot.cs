﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WPRMebel.DB.TestSqlServer.Context;

namespace WPRMebel.DB.TestSqlServer.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SectionId")
                        .HasColumnType("int");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.HasIndex("VendorId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.Element", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ElementId")
                        .HasColumnType("int");

                    b.Property<double>("ExtraPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ElementId");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.ElementProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ElementId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceChanging")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ElementId");

                    b.ToTable("ElementProperties");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.ElementPropertyValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ElementPropertyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ElementPropertyId");

                    b.ToTable("ElementPropertyValues");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.Category", b =>
                {
                    b.HasOne("WPRMebel.Entityes.Catalog.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId");

                    b.HasOne("WPRMebel.Entityes.Catalog.Vendor", "Vendor")
                        .WithMany("Categories")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.Element", b =>
                {
                    b.HasOne("WPRMebel.Entityes.Catalog.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("WPRMebel.Entityes.Catalog.Element", null)
                        .WithMany("Elements")
                        .HasForeignKey("ElementId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.ElementProperty", b =>
                {
                    b.HasOne("WPRMebel.Entityes.Catalog.Element", "Element")
                        .WithMany("ElementProperties")
                        .HasForeignKey("ElementId");

                    b.Navigation("Element");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.ElementPropertyValue", b =>
                {
                    b.HasOne("WPRMebel.Entityes.Catalog.ElementProperty", "ElementProperty")
                        .WithMany("ElementPropertyValues")
                        .HasForeignKey("ElementPropertyId");

                    b.Navigation("ElementProperty");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.Element", b =>
                {
                    b.Navigation("ElementProperties");

                    b.Navigation("Elements");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.ElementProperty", b =>
                {
                    b.Navigation("ElementPropertyValues");
                });

            modelBuilder.Entity("WPRMebel.Entityes.Catalog.Vendor", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
