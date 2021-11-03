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
    public class SpecialTeamsController : ControllerBase
    {
        private readonly ISpecialTeamsRepository _specialTeamsRepository;

        public SpecialTeamsController(ISpecialTeamsRepository specialTeamsRepository)
        {
            _specialTeamsRepository = specialTeamsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_specialTeamsRepository.GetAllSpecialTeams());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var player = _specialTeamsRepository.GetSpecialTeamsById(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost]
        public IActionResult Add(SpecialTeams specialTeams)
        {
            _specialTeamsRepository.Add(specialTeams);
            return CreatedAtAction("Get", new { id = specialTeams.Id }, specialTeams);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _specialTeamsRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, SpecialTeams specialTeams)
        {
            if (id != specialTeams.Id)
            {
                return BadRequest();
            }
            _specialTeamsRepository.Update(specialTeams);
            return NoContent();
        }
    }
}