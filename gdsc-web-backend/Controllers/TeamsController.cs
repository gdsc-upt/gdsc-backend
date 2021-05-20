using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]  
    [Route("api/[controller]")]  
    [Consumes("application/json")]
    [Produces("application/json")]
    public class TeamsController : ControllerBase
    {
        private readonly List<TeamModel> _mockTeams = new();

        [HttpGet]
        public List<TeamModel> Get()
        {
            return _mockTeams;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TeamModel), StatusCodes.Status201Created)]
        public ActionResult<TeamModel> Post([FromBody] TeamModel entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            var existing = _mockTeams.Find(e => e.Id == entity.Id);
            if (existing != null)
            {
                return BadRequest(new ErrorViewModel {Message = "An object with the same ID already exists"});
            }

            _mockTeams.Add(entity);
            entity = _mockTeams.Find(example => example == entity);

            return Created("api/Examples/" + entity!.Id, entity);
        }
        
        [HttpGet("{teamId}/members")]
        public List<MemberModel> GetMembers(string teamId)
        {
            return new List<MemberModel>
            {
                new MemberModel 
                {
                    Id = "1",
                    Name = "Gigel",
                    Email = "yahoo@gigel.com",
                    TeamId = teamId
                },
                new MemberModel 
                {
                    Id = "2",
                    Name = "Dorel",
                    Email = "dorel@gigel.com",
                    TeamId = teamId
                },
            };
        }
    }
}
