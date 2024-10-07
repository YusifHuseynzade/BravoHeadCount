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

            modelBuilder.Entity("Domain.Entities.BakuDistrict", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.HasKey("Id");

                    b.ToTable("BakuDistricts");
                });

            modelBuilder.Entity("Domain.Entities.BakuMetro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.HasKey("Id");

                    b.ToTable("BakuMetros");
                });

            modelBuilder.Entity("Domain.Entities.BakuTarget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.HasKey("Id");

                    b.ToTable("BakuTargets");
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

                    b.Property<int?>("BakuDistrictId")
                        .HasColumnType("integer");

                    b.Property<int?>("BakuMetroId")
                        .HasColumnType("integer");

                    b.Property<int?>("BakuTargetId")
                        .HasColumnType("integer");

                    b.Property<string>("ContractEndDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FIN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<string>("RecruiterComment")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int?>("ResidentalAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("SectionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("SubSectionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BakuDistrictId");

                    b.HasIndex("BakuMetroId");

                    b.HasIndex("BakuTargetId");

                    b.HasIndex("PositionId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ResidentalAreaId");

                    b.HasIndex("SectionId");

                    b.HasIndex("SubSectionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Domain.Entities.HeadCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ColorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("HCNumber")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsVacant")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.Property<int?>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int?>("SectionId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubSectionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PositionId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SectionId");

                    b.HasIndex("SubSectionId");

                    b.ToTable("HeadCounts");
                });

            modelBuilder.Entity("Domain.Entities.HeadCountBackgroundColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorHexCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("HeadCountBackgroundColors");
                });

            modelBuilder.Entity("Domain.Entities.HeadCountHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("FromProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("ToProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FromProjectId");

                    b.HasIndex("ToProjectId");

                    b.ToTable("HeadCountHistories");
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

                    b.Property<string>("AreaManager")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AreaManagerBadge")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AreaManagerMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FunctionalArea")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsHeadOffice")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsStore")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OperationDirector")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OperationDirectorMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProjectCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Recruiter")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RecruiterMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StoreClosedDate")
                        .HasColumnType("text");

                    b.Property<string>("StoreManagerMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StoreOpeningDate")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Domain.Entities.ProjectHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NewAreaManager")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NewAreaManagerEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NewDirector")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NewDirectorEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NewFormat")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NewFunctionalArea")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("NewIsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("NewRecruiter")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NewRecruiterEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NewStoreManagerEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldAreaManager")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldAreaManagerEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldDirector")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldDirectorEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldFormat")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldFunctionalArea")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("OldIsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("OldRecruiter")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldRecruiterEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldStoreManagerEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectHistories");
                });

            modelBuilder.Entity("Domain.Entities.ProjectSections", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("SectionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SectionId");

                    b.ToTable("ProjectSections");
                });

            modelBuilder.Entity("Domain.Entities.ResidentalArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.HasKey("Id");

                    b.ToTable("ResidentalAreas");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("RoleType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.ScheduledData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 10, 7, 16, 12, 35, 434, DateTimeKind.Utc).AddTicks(6397));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Fact")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GraduationBalance")
                        .HasColumnType("integer");

                    b.Property<string>("GraduationSchedule")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("HolidayBalance")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Plan")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ScheduledDatas");
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

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Domain.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("HeadCountNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Domain.Entities.StoreHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NewHeadCountNumber")
                        .HasColumnType("integer");

                    b.Property<int>("OldHeadCountNumber")
                        .HasColumnType("integer");

                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("StoreHistories");
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
                    b.HasOne("Domain.Entities.BakuDistrict", "BakuDistrict")
                        .WithMany("Employees")
                        .HasForeignKey("BakuDistrictId");

                    b.HasOne("Domain.Entities.BakuMetro", "BakuMetro")
                        .WithMany("Employees")
                        .HasForeignKey("BakuMetroId");

                    b.HasOne("Domain.Entities.BakuTarget", "BakuTarget")
                        .WithMany("Employees")
                        .HasForeignKey("BakuTargetId");

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

                    b.HasOne("Domain.Entities.ResidentalArea", "ResidentalArea")
                        .WithMany("Employees")
                        .HasForeignKey("ResidentalAreaId");

                    b.HasOne("Domain.Entities.Section", "Section")
                        .WithMany("Employees")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.SubSection", "SubSection")
                        .WithMany("Employees")
                        .HasForeignKey("SubSectionId");

                    b.Navigation("BakuDistrict");

                    b.Navigation("BakuMetro");

                    b.Navigation("BakuTarget");

                    b.Navigation("Position");

                    b.Navigation("Project");

                    b.Navigation("ResidentalArea");

                    b.Navigation("Section");

                    b.Navigation("SubSection");
                });

            modelBuilder.Entity("Domain.Entities.HeadCount", b =>
                {
                    b.HasOne("Domain.Entities.HeadCountBackgroundColor", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId");

                    b.HasOne("Domain.Entities.Employee", "Employee")
                        .WithMany("HeadCounts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.SetNull);

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

                    b.Navigation("Color");

                    b.Navigation("Employee");

                    b.Navigation("Parent");

                    b.Navigation("Position");

                    b.Navigation("Project");

                    b.Navigation("Section");

                    b.Navigation("SubSection");
                });

            modelBuilder.Entity("Domain.Entities.HeadCountHistory", b =>
                {
                    b.HasOne("Domain.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Project", "FromProject")
                        .WithMany()
                        .HasForeignKey("FromProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Project", "ToProject")
                        .WithMany()
                        .HasForeignKey("ToProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("FromProject");

                    b.Navigation("ToProject");
                });

            modelBuilder.Entity("Domain.Entities.ProjectHistory", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Entities.ProjectSections", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany("ProjectSections")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Section", "Section")
                        .WithMany("ProjectSections")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Domain.Entities.ScheduledData", b =>
                {
                    b.HasOne("Domain.Entities.Employee", "Employee")
                        .WithMany("ScheduledDatas")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany("ScheduledDatas")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Entities.Store", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Entities.StoreHistory", b =>
                {
                    b.HasOne("Domain.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
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

            modelBuilder.Entity("Domain.Entities.BakuDistrict", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.BakuMetro", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.BakuTarget", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Navigation("HeadCounts");

                    b.Navigation("ScheduledDatas");
                });

            modelBuilder.Entity("Domain.Entities.Position", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("ProjectSections");

                    b.Navigation("ScheduledDatas");
                });

            modelBuilder.Entity("Domain.Entities.ResidentalArea", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("AppUsers");
                });

            modelBuilder.Entity("Domain.Entities.Section", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("ProjectSections");

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
