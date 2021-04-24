using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")] // specifies which type of data this controller accepts
    [Produces("application/json")] // specifies which type of data this controller returns
    public class MembersController : ControllerBase
    {
        public static List<MemberModel> _mockMembers = new();

        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MemberModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MemberModel>> Get()
        {
            return Ok(_mockMembers);
        }
        
        
        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MemberModel), StatusCodes.Status201Created)]
        public ActionResult<MemberModel> Post([FromBody] MemberModel entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            var existing = _mockMembers.Find(e => e.Id == entity.Id);
            if (existing != null)
            {
                return BadRequest(new ErrorViewModel {Message = "An object with the same ID already exists"});
            }

            _mockMembers.Add(entity);
            entity = _mockMembers.Find(example => example == entity);

            return Created("api/Members/" + entity!.Id, entity);
        }
        
    }
}
