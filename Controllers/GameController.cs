using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NobelTaskForInterview.Contexts;
using NobelTaskForInterview.Models;

namespace NobelTaskForInterview.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    public GameController(GameContext context)
    {
        gameContext = context;
    }

    private readonly GameContext gameContext;

    [HttpGet]
    [Route("StartGame")]
    public async Task<IActionResult> StartGame()
    {
        Game newGame = new();

        await gameContext.AddAsync(newGame);
        var result = await gameContext.SaveChangesAsync();

        if (result > 0)
        {
            return Ok(newGame);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data");
        }
    }

    [HttpPost]
    [Route("PlayHand")]
    public async Task<IActionResult> PlayHand(int gameId, Sign userSign)
    {
        Game? game = gameContext.Games.Where(a => a.Id == gameId).FirstOrDefault();

        if (game == null || game.GameFinished != null )
            return StatusCode(StatusCodes.Status400BadRequest, $"There is no game with id : {gameId} , or the game has already finished");

        Random random = new();
        Sign computerSign = (Sign)random.Next(0, 3);

        try
        {
            Result result = GetResult(userSign, computerSign);
            Move move = new() {GameId = gameId, PlayerSign = userSign, ComputerSign = computerSign, Result = result };

            await gameContext.Moves.AddAsync(move);
            var savedChanges =  await gameContext.SaveChangesAsync();

            if (savedChanges > 0)
                return Ok(move);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data");

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpPost]
    [Route("EndGame")]
    public async Task<IActionResult> EndGame(int gameId)
    {
        Game? game = await gameContext.Games.Where(a => a.Id == gameId).FirstOrDefaultAsync();

        if (game == null)
            return StatusCode(StatusCodes.Status400BadRequest, $"There is no game with id : {gameId}");

        game.GameFinished = DateTime.Now;

        gameContext.Update(game);
        var savedChanges = await gameContext.SaveChangesAsync();

        if (savedChanges > 0)
            return Ok(game);
        else
            return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data");
    }

    [HttpGet]
    [Route("GetStatistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var stats = (await gameContext.Games.Include(a => a.Moves).ToListAsync()).OrderByDescending(a => a.Id);
        return Ok(stats);
    }

    private Result GetResult(Sign userSign, Sign computerSign)
    {
        if (userSign == computerSign)
        {
            return Result.Draw;
        }

        switch (userSign)
        {
            case Sign.Rock:
                return (computerSign == Sign.Scissors) ? Result.Win : Result.Lose;
            case Sign.Paper:
                return (computerSign == Sign.Rock) ? Result.Win : Result.Lose;
            case Sign.Scissors:
                return (computerSign == Sign.Paper) ? Result.Win : Result.Lose;
            default:
                throw new InvalidOperationException("Invalid sign");
        }
    }
}