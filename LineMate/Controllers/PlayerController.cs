using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using LineMate.Models;
using LineMate.Repositories;

namespace LineMate.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_playerRepository.GetAllPlayers());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var player = _playerRepository.GetPlayerById(id);
            if(player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost]
        public IActionResult Add(Player player)
        {
            _playerRepository.Add(player);
            return CreatedAtAction("Get", new { id = player.Id }, player);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _playerRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }
            _playerRepository.Update(player);
            return NoContent();
        }
    }
}