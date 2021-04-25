using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/teams")]
    public class TeamsController : ControllerBase
    {
        [HttpGet]
        public List<TeamModel> Get()
        {
            return new()
            {
                new TeamModel
                {
                    Id = "1",
                    Name = "FCB"
                },
                new TeamModel()
                {
                    Id = "2",
                    Name = "LFC"
                }
            };
        }

        [HttpGet("{teamId}/members")]
        public List<MemberModel> GetMembers(string teamId)
        {
            return new()
            {
                new MemberModel
                {
                    Id = "1",
                    Name = "Gigel",
                    Email = "yahoo@gigel.com",
                    TeamId = teamId
                },
                new MemberModel()
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