using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gdsc_web_backend.Database;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers.v1
{
    public class TechnologyController : ControllerBase
    {
        private readonly IRepository<TechnologyModel> _repository;
        public TechnologyController(IRepository<TechnologyModel> repository)
        {
            _repository = repository;
        }
        
        //get all
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TechnologyModel>>> GetAll()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }
        
        //post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TechnologyModel>> Post(TechnologyModel entity)
        {
            entity = await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Post), new {entity.Id}, entity);
        }
        
        //delete
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<TechnologyModel>> Delete([FromRoute] string id)
        {
            var entity = await _repository.DeleteAsync(id);
            return entity is null ? NotFound() : Ok(entity);
        }
        
        


    }
}