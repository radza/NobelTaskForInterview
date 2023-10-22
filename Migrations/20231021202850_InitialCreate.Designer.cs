using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using NobelTaskForInterview.Contexts;
using NobelTaskForInterview.Models;


#nullable disable

namespace NobelTaskForInterview.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20231021202850_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("NobelTaskForInterviewDomain.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<DateTime?>("GameFinished")
                        .HasColumnType("text")
                        .HasColumnName("game_finished_at");

                    b.Property<DateTime>("GameStarted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("game_started_at")
                        .HasDefaultValueSql("datetime('now')");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("NobelTaskForInterviewDomain.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<Sign>("ComputerSign")
                        .HasColumnType("integer")
                        .HasColumnName("computer_sign");

                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<Sign>("PlayerSign")
                        .HasColumnType("integer")
                        .HasColumnName("player_sign");

                    b.Property<Result>("Result")
                        .HasColumnType("integer")
                        .HasColumnName("result");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("NobelTaskForInterviewDomain.Move", b =>
                {
                    b.HasOne("NobelTaskForInterviewDomain.Game", "Game")
                        .WithMany("Moves")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("NobelTaskForInterviewDomain.Game", b =>
                {
                    b.Navigation("Moves");
                });
#pragma warning restore 612, 618
        }
    }
}
