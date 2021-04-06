﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MintGarage.Database;

namespace MintGarage.Migrations
{
    [DbContext(typeof(MintGarageContext))]
    [Migration("20210326050603_Initializ")]
    partial class Initializ
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MintGarage.Models.Categories.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("MintGarage.Models.ConsultationForms.ConsultationForm", b =>
                {
                    b.Property<int>("ConsultationFormID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServiceID")
                        .HasColumnType("int");

                    b.HasKey("ConsultationFormID");

                    b.HasIndex("ServiceID");

                    b.ToTable("ConsultationForm");
                });

            modelBuilder.Entity("MintGarage.Models.Products.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("ProductColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductHeight")
                        .HasColumnType("float");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductLength")
                        .HasColumnType("float");

                    b.Property<string>("ProductMaterial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.Property<string>("ProductType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductWidth")
                        .HasColumnType("float");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("MintGarage.Models.Service.TypeService", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceID");

                    b.ToTable("TypeService");
                });

            modelBuilder.Entity("MintGarage.Models.ConsultationForms.ConsultationForm", b =>
                {
                    b.HasOne("MintGarage.Models.Service.TypeService", "TypeService")
                        .WithMany("ConsultationForm")
                        .HasForeignKey("ServiceID");

                    b.Navigation("TypeService");
                });

            modelBuilder.Entity("MintGarage.Models.Products.Product", b =>
                {
                    b.HasOne("MintGarage.Models.Categories.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CategoryID");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MintGarage.Models.Categories.Category", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("MintGarage.Models.Service.TypeService", b =>
                {
                    b.Navigation("ConsultationForm");
                });
#pragma warning restore 612, 618
        }
    }
}