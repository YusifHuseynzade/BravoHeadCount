﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("OTPToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("OTPTokenCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OTPTokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Badge")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("FunctionalAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("SectionId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubSectionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FunctionalAreaId");

                    b.HasIndex("PositionId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SectionId");

                    b.HasIndex("SubSectionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Format", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Formats");
                });

            modelBuilder.Entity("Domain.Entities.FunctionalArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("FunctionalAreas");
                });

            modelBuilder.Entity("Domain.Entities.HeadCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("FunctionalAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("HCNumber")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsVacant")
                        .HasColumnType("boolean");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.Property<int?>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<string>("RecruiterComment")
                        .HasColumnType("text");

                    b.Property<int?>("SectionId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubSectionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FunctionalAreaId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PositionId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SectionId");

                    b.HasIndex("SubSectionId");

                    b.ToTable("HeadCounts");
                });

            modelBuilder.Entity("Domain.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FunctionalAreaId")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsHeadOffice")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsStore")
                        .HasColumnType("boolean");

                    b.Property<string>("ProjectCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("FunctionalAreaId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Domain.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AreaManagerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DirectorId")
                        .HasColumnType("integer");

                    b.Property<int?>("FormatId")
                        .HasColumnType("integer");

                    b.Property<int>("FunctionalAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("HeadCountNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int?>("RecruiterId")
                        .HasColumnType("integer");

                    b.Property<int?>("StoreManagerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AreaManagerId");

                    b.HasIndex("DirectorId");

                    b.HasIndex("FormatId");

                    b.HasIndex("FunctionalAreaId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RecruiterId");

                    b.HasIndex("StoreManagerId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Domain.Entities.SubSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("SectionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("SubSections");
                });

            modelBuilder.Entity("Domain.Entities.AppUser", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("AppUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.HasOne("Domain.Entities.FunctionalArea", "FunctionalArea")
                        .WithMany("Employees")
                        .HasForeignKey("FunctionalAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany("Employees")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Section", "Section")
                        .WithMany("Employees")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.SubSection", "SubSection")
                        .WithMany("Employees")
                        .HasForeignKey("SubSectionId");

                    b.Navigation("FunctionalArea");

                    b.Navigation("Position");

                    b.Navigation("Project");

                    b.Navigation("Section");

                    b.Navigation("SubSection");
                });

            modelBuilder.Entity("Domain.Entities.HeadCount", b =>
                {
                    b.HasOne("Domain.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("Domain.Entities.FunctionalArea", "FunctionalArea")
                        .WithMany()
                        .HasForeignKey("FunctionalAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.HeadCount", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("Domain.Entities.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");

                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId");

                    b.HasOne("Domain.Entities.SubSection", "SubSection")
                        .WithMany()
                        .HasForeignKey("SubSectionId");

                    b.Navigation("Employee");

                    b.Navigation("FunctionalArea");

                    b.Navigation("Parent");

                    b.Navigation("Position");

                    b.Navigation("Project");

                    b.Navigation("Section");

                    b.Navigation("SubSection");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.HasOne("Domain.Entities.FunctionalArea", "FunctionalArea")
                        .WithMany("Projects")
                        .HasForeignKey("FunctionalAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FunctionalArea");
                });

            modelBuilder.Entity("Domain.Entities.Section", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany("Sections")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Entities.Store", b =>
                {
                    b.HasOne("Domain.Entities.Employee", "AreaManager")
                        .WithMany()
                        .HasForeignKey("AreaManagerId");

                    b.HasOne("Domain.Entities.Employee", "Director")
                        .WithMany()
                        .HasForeignKey("DirectorId");

                    b.HasOne("Domain.Entities.Format", "Format")
                        .WithMany("Stores")
                        .HasForeignKey("FormatId");

                    b.HasOne("Domain.Entities.FunctionalArea", "FunctionalArea")
                        .WithMany()
                        .HasForeignKey("FunctionalAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("Domain.Entities.Employee", "Recruiter")
                        .WithMany()
                        .HasForeignKey("RecruiterId");

                    b.HasOne("Domain.Entities.Employee", "StoreManager")
                        .WithMany()
                        .HasForeignKey("StoreManagerId");

                    b.Navigation("AreaManager");

                    b.Navigation("Director");

                    b.Navigation("Format");

                    b.Navigation("FunctionalArea");

                    b.Navigation("Project");

                    b.Navigation("Recruiter");

                    b.Navigation("StoreManager");
                });

            modelBuilder.Entity("Domain.Entities.SubSection", b =>
                {
                    b.HasOne("Domain.Entities.Section", "Section")
                        .WithMany("SubSections")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Domain.Entities.Format", b =>
                {
                    b.Navigation("Stores");
                });

            modelBuilder.Entity("Domain.Entities.FunctionalArea", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Domain.Entities.Position", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("AppUsers");
                });

            modelBuilder.Entity("Domain.Entities.Section", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("SubSections");
                });

            modelBuilder.Entity("Domain.Entities.SubSection", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
