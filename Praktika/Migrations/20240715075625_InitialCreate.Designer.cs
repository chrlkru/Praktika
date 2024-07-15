﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Praktika;

#nullable disable

namespace Praktika.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240715075625_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Praktika.DiscontCard", b =>
                {
                    b.Property<int>("DiscontCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Discont")
                        .HasColumnType("REAL");

                    b.Property<int>("OrderListId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("OrderSum")
                        .HasColumnType("REAL");

                    b.Property<int>("UsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiscontCardId");

                    b.HasIndex("OrderListId");

                    b.HasIndex("UsersId");

                    b.ToTable("DiscontCard");
                });

            modelBuilder.Entity("Praktika.OrderList", b =>
                {
                    b.Property<int>("OrderListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("OrderListId");

                    b.HasIndex("UsersId");

                    b.ToTable("OrderList");
                });

            modelBuilder.Entity("Praktika.User", b =>
                {
                    b.Property<int>("UsersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ДатаРождения")
                        .HasColumnType("TEXT");

                    b.Property<string>("Телефон")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Фио")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UsersId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Praktika.DiscontCard", b =>
                {
                    b.HasOne("Praktika.OrderList", "OrderList")
                        .WithMany("DiscontCards")
                        .HasForeignKey("OrderListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Praktika.User", "Users")
                        .WithMany("DiscontCards")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderList");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Praktika.OrderList", b =>
                {
                    b.HasOne("Praktika.User", "Users")
                        .WithMany("OrderLists")
                        .HasForeignKey("UsersId");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Praktika.OrderList", b =>
                {
                    b.Navigation("DiscontCards");
                });

            modelBuilder.Entity("Praktika.User", b =>
                {
                    b.Navigation("DiscontCards");

                    b.Navigation("OrderLists");
                });
#pragma warning restore 612, 618
        }
    }
}
