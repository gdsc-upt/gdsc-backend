using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using GdscBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [Authorize(Roles = "admin")]
    [ApiVersion("1")]
    [Route("v1/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ContactModel> _repository;

        public ContactController(IRepository<ContactModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ContactRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ContactRequest>>> Post(ContactRequest entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            entity = Map(await _repository.AddAsync(Map(entity)));

            return CreatedAtAction(nameof(Post), new {Map(entity).Id}, entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ContactRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactRequest>> Delete([FromRoute] string id)
        {
            var entity = Map(await _repository.DeleteAsync(id));

            return entity is null ? NotFound() : Ok(entity);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ContactRequest>>> Get()
        {
            return Ok(Map((await _repository.GetAsync()).ToList()));
        }

        [HttpDelete]
        public async Task<ActionResult<ContactRequest>> Delete(string[] ids)
        {
            var entity = Map(await _repository.DeleteAsync(ids));
            return entity is null ? NotFound() : Ok(entity);
        }

        private ContactModel Map(ContactRequest entity)
        {
            return _mapper.Map<ContactModel>(entity);
        }

        private ContactRequest Map(ContactModel entity)
        {
            return _mapper.Map<ContactRequest>(entity);
        }

        private IEnumerable<ContactRequest> Map(IEnumerable<ContactModel> entity)
        {
            return _mapper.Map<IEnumerable<ContactRequest>>(entity);
        }

        private IEnumerable<ContactModel> Map(IEnumerable<ContactRequest> entity)
        {
            return _mapper.Map<IEnumerable<ContactModel>>(entity);
        }
    }
}
