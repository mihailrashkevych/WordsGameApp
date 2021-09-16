﻿// <auto-generated />
using GameApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameApp.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20210916092202_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameApp.Models.User", b =>
                {
                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Phone");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameApp.Models.WordsForUser", b =>
                {
                    b.Property<string>("UserPhone")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserPhone", "Word");

                    b.ToTable("WordsForUsers");
                });

            modelBuilder.Entity("GameApp.Models.WordsForUser", b =>
                {
                    b.HasOne("GameApp.Models.User", null)
                        .WithMany("WordsForUsers")
                        .HasForeignKey("UserPhone")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameApp.Models.User", b =>
                {
                    b.Navigation("WordsForUsers");
                });
#pragma warning restore 612, 618
        }
    }
}