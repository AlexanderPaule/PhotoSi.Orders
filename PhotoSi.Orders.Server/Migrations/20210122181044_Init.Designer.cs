﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhotoSi.Orders.Server.Orders.Data.Context;

namespace PhotoSi.Orders.Server.Migrations
{
    [DbContext(typeof(SalesDbContext))]
    [Migration("20210122181044_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DbCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DbUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OptionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DbCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DbUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DbCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DbUpdated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OrderedOptionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DbCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DbUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("OptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderedProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OptionId");

                    b.HasIndex("OrderedProductId");

                    b.ToTable("OrderedOptions");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OrderedProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DbCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DbUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("OrderEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderedProducts");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DbCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DbUpdated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OptionEntity", b =>
                {
                    b.HasOne("PhotoSi.Orders.Server.Orders.Data.Models.ProductEntity", "Product")
                        .WithMany("Options")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OrderEntity", b =>
                {
                    b.HasOne("PhotoSi.Orders.Server.Orders.Data.Models.CategoryEntity", "Category")
                        .WithMany("Orders")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OrderedOptionEntity", b =>
                {
                    b.HasOne("PhotoSi.Orders.Server.Orders.Data.Models.OptionEntity", "ReferencedOption")
                        .WithMany("CustomizedOptions")
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoSi.Orders.Server.Orders.Data.Models.OrderedProductEntity", "OrderedProduct")
                        .WithMany("Options")
                        .HasForeignKey("OrderedProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("OrderedProduct");

                    b.Navigation("ReferencedOption");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OrderedProductEntity", b =>
                {
                    b.HasOne("PhotoSi.Orders.Server.Orders.Data.Models.OrderEntity", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderEntityId");

                    b.HasOne("PhotoSi.Orders.Server.Orders.Data.Models.ProductEntity", "ReferencedProduct")
                        .WithMany("OrderedProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReferencedProduct");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.ProductEntity", b =>
                {
                    b.HasOne("PhotoSi.Orders.Server.Orders.Data.Models.CategoryEntity", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.CategoryEntity", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OptionEntity", b =>
                {
                    b.Navigation("CustomizedOptions");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OrderEntity", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.OrderedProductEntity", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("PhotoSi.Orders.Server.Orders.Data.Models.ProductEntity", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("OrderedProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
