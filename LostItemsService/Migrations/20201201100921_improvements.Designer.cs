﻿// <auto-generated />
using System;
using LostItemsService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LostItemsService.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20201201100921_improvements")]
    partial class improvements
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("LostItemsService.Database.Entities.Comments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("LostItemId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LostItemId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("LostItemsService.Database.Entities.LostItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(5000)")
                        .HasMaxLength(5000);

                    b.Property<bool>("Found")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("FoundAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LostAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Model")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("city")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LostItems");
                });

            modelBuilder.Entity("LostItemsService.Database.Entities.Comments", b =>
                {
                    b.HasOne("LostItemsService.Database.Entities.LostItem", "LostItem")
                        .WithMany("Comments")
                        .HasForeignKey("LostItemId");
                });
#pragma warning restore 612, 618
        }
    }
}
