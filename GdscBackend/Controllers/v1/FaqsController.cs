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
    [Route("api/v1/faqs")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class FaqsController : ControllerBase
    {
        private readonly IRepository<FaqModel> _repository;

        public FaqsController(IRepository<FaqModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FaqModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FaqModel>> Get([FromRoute] string id)
        {
            var entity = await _repository.GetAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FaqModel), StatusCodes.Status201Created)]
        public async Task<ActionResult<FaqModel>> Post(FaqModel entity)
        {
            entity = await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(Post), new {entity.Id}, entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ExampleModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FaqModel>> Delete([FromRoute] string id)
        {
            var entity = await _repository.DeleteAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }
    }
}