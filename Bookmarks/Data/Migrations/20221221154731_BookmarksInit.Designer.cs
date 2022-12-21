﻿// <auto-generated />
using System;
using Bookmarks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bookmarks.Data.Migrations
{
    [DbContext(typeof(BookmarksDbContext))]
    [Migration("20221221154731_BookmarksInit")]
    partial class BookmarksInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Bookmarks.Data.Entities.Bookmark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Bookmarks", "Info");
                });

            modelBuilder.Entity("Bookmarks.Data.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Groups", "Info");
                });

            modelBuilder.Entity("Bookmarks.Data.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("Tags", "Info");
                });

            modelBuilder.Entity("BookmarkTag", b =>
                {
                    b.Property<Guid>("BookmarksId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BookmarksId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BookmarkTag", "Info");
                });

            modelBuilder.Entity("Bookmarks.Data.Entities.Bookmark", b =>
                {
                    b.HasOne("Bookmarks.Data.Entities.Group", "Group")
                        .WithMany("Bookmarks")
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("BookmarkTag", b =>
                {
                    b.HasOne("Bookmarks.Data.Entities.Bookmark", null)
                        .WithMany()
                        .HasForeignKey("BookmarksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bookmarks.Data.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bookmarks.Data.Entities.Group", b =>
                {
                    b.Navigation("Bookmarks");
                });
#pragma warning restore 612, 618
        }
    }
}
