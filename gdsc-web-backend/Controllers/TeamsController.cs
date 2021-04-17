using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]  
    [Route("api/[controller]")]  
    public class TeamsController : ControllerBase
    {
        [HttpGet]
        public List<TeamModel> Get()
        {
            return new List<TeamModel>
            {
                new TeamModel
                {
                    Id = "1",
                    Name = "FCB"
                },
                new TeamModel
                {
                    Id = "2",
                    Name = "LFC"
                }
            };
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
                }
            };
        }
    }
}
