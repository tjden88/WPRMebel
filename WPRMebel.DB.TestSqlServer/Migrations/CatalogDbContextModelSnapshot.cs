// <auto-generated />
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

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Extra")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("CatalogElement");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CatalogElement");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<int?>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.HasIndex("VendorId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.ChildCatalogElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CatalogElementId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerCatalogElementId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CatalogElementId");

                    b.HasIndex("OwnerCatalogElementId");

                    b.ToTable("ChildCatalogElements");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.ElementProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CatalogElementId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceChanging")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CatalogElementId");

                    b.ToTable("ElementProperties");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.ElementPropertyValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ElementPropertyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ElementPropertyId");

                    b.ToTable("ElementPropertyValues");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Section", b =>
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

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Fitting", b =>
                {
                    b.HasBaseType("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement");

                    b.HasDiscriminator().HasValue("Fitting");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.RunningMaterial", b =>
                {
                    b.HasBaseType("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement");

                    b.Property<int?>("Width")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("RunningMaterial");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Service", b =>
                {
                    b.HasBaseType("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement");

                    b.HasDiscriminator().HasValue("Service");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.SheetMaterial", b =>
                {
                    b.HasBaseType("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement");

                    b.Property<int?>("DetaliMaxHeight")
                        .HasColumnType("int");

                    b.Property<int?>("DetaliMaxWidth")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("SheetMaterial");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement", b =>
                {
                    b.HasOne("WPRMebel.Domain.Base.Catalog.Category", "Category")
                        .WithMany("CatalogElements")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Category", b =>
                {
                    b.HasOne("WPRMebel.Domain.Base.Catalog.Section", "Section")
                        .WithMany("Categories")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WPRMebel.Domain.Base.Catalog.Vendor", "Vendor")
                        .WithMany("Categories")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Section");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.ChildCatalogElement", b =>
                {
                    b.HasOne("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement", "CatalogElement")
                        .WithMany()
                        .HasForeignKey("CatalogElementId");

                    b.HasOne("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement", "OwnerCatalogElement")
                        .WithMany("ChildCatalogElements")
                        .HasForeignKey("OwnerCatalogElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogElement");

                    b.Navigation("OwnerCatalogElement");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.ElementProperty", b =>
                {
                    b.HasOne("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement", "CatalogElement")
                        .WithMany("ElementProperties")
                        .HasForeignKey("CatalogElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogElement");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.ElementPropertyValue", b =>
                {
                    b.HasOne("WPRMebel.Domain.Base.Catalog.ElementProperty", "ElementProperty")
                        .WithMany("ElementPropertyValues")
                        .HasForeignKey("ElementPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ElementProperty");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Abstract.CatalogElement", b =>
                {
                    b.Navigation("ChildCatalogElements");

                    b.Navigation("ElementProperties");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Category", b =>
                {
                    b.Navigation("CatalogElements");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.ElementProperty", b =>
                {
                    b.Navigation("ElementPropertyValues");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Section", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("WPRMebel.Domain.Base.Catalog.Vendor", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
