using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]  
    [Route("api/[controller]")]  
    public class TeamsController : ControllerBase
    {
        private readonly List<TeamModel> _mockTeams = new List<TeamModel>
        {
            new()
            {
                Id = "1",
                Name = "FCSB"
            },
            new()
            {
                Id = "2",
                Name = "asfasf"
            }
        };
        
        [HttpGet]
        public ActionResult<TeamModel> Get()
        {
            return Ok(_mockTeams);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExampleModel), StatusCodes.Status201Created)]
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
            entity = _mockTeams.Find(team => team == entity);

            return Created("api/Teams/" + entity!.Id, entity);
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
