using System.Collections.Generic;
using GdscBackend.Models;
using GdscBackend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/members")]
    public class MembersController : ControllerBase
    {
        public static readonly List<MemberModel> MockMembers = new();


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MemberModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MemberModel>> Get()
        {
            return Ok(MockMembers);
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

            var existing = MockMembers.Find(e => e.Id == entity.Id);
            if (existing != null)
            {
                return BadRequest(new ErrorViewModel {Message = "An object with the same ID already exists"});
            }

            MockMembers.Add(entity);
            entity = MockMembers.Find(example => example == entity);

            return Created("api/members/" + entity!.Id, entity);
        }
    }
}