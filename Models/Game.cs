namespace NobelTaskForInterview.Models;

public class Game
{   
    public int Id { get; set; }
    public DateTime GameStarted { get; set; }
    public DateTime? GameFinished { get; set; }
    public List<Move> Moves { get; set; } = new List<Move>();
}
