using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize(Roles = "admin")]
    [Route("v1/faqs")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class FaqsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<FaqModel> _repository;

        public FaqsController(IRepository<FaqModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FaqRequest>>> Get()
        {
            return Ok(Map((await _repository.GetAsync()).ToList()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FaqRequest>> Get([FromRoute] string id)
        {
            var entity = Map(await _repository.GetAsync(id));

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<FaqRequest>> Post(FaqRequest entity)
        {
            entity = Map(await _repository.AddAsync(Map(entity)));

            return CreatedAtAction(nameof(Post), new {Map(entity).Id}, entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<FaqRequest>> Delete([FromRoute] string id)
        {
            var entity = Map(await _repository.DeleteAsync(id));

            return entity is null ? NotFound() : Ok(entity);
        }

        private FaqModel Map(FaqRequest entity)
        {
            return _mapper.Map<FaqModel>(entity);
        }

        private FaqRequest Map(FaqModel entity)
        {
            return _mapper.Map<FaqRequest>(entity);
        }

        private IEnumerable<FaqRequest> Map(IEnumerable<FaqModel> entity)
        {
            return _mapper.Map<IEnumerable<FaqRequest>>(entity);
        }

        private IEnumerable<FaqModel> Map(IEnumerable<FaqRequest> entity)
        {
            return _mapper.Map<IEnumerable<FaqModel>>(entity);
        }
    }
}
