﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecruitementManagementApp.Models;

#nullable disable

namespace RecruitementManagementApp.Migrations
{
    [DbContext(typeof(RhmanagementDbContext))]
    [Migration("20240502191705_Addfirstmigration")]
    partial class Addfirstmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RecruitementManagementApp.Models.Admin", b =>
                {
                    b.Property<int>("IdAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAdmin"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAdmin");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.Employee", b =>
                {
                    b.Property<int>("IdEmployee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmployee"));

                    b.Property<DateTime?>("DateNaiss")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEmployee");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.EmployeeFormation", b =>
                {
                    b.Property<int>("IdEmployee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmployee"));

                    b.Property<int>("EmployeeIdEmployee")
                        .HasColumnType("int");

                    b.Property<int>("FormationIdFormation")
                        .HasColumnType("int");

                    b.Property<int>("IdFormation")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("IdEmployee");

                    b.HasIndex("EmployeeIdEmployee");

                    b.HasIndex("FormationIdFormation");

                    b.ToTable("employeeFormation");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.Formation", b =>
                {
                    b.Property<int>("IdFormation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFormation"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdRh")
                        .HasColumnType("int");

                    b.Property<int?>("LeRhIdRh")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("archived")
                        .HasColumnType("bit");

                    b.Property<bool>("published")
                        .HasColumnType("bit");

                    b.HasKey("IdFormation");

                    b.HasIndex("LeRhIdRh");

                    b.ToTable("Formations");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.Rh", b =>
                {
                    b.Property<int>("IdRh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRh"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRh");

                    b.ToTable("RHs");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("profilecompleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.EmployeeFormation", b =>
                {
                    b.HasOne("RecruitementManagementApp.Models.Employee", "Employee")
                        .WithMany("employeeFormations")
                        .HasForeignKey("EmployeeIdEmployee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecruitementManagementApp.Models.Formation", "Formation")
                        .WithMany("employeeFormations")
                        .HasForeignKey("FormationIdFormation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Formation");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.Formation", b =>
                {
                    b.HasOne("RecruitementManagementApp.Models.Rh", "LeRh")
                        .WithMany("Formations")
                        .HasForeignKey("LeRhIdRh");

                    b.Navigation("LeRh");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.Employee", b =>
                {
                    b.Navigation("employeeFormations");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.Formation", b =>
                {
                    b.Navigation("employeeFormations");
                });

            modelBuilder.Entity("RecruitementManagementApp.Models.Rh", b =>
                {
                    b.Navigation("Formations");
                });
#pragma warning restore 612, 618
        }
    }
}
