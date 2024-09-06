using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class New : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromKeyedServices("GameServices")] IGameServices gameServices, NewGameRequest newGameRequest)
        {
            GameState gameState = gameServices.NewGame(newGameRequest);
            if(!string.IsNullOrEmpty(gameState.Error))
                return BadRequest(gameState.Error);
            return Ok(new GameInfoResponse(gameState));
        }
    }
}
