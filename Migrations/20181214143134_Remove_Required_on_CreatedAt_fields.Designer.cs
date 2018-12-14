﻿// <auto-generated />
using System;
using AngularCore.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AngularCore.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20181214143134_Remove_Required_on_CreatedAt_fields")]
    partial class Remove_Required_on_CreatedAt_fields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AngularCore.Data.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("ModifiedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("PostId")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("AngularCore.Data.Models.Post", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Post");
                });

            modelBuilder.Entity("AngularCore.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("ProfilePictureId");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.HasIndex("ProfilePictureId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AngularCore.Data.Models.UserFriends", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("FriendId");

                    b.Property<DateTime>("CreatedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("ModifiedAt")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("UserId", "FriendId");

                    b.HasAlternateKey("FriendId", "UserId");

                    b.ToTable("UserFriends");
                });

            modelBuilder.Entity("AngularCore.Data.Models.Image", b =>
                {
                    b.HasBaseType("AngularCore.Data.Models.Post");

                    b.Property<string>("MediaUrl")
                        .IsRequired();

                    b.ToTable("Image");

                    b.HasDiscriminator().HasValue("Image");
                });

            modelBuilder.Entity("AngularCore.Data.Models.Comment", b =>
                {
                    b.HasOne("AngularCore.Data.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AngularCore.Data.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AngularCore.Data.Models.Post", b =>
                {
                    b.HasOne("AngularCore.Data.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngularCore.Data.Models.User", b =>
                {
                    b.HasOne("AngularCore.Data.Models.Image", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId");
                });

            modelBuilder.Entity("AngularCore.Data.Models.UserFriends", b =>
                {
                    b.HasOne("AngularCore.Data.Models.User", "Friend")
                        .WithMany("FriendUsers")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AngularCore.Data.Models.User", "User")
                        .WithMany("UserFriends")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
