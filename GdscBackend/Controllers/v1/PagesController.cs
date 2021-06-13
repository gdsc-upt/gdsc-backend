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
    [ApiVersion("1")]
    [Authorize(Roles = "admin")]
    [Route("v1/pages")]
    public class PagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PageModel> _repository;

        public PagesController(IRepository<PageModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PageRequest>>> Get()
        {
            return Ok(Map((await _repository.GetAsync()).ToList()));
        }


        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PageRequest), StatusCodes.Status201Created)]
        public async Task<ActionResult<PageRequest>> Post(PageRequest entity)
        {
            entity = Map(await _repository.AddAsync(Map(entity)));

            return CreatedAtAction(nameof(Post), new {Map(entity).Id}, entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PageRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageRequest>> Delete([FromRoute] string id)
        {
            var entity = Map(await _repository.DeleteAsync(id));

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MenuItemRequest>> Update(PageRequest entity)
        {
            entity = Map(await _repository.UpdateAsync(Map(entity)));

            return CreatedAtAction(nameof(Update), new {Map(entity).Id}, entity);
        }

        private PageModel Map(PageRequest entity)
        {
            return _mapper.Map<PageModel>(entity);
        }

        private PageRequest Map(PageModel entity)
        {
            return _mapper.Map<PageRequest>(entity);
        }

        private IEnumerable<PageRequest> Map(IEnumerable<PageModel> entity)
        {
            return _mapper.Map<IEnumerable<PageRequest>>(entity);
        }

        private IEnumerable<PageModel> Map(IEnumerable<PageRequest> entity)
        {
            return _mapper.Map<IEnumerable<PageModel>>(entity);
        }
    }
}