// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Negrea_Georgiana_MasterProiect.Data;

namespace Negrea_Georgiana_MasterProiect.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20220212091531_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.Boot", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("ID");

                    b.ToTable("Boot");
                });

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BootID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderID");

                    b.HasIndex("BootID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.Seller", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("SellerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Seller");
                });

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.SoldBoot", b =>
                {
                    b.Property<int>("BootID")
                        .HasColumnType("int");

                    b.Property<int>("SellerID")
                        .HasColumnType("int");

                    b.HasKey("BootID", "SellerID");

                    b.HasIndex("SellerID");

                    b.ToTable("SoldBoot");
                });

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.Order", b =>
                {
                    b.HasOne("Negrea_Georgiana_MasterProiect.Models.Boot", "Boot")
                        .WithMany("Orders")
                        .HasForeignKey("BootID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Negrea_Georgiana_MasterProiect.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boot");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.SoldBoot", b =>
                {
                    b.HasOne("Negrea_Georgiana_MasterProiect.Models.Boot", "Boot")
                        .WithMany("SoldBoots")
                        .HasForeignKey("BootID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Negrea_Georgiana_MasterProiect.Models.Seller", "Seller")
                        .WithMany("SoldBoots")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boot");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.Boot", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("SoldBoots");
                });

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Negrea_Georgiana_MasterProiect.Models.Seller", b =>
                {
                    b.Navigation("SoldBoots");
                });
#pragma warning restore 612, 618
        }
    }
}
