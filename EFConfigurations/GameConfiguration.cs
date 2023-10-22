using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NobelTaskForInterview.Models;

namespace NobelTaskForInterview.EFConfigurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{

    public void Configure(EntityTypeBuilder<Game> b)
    {
        b.Property(a => a.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("integer")
            .HasColumnName("id");

        b.Property(a => a.GameStarted)
            .HasDefaultValueSql("datetime('now')")
            .HasColumnType("text") 
            .HasColumnName("game_started_at");

        b.Property(a => a.GameFinished)
            .HasColumnType("text") 
            .HasColumnName("game_finished_at")
            .IsRequired(false);

        b.HasKey(a => a.Id);

        b.HasMany(a => a.Moves)
            .WithOne(a => a.Game)
            .HasForeignKey(a => a.GameId)
            .IsRequired();
    }

}
