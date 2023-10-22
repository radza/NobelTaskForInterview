namespace NobelTaskForInterview.Models;

public class Move
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public Sign PlayerSign { get; set; }
    public Sign ComputerSign { get; set; }
    public Result Result { get; set; }

    public Game Game { get; set; } = null!;
}
