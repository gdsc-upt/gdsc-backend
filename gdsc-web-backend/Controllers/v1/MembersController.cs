using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using gdsc_web_backend.Database;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/members")]
    [Consumes(MediaTypeNames.Application.Json)] 
    [Produces(MediaTypeNames.Application.Json)] 
    public class MembersController : ControllerBase
    {
        private readonly IRepository<MemberModel> _repository;

        public MembersController(IRepository<MemberModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MemberModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MemberModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }


        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MemberModel), StatusCodes.Status201Created)]
        public async Task<ActionResult<MemberModel>> Post(MemberModel entity)
        {
            entity = await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Post), new {entity.Id}, entity);
        }
    }
}