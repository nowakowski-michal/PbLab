﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(WebshopContext))]
    [Migration("20240327101942_nowaasd7")]
    partial class nowaasd7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Model.BasketPosition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int?>("UserGroupID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("UserGroupID");

                    b.HasIndex("UserID");

                    b.ToTable("BasketPositions");
                });

            modelBuilder.Entity("Model.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<bool>("isPayed")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Model.OrderPosition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderPositions");
                });

            modelBuilder.Entity("Model.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("GroupID")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Model.ProductGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ParentID");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("GroupID")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Model.UserGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("Model.BasketPosition", b =>
                {
                    b.HasOne("Model.Product", "Product")
                        .WithMany("BasketPositions")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Model.UserGroup", null)
                        .WithMany("BasketPositions")
                        .HasForeignKey("UserGroupID");

                    b.HasOne("Model.User", "User")
                        .WithMany("BasketPositions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.Order", b =>
                {
                    b.HasOne("Model.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.OrderPosition", b =>
                {
                    b.HasOne("Model.Order", "Order")
                        .WithMany("Positions")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Product", "Product")
                        .WithMany("OrderPositions")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Model.Product", b =>
                {
                    b.HasOne("Model.ProductGroup", "Group")
                        .WithMany("Products")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Model.ProductGroup", b =>
                {
                    b.HasOne("Model.ProductGroup", "Parent")
                        .WithMany("Childrens")
                        .HasForeignKey("ParentID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.HasOne("Model.UserGroup", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupID");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Model.Order", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Model.Product", b =>
                {
                    b.Navigation("BasketPositions");

                    b.Navigation("OrderPositions");
                });

            modelBuilder.Entity("Model.ProductGroup", b =>
                {
                    b.Navigation("Childrens");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Navigation("BasketPositions");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Model.UserGroup", b =>
                {
                    b.Navigation("BasketPositions");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}