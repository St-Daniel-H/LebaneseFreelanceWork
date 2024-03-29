﻿// <auto-generated />
using System;
using LebUpwor.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LebUpwor.core.Migrations
{
    [DbContext(typeof(UpworkLebContext))]
    [Migration("20240302084527_addedReports")]
    partial class addedReports
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JobTag", b =>
                {
                    b.Property<int>("JobsJobId")
                        .HasColumnType("int");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("int");

                    b.HasKey("JobsJobId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("JobTag");
                });

            modelBuilder.Entity("LebUpwor.core.Models.AppliedToTask", b =>
                {
                    b.Property<int>("AppliedToTaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppliedToTaskId"));

                    b.Property<DateTime>("AppliedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AppliedToTaskId");

                    b.HasIndex("JobId");

                    b.HasIndex("UserId");

                    b.ToTable("AppliedToTasks");
                });

            modelBuilder.Entity("LebUpwor.core.Models.CashOutHistory", b =>
                {
                    b.Property<int>("CashOutHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CashOutHistoryId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CashOutHistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("CashOutHistories");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FinishedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FinishedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<double>("Offer")
                        .HasColumnType("float");

                    b.Property<DateTime>("PostedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("JobId");

                    b.HasIndex("FinishedByUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("MessageId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Report", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFinishJobFailure")
                        .HasColumnType("bit");

                    b.Property<int?>("ReportedById")
                        .HasColumnType("int");

                    b.Property<int?>("ReportedMessageId")
                        .HasColumnType("int");

                    b.Property<int?>("ReportedPostId")
                        .HasColumnType("int");

                    b.Property<int?>("ReportedUserId")
                        .HasColumnType("int");

                    b.HasKey("ReportId");

                    b.HasIndex("ReportedById");

                    b.HasIndex("ReportedMessageId");

                    b.HasIndex("ReportedPostId");

                    b.HasIndex("ReportedUserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<int?>("AddedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.HasIndex("AddedByUserId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("LebUpwor.core.Models.TokenHistory", b =>
                {
                    b.Property<int>("TokenHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenHistoryId"));

                    b.Property<double>("AmountSent")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("TokenHistoryId");

                    b.HasIndex("JobId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("TokenHistories");
                });

            modelBuilder.Entity("LebUpwor.core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("CVpdf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GoogleAccountId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JoinedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastSeenDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Token")
                        .HasColumnType("float");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TagUser", b =>
                {
                    b.Property<int>("TagsTagId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("TagsTagId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("TagUser");
                });

            modelBuilder.Entity("JobTag", b =>
                {
                    b.HasOne("LebUpwor.core.Models.Job", null)
                        .WithMany()
                        .HasForeignKey("JobsJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LebUpwor.core.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LebUpwor.core.Models.AppliedToTask", b =>
                {
                    b.HasOne("LebUpwor.core.Models.Job", "Job")
                        .WithMany("AppliedUsers")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LebUpwor.core.Models.User", "User")
                        .WithMany("AppliedToTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LebUpwor.core.Models.CashOutHistory", b =>
                {
                    b.HasOne("LebUpwor.core.Models.User", "User")
                        .WithMany("CashOutHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Job", b =>
                {
                    b.HasOne("LebUpwor.core.Models.User", "FinishedByUser")
                        .WithMany("JobsFinished")
                        .HasForeignKey("FinishedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LebUpwor.core.Models.User", "User")
                        .WithMany("JobsPosted")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FinishedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Message", b =>
                {
                    b.HasOne("LebUpwor.core.Models.User", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LebUpwor.core.Models.User", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Report", b =>
                {
                    b.HasOne("LebUpwor.core.Models.User", "ReportedBy")
                        .WithMany()
                        .HasForeignKey("ReportedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LebUpwor.core.Models.Message", "ReportedMessage")
                        .WithMany()
                        .HasForeignKey("ReportedMessageId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LebUpwor.core.Models.Job", "ReportedPost")
                        .WithMany()
                        .HasForeignKey("ReportedPostId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LebUpwor.core.Models.User", "ReportedUser")
                        .WithMany()
                        .HasForeignKey("ReportedUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ReportedBy");

                    b.Navigation("ReportedMessage");

                    b.Navigation("ReportedPost");

                    b.Navigation("ReportedUser");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Tag", b =>
                {
                    b.HasOne("LebUpwor.core.Models.User", "AddedByUser")
                        .WithMany()
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("AddedByUser");
                });

            modelBuilder.Entity("LebUpwor.core.Models.TokenHistory", b =>
                {
                    b.HasOne("LebUpwor.core.Models.Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LebUpwor.core.Models.User", "Receiver")
                        .WithMany("ReceivedTokenHistories")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LebUpwor.core.Models.User", "Sender")
                        .WithMany("SentTokenHistories")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("LebUpwor.core.Models.User", b =>
                {
                    b.HasOne("LebUpwor.core.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TagUser", b =>
                {
                    b.HasOne("LebUpwor.core.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LebUpwor.core.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LebUpwor.core.Models.Job", b =>
                {
                    b.Navigation("AppliedUsers");
                });

            modelBuilder.Entity("LebUpwor.core.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("LebUpwor.core.Models.User", b =>
                {
                    b.Navigation("AppliedToTasks");

                    b.Navigation("CashOutHistory");

                    b.Navigation("JobsFinished");

                    b.Navigation("JobsPosted");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("ReceivedTokenHistories");

                    b.Navigation("SentMessages");

                    b.Navigation("SentTokenHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
