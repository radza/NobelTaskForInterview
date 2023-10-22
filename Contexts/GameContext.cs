using Microsoft.EntityFrameworkCore;
using NobelTaskForInterview.EFConfigurations;
using NobelTaskForInterview.Models;


namespace NobelTaskForInterview.Contexts;

public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Move> Moves { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new MoveConfiguration());
    }
}
