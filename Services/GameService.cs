using Microsoft.EntityFrameworkCore;
using NobelTaskForInterview.Contexts;
using NobelTaskForInterview.Factories;
using NobelTaskForInterview.Models;

namespace NobelTaskForInterview.Services;

public class GameService : IGameService
{
    private readonly GameContext gameContext;
    public GameService(GameContext context)
    {
        gameContext = context;
    }

    /// <summary>
    /// Used for evaluating winer in a game move
    /// </summary>
    /// <param name="firstSign">First user sign</param>
    /// <param name="secondSign">Second user sign</param>
    /// <returns>Returns if first user won, lost or game was a draw</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Result DecideIfPlayerWon(Sign firstSign, Sign secondSign)
    {
        if (firstSign == secondSign)
        {
            return Result.Draw;
        }

        switch (firstSign)
        {
            case Sign.Rock:
                return (secondSign == Sign.Scissors) ? Result.Win : Result.Lose;
            case Sign.Paper:
                return (secondSign == Sign.Rock) ? Result.Win : Result.Lose;
            case Sign.Scissors:
                return (secondSign == Sign.Paper) ? Result.Win : Result.Lose;
            default:
                throw new InvalidOperationException("Invalid sign");
        }
    }
    public async Task<Move> PlayHand(Game game, Sign userSign) 
    {


        Random rand = new();
        Sign compSign = (Sign)rand.Next(0,3);

        Result result = DecideIfPlayerWon(userSign, compSign);

        Move newMove =  MoveFactory.CreateMove(game.Id,userSign,compSign,result);

        await gameContext.Moves.AddAsync(newMove);
        var savedChanges = await gameContext.SaveChangesAsync();
        
        if(savedChanges < 1 )
            throw new NobelException("EF error");

        return newMove;

    }
    public async Task<Game> FinishGame(int id)
    {
        Game? game = await gameContext.Games.FindAsync(id);

        if (game == null || game.GameFinished != null)
            throw new NobelException($"There is no game with id : {id}, or the game is already finished");

        game.GameFinished = DateTime.Now;

        gameContext.Update(game);
        var savedChanges = await gameContext.SaveChangesAsync();

        if (savedChanges > 0)
            return game;
        else
            throw new NobelException("EF error");
    }


    public async Task<Game> GetGameById(int id)
    {
        Game? game = await gameContext.Games.FindAsync(id);

        if (game == null)
            throw new NobelException("Game doesnt exist");

        return game;
    }

    public async Task<List<Game>> GetStatistic()
    {
        var stats = await gameContext.Games.Include(a => a.Moves).OrderByDescending(a => a.Id).ToListAsync();
        return stats;
    }

    public async Task<Game> StartGame()
    {
        Game newGame = GameFactory.CeateGame();

        await gameContext.AddAsync(newGame);
        var result = await gameContext.SaveChangesAsync();

        if (result > 0)
        {
            return newGame;
        }
        else
        {
            throw new NobelException("EF error");
        }

    }



}
