using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gdsc_web_backend.Database;
using GdscBackend.Models;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v1/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IRepository<ContactModel> _repository;

        public ContactController(IRepository<ContactModel> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ContactModel>>> Post(ContactModel entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            entity = await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Post), new {entity.Id}, entity);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ContactModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactModel>> Delete([FromRoute] string id)
        {
            var entity = await _repository.DeleteAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ContactModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }

        [HttpDelete]
        public async Task<ActionResult<ContactModel>> Delete(string[] ids)
        {
            var entity = await _repository.DeleteAsync(ids);
            return entity is null ? NotFound() : Ok(entity);
        }
        
    }
}
