using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GdscBackend.Database;
using GdscBackend.Email;
using GdscBackend.Models;
using GdscBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [Authorize(Roles = "admin")]
    [ApiVersion("1")]
    [Route("v1/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IRepository<ContactModel> _repository;
        private readonly EmailSender _sender;

        public ContactController(IRepository<ContactModel> repository, EmailSender sender)
        {
            _repository = repository;
            _sender = sender;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ContactModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ContactModel>>> Post(ContactModel entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel { Message = "Request has no body" });
            }
            
            var isValid = new EmailAddressAttribute().IsValid(entity.Email);

            if (!isValid)
            {
                return BadRequest(new ErrorViewModel { Message = "Invalid email provided" });
            }
            
            entity = await _repository.AddAsync(entity);
            
            _sender.SendEmail(entity.Email, entity.Subject, entity.Text);
            
            return CreatedAtAction(nameof(Post), new { entity.Id }, entity);
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