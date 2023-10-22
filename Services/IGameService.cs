using NobelTaskForInterview.Models;

namespace NobelTaskForInterview.Services;

public interface IGameService
{
    Task<Game> GetGameById(int id);
    Task<Game> StartGame();
    Task<Game> FinishGame(int id);
    Result DecideIfPlayerWon(Sign gameId, Sign playerSign);
    Task<List<Game>> GetStatistic();
    Task<Move> PlayHand(Game game, Sign userSign);
}
