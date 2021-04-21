using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly List<ContactModel> _mockContact = new();

        
        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ContactModel), StatusCodes.Status201Created)]
        public ActionResult<ContactModel> Post([FromBody] ContactModel entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            //create a variable where we return the value of the find function applied on the _mockContact
            var doesExists = _mockContact.Find(p => p.Id == entity.Id);
            if (doesExists != null)
            {
                return BadRequest(new ErrorViewModel {Message = $"{entity} already exists"});
            }

            _mockContact.Add(entity);

            entity = _mockContact.Find(e => e == entity);
            return Ok(entity);
        }
    }
}