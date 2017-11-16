﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OcenUczelnie.Infrastructure.EF;
using System;

namespace OcenUczelnie.Infrastructure.Migrations
{
    [DbContext(typeof(OcenUczelnieContext))]
    [Migration("20171114104245_changeReviewDomain")]
    partial class changeReviewDomain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OcenUczelnie.Core.Domain.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Department")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid?>("UniversityId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("OcenUczelnie.Core.Domain.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<Guid>("CourseId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("Rating");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("OcenUczelnie.Core.Domain.ReviewUserApproved", b =>
                {
                    b.Property<Guid>("ReviewId");

                    b.Property<Guid>("UserId");

                    b.HasKey("ReviewId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ReviewUserApproved");
                });

            modelBuilder.Entity("OcenUczelnie.Core.Domain.ReviewUserDisapproved", b =>
                {
                    b.Property<Guid>("ReviewId");

                    b.Property<Guid>("UserId");

                    b.HasKey("ReviewId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ReviewUserDisapproved");
                });

            modelBuilder.Entity("OcenUczelnie.Core.Domain.University", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Place")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("OcenUczelnie.Core.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Role")
                        .IsRequired();

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OcenUczelnie.Core.Domain.Course", b =>
                {
                    b.HasOne("OcenUczelnie.Core.Domain.University", "University")
                        .WithMany("Courses")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OcenUczelnie.Core.Domain.Review", b =>
                {
                    b.HasOne("OcenUczelnie.Core.Domain.Course", "Course")
                        .WithMany("Reviews")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OcenUczelnie.Core.Domain.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OcenUczelnie.Core.Domain.ReviewUserApproved", b =>
                {
                    b.HasOne("OcenUczelnie.Core.Domain.Review", "Review")
                        .WithMany("ReviewUserApproved")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OcenUczelnie.Core.Domain.User", "User")
                        .WithMany("ReviewUserApproved")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OcenUczelnie.Core.Domain.ReviewUserDisapproved", b =>
                {
                    b.HasOne("OcenUczelnie.Core.Domain.Review", "Review")
                        .WithMany("ReviewUserDisapproved")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OcenUczelnie.Core.Domain.User", "User")
                        .WithMany("ReviewUserDisapproved")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
