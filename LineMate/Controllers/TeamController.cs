using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System;
using LineMate.Models;
using LineMate.Repositories;

namespace LineMate.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public TeamController(ITeamRepository teamRepository, IUserProfileRepository userProfileRepository)
        {
            _teamRepository = teamRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_teamRepository.GetAllTeams());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var team = _teamRepository.GetTeamById(id);
            if(team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        public IActionResult Add(Team team)
        {
            var currentUser = GetCurrentUserProfileId();

            team.CreatedByUserProfileId = currentUser.Id;
            _teamRepository.Add(team);
            return CreatedAtAction("Get", new { id = team.Id }, team);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _teamRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Team team)
        {
            var currentUser = GetCurrentUserProfileId();
            if (id != team.Id)
            {
                return BadRequest();
            }
            team.CreatedByUserProfileId = currentUser.Id;
            _teamRepository.Update(team);
            return NoContent();
        }

        private UserProfile GetCurrentUserProfileId()
        {
            var firebaseUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userProfileRepository.GetByFireBaseUserId(firebaseUserId);
        }
    }
}