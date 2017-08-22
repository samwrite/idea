using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using idea.Models;

namespace idea.Migrations
{
    [DbContext(typeof(IdeaContext))]
    [Migration("20170822212542_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("idea.Models.Idea", b =>
                {
                    b.Property<int>("IdeaId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("CreatorUserId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("content");

                    b.HasKey("IdeaId");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("idea.Models.Liked", b =>
                {
                    b.Property<int>("LikedId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("IdeaId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("LikedId");

                    b.HasIndex("IdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Likeds");
                });

            modelBuilder.Entity("idea.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("idea.Models.Idea", b =>
                {
                    b.HasOne("idea.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");
                });

            modelBuilder.Entity("idea.Models.Liked", b =>
                {
                    b.HasOne("idea.Models.Idea", "Idea")
                        .WithMany("Likeds")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("idea.Models.User", "User")
                        .WithMany("Likeds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
