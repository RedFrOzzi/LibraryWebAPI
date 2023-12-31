﻿// <auto-generated />
using System;
using LibraryWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryWebAPI.Migrations
{
    [DbContext(typeof(LibraryDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("LibraryWebAPI.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BookStackId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookStackId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("LibraryWebAPI.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookStackId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CurrentReaderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookStackId");

                    b.HasIndex("CurrentReaderId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryWebAPI.Models.BookReader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BookReaders");
                });

            modelBuilder.Entity("LibraryWebAPI.Models.BookStack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BookStacks");
                });

            modelBuilder.Entity("LibraryWebAPI.Models.Author", b =>
                {
                    b.HasOne("LibraryWebAPI.Models.BookStack", null)
                        .WithMany("Authors")
                        .HasForeignKey("BookStackId");
                });

            modelBuilder.Entity("LibraryWebAPI.Models.Book", b =>
                {
                    b.HasOne("LibraryWebAPI.Models.BookStack", "BookStack")
                        .WithMany("Books")
                        .HasForeignKey("BookStackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryWebAPI.Models.BookReader", "CurrentReader")
                        .WithMany("BorrowedBooks")
                        .HasForeignKey("CurrentReaderId");

                    b.Navigation("BookStack");

                    b.Navigation("CurrentReader");
                });

            modelBuilder.Entity("LibraryWebAPI.Models.BookReader", b =>
                {
                    b.Navigation("BorrowedBooks");
                });

            modelBuilder.Entity("LibraryWebAPI.Models.BookStack", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
