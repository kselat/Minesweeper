using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Turn : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromKeyedServices("GameServices")] IGameServices gameServices, GameTurnRequest gameTurnRequest)
        {
            GameState gameState = gameServices.Turn(gameTurnRequest);
            if (!string.IsNullOrEmpty(gameState.Error))
                return BadRequest(gameState.Error);
            return Ok(new GameInfoResponse(gameState));
        }
    }
}
