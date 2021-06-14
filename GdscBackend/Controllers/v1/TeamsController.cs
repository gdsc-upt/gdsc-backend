﻿using System.Collections.Generic;
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
    [Route("v1/teams")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class TeamsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TeamModel> _repository;

        public TeamsController(IRepository<TeamModel> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TeamModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamModel>> Get([FromRoute] string id)
        {
            var entity = await _repository.GetAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamModel>> Post(TeamRequest entity)
        {
            entity = Map(await _repository.AddAsync(Map(entity)));

            return CreatedAtAction(nameof(Post), new { Map(entity).Id }, entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamModel>> Delete([FromRoute] string id)
        {
            var entity = await _repository.DeleteAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }
        private TeamModel Map(TeamRequest entity)
        {
            return _mapper.Map<TeamModel>(entity);
        }

        private TeamRequest Map(TeamModel entity)
        {
            return _mapper.Map<TeamRequest>(entity);
        }

        private IEnumerable<TeamRequest> Map(IEnumerable<TeamModel> entity)
        {
            return _mapper.Map<IEnumerable<TeamRequest>>(entity);
        }

        private IEnumerable<TeamModel> Map(IEnumerable<TeamRequest> entity)
        {
            return _mapper.Map<IEnumerable<TeamModel>>(entity);
        }
    }
}