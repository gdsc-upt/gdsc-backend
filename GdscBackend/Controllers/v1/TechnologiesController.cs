﻿using System.Collections.Generic;
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
    [Route("api/v1/technologies")]
    [Consumes(MediaTypeNames.Application.Json)] 
    [Produces(MediaTypeNames.Application.Json)] 
    public class TechnologiesController : ControllerBase
    {
        private readonly IRepository<TechnologyModel> _repository;
        public TechnologiesController(IRepository<TechnologyModel> repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TechnologyModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TechnologyModel>> Post(TechnologyModel entity)
        {
            entity = await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Post), new {entity.Id}, entity);
        }
        
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