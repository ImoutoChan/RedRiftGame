﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RedRiftGame.Infrastructure.Persistence;

#nullable disable

namespace RedRiftGame.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(RedRiftGameDbContext))]
    [Migration("20220419103327_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RedRiftGame.Infrastructure.Persistence.Entities.MatchEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Instant>("FinishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GuestFinalHealth")
                        .HasColumnType("integer");

                    b.Property<string>("GuestName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("HostFinalHealth")
                        .HasColumnType("integer");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalTurnsPlayed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });
#pragma warning restore 612, 618
        }
    }
}