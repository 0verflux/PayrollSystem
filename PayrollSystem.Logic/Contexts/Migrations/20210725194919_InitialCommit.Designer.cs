﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayrollSystem.Logic.Contexts;

namespace PayrollSystem.Logic.Contexts.Migrations
{
    [DbContext(typeof(PayrollDBContext))]
    [Migration("20210725194919_InitialCommit")]
    partial class InitialCommit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PayrollSystem.Logic.Domain.Employees.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PositionID")
                        .HasColumnType("int")
                        .HasColumnName("PositionID");

                    b.HasKey("ID");

                    b.HasIndex("PositionID");

                    b.ToTable("Employees", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.PayrollEntries.PayrollEntry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PayrollEntryID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentPositionID")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<double>("HoursOvertime")
                        .HasColumnType("float")
                        .HasColumnName("HoursOvertime");

                    b.Property<double>("HoursWorked")
                        .HasColumnType("float")
                        .HasColumnName("HoursWorked");

                    b.HasKey("ID");

                    b.HasIndex("CurrentPositionID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("PayrollEntries", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.Positions.Position", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PositionID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("Name");

                    b.Property<decimal>("RatePerHour")
                        .HasColumnType("decimal(14,4)")
                        .HasColumnName("RatePerHour");

                    b.HasKey("ID");

                    b.HasIndex(new[] { "Name" }, "IX_Positions")
                        .IsUnique();

                    b.ToTable("Positions", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.SalaryAdjustmentDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SalaryAdjustmentDetailID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PayrollEntryID")
                        .HasColumnType("int");

                    b.Property<int>("SalaryAdjustmentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SalaryAdjustmentID");

                    b.HasIndex(new[] { "PayrollEntryID", "SalaryAdjustmentID" }, "IX_SalaryAdjustmentDetails")
                        .IsUnique();

                    b.ToTable("SalaryAdjustmentDetails", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.SalaryAdjustments.SalaryAdjustment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SalaryAdjustmentID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("Code");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.HasKey("ID");

                    b.HasIndex(new[] { "Code" }, "IX_SalaryAdjustments")
                        .IsUnique();

                    b.ToTable("SalaryAdjustments", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.Employees.Employee", b =>
                {
                    b.HasOne("PayrollSystem.Logic.Domain.Positions.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionID")
                        .HasConstraintName("FK_Employees_Positions")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsOne("PayrollSystem.Logic.Common.DateRange", "EmploymentDate", b1 =>
                        {
                            b1.Property<int>("EmployeeID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime?>("End")
                                .HasColumnType("date")
                                .HasColumnName("EmploymentEndDate");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("date")
                                .HasColumnName("EmploymentStartDate");

                            b1.HasKey("EmployeeID");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeID");
                        });

                    b.OwnsOne("PayrollSystem.Logic.Domain.Employees.PersonalInformation", "PersonalInformation", b1 =>
                        {
                            b1.Property<int>("EmployeeID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(512)
                                .HasColumnType("nvarchar(512)")
                                .HasColumnName("Address");

                            b1.Property<DateTime>("BirthDate")
                                .HasColumnType("date")
                                .HasColumnName("BirthDate");

                            b1.Property<string>("FullName")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("FullName");

                            b1.Property<string>("Gender")
                                .IsRequired()
                                .HasMaxLength(1)
                                .IsUnicode(false)
                                .HasColumnType("char(1)")
                                .HasColumnName("Gender")
                                .IsFixedLength(true);

                            b1.HasKey("EmployeeID");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeID");
                        });

                    b.Navigation("EmploymentDate")
                        .IsRequired();

                    b.Navigation("PersonalInformation")
                        .IsRequired();

                    b.Navigation("Position");
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.PayrollEntries.PayrollEntry", b =>
                {
                    b.HasOne("PayrollSystem.Logic.Domain.Positions.Position", "CurrentPosition")
                        .WithMany("PayrollEntries")
                        .HasForeignKey("CurrentPositionID")
                        .HasConstraintName("FK_PayrollEntries_Positions")
                        .IsRequired();

                    b.HasOne("PayrollSystem.Logic.Domain.Employees.Employee", "Employee")
                        .WithMany("PayrollEntries")
                        .HasForeignKey("EmployeeID")
                        .HasConstraintName("FK_PayrollEntries_Employees")
                        .IsRequired();

                    b.OwnsOne("PayrollSystem.Logic.Common.DateRange", "Date", b1 =>
                        {
                            b1.Property<int>("PayrollEntryID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime?>("End")
                                .IsRequired()
                                .HasColumnType("date")
                                .HasColumnName("EndDate");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("date")
                                .HasColumnName("StartDate");

                            b1.HasKey("PayrollEntryID");

                            b1.ToTable("PayrollEntries");

                            b1.WithOwner()
                                .HasForeignKey("PayrollEntryID");
                        });

                    b.Navigation("CurrentPosition");

                    b.Navigation("Date")
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.SalaryAdjustmentDetail", b =>
                {
                    b.HasOne("PayrollSystem.Logic.Domain.PayrollEntries.PayrollEntry", "PayrollEntry")
                        .WithMany("SalaryAdjustmentDetails")
                        .HasForeignKey("PayrollEntryID")
                        .HasConstraintName("FK_SalaryAdjustmentDetails_PayrollEntries")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PayrollSystem.Logic.Domain.SalaryAdjustments.SalaryAdjustment", "SalaryAdjustment")
                        .WithMany("SalaryAdjustmentDetails")
                        .HasForeignKey("SalaryAdjustmentID")
                        .HasConstraintName("FK_SalaryAdjustmentDetails_SalaryAdjustments")
                        .IsRequired();

                    b.OwnsOne("PayrollSystem.Logic.Domain.SalaryAdjustmentDetails.Percentage", "Percentage", b1 =>
                        {
                            b1.Property<int>("SalaryAdjustmentDetailID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<double>("Ratio")
                                .HasColumnType("float")
                                .HasColumnName("Ratio");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(14,4)")
                                .HasColumnName("Value");

                            b1.HasKey("SalaryAdjustmentDetailID");

                            b1.ToTable("SalaryAdjustmentDetails");

                            b1.WithOwner()
                                .HasForeignKey("SalaryAdjustmentDetailID");
                        });

                    b.Navigation("PayrollEntry");

                    b.Navigation("Percentage")
                        .IsRequired();

                    b.Navigation("SalaryAdjustment");
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.Employees.Employee", b =>
                {
                    b.Navigation("PayrollEntries");
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.PayrollEntries.PayrollEntry", b =>
                {
                    b.Navigation("SalaryAdjustmentDetails");
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.Positions.Position", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("PayrollEntries");
                });

            modelBuilder.Entity("PayrollSystem.Logic.Domain.SalaryAdjustments.SalaryAdjustment", b =>
                {
                    b.Navigation("SalaryAdjustmentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
