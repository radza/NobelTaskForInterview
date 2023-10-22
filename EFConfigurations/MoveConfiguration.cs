using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NobelTaskForInterview.Models;

namespace NobelTaskForInterview.EFConfigurations;

public class MoveConfiguration : IEntityTypeConfiguration<Move>
{

    public void Configure(EntityTypeBuilder<Move> b)
    {

        b.Property(a => a.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("integer")
            .HasColumnName("id");

        b.Property(a => a.PlayerSign)
            .HasConversion<int>()
            .HasColumnType("integer")
            .HasColumnName("player_sign")
            .IsRequired();

        b.Property(a => a.ComputerSign)
            .HasConversion<int>()
            .HasColumnType("integer")
            .HasColumnName("computer_sign")
            .IsRequired();

        b.Property(a => a.Result)
            .HasConversion<int>()
            .HasColumnType("integer")
            .HasColumnName("result")
            .IsRequired();

        b.Property(a => a.GameId)
            .HasColumnType("integer")
            .HasColumnName("game_id")
            .IsRequired();

        b.HasKey(a => a.Id);

        b.HasOne(a => a.Game)
            .WithMany(a => a.Moves)
            .HasForeignKey(a => a.GameId)
            .IsRequired();
    }
}
