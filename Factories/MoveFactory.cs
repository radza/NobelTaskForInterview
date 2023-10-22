using NobelTaskForInterview.Models;

namespace NobelTaskForInterview.Factories;

public class MoveFactory
{
    public static Move CreateMove(int gameId, Sign playerSign, Sign computerSign, Result result)
    {
        return new Move() { GameId = gameId, PlayerSign = playerSign, ComputerSign = computerSign, Result = result };
    }
    public static Move CreateMove( Sign playerSign, Sign computerSign, Result result)
    {
        return new Move() {  PlayerSign = playerSign, ComputerSign = computerSign, Result = result };
    }
    public static Move CreateMove()
    {
        return new Move();
    }
}
