using System.Collections.Generic;
using System.Threading.Tasks;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.ViewModels;
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
        public async Task<ActionResult<IEnumerable<ContactModel>>> Post(ContactModel entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            await _repository.AddAsync(entity);

            return Ok(entity);
        }
    }
}
