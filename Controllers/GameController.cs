using Microsoft.AspNetCore.Mvc;
using NobelTaskForInterview.Models;
using NobelTaskForInterview.Services;

namespace NobelTaskForInterview.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService gameService;

    public GameController(IGameService service)
    {
        gameService = service;
    }

    [HttpGet]
    [Route("StartGame")]
    public async Task<IActionResult> StartGame()
    {
        var game = await gameService.StartGame();
        return Ok(game);
    }

    [HttpPost]
    [Route("PlayHand")]
    public async Task<IActionResult> PlayHand(int gameId, Sign userSign)
    {
        var game = await gameService.GetGameById(gameId);
        if (game.GameFinished != null)
            throw new NobelException($"Game with id : {gameId} is already finished");

        var move = await gameService.PlayHand(game, userSign);
        return Ok(move);
    }

    [HttpPost]
    [Route("EndGame")]
    public async Task<IActionResult> EndGame(int gameId)
    {
        var game = await gameService.GetGameById(gameId);
        if (game.GameFinished != null)
            throw new NobelException($"Game with id : {gameId} is already finished");

        return Ok( await gameService.FinishGame(gameId));
    }

    [HttpGet]
    [Route("GetStatistics")]
    public async Task<IActionResult> GetStatistics()
    {
        return Ok(await gameService.GetStatistic());
    }


}