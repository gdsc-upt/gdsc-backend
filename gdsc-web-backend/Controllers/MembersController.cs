using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private static List<MemberModel> members = new List<MemberModel>
        {
            new MemberModel 
            {
                Id = "1",
                Name = "Gigel",
                Email = "yahoo@gigel.com",
                TeamId = "1"
            },
            new MemberModel 
            {
                Id = "2",
                Name = "Dorel",
                Email = "dorel@gigel.com",
                TeamId = "2"
            }
        };


        [HttpGet]
        public List<MemberModel> Get()
        {
            return members;
        }

    }
}
