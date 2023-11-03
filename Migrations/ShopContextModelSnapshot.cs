﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication5.DBContext;

#nullable disable

namespace WebApplication5.Migrations
{
    [DbContext(typeof(ShopContext))]
    partial class ShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication5.Context.Data", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<short>("DOLocationID")
                        .HasColumnType("smallint");

                    b.Property<short>("PULocationID")
                        .HasColumnType("smallint");

                    b.Property<float>("fare_amount")
                        .HasColumnType("real");

                    b.Property<byte>("passenger_count")
                        .HasColumnType("tinyint");

                    b.Property<string>("store_and_fwd_flag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("tip_amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("tpep_dropoff_datetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("tpep_pickup_datetime")
                        .HasColumnType("datetime2");

                    b.Property<float>("trip_distance")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.ToTable("datas");
                });
#pragma warning restore 612, 618
        }
    }
}
