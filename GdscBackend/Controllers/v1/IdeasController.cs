using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Models;
using GdscBackend.RequestModels;
using GdscBackend.Utils;
using GdscBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v1/ideas")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class IdeasController : ControllerBase
    {
        private readonly IRepository<IdeaModel> _repository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _sender;

        public IdeasController(IRepository<IdeaModel> repository, IEmailSender sender, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _sender = sender;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<IdeaModel>>> Get()
        {
            return Ok((await _repository.GetAsync()).ToList());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IdeaModel>> Get([FromRoute] string id)
        {
            var entity = await _repository.GetAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdeaModel>> Post(IdeaRequest entity)
        {
            if (entity is null)
            {
                return BadRequest(new ErrorViewModel {Message = "Request has no body"});
            }

            if (!new EmailAddressAttribute().IsValid(entity.Email))
            {
                return BadRequest(new ErrorViewModel {Message = "Invalid email provided"});
            }

            var newEntity = await _repository.AddAsync(Map(entity));

            _sender.SendEmail(newEntity.Email, "Google Developer Student Clubs UPT Idea sent!",
                "Thanks for submitting your idea! \nWe will contact you as soon as possible!");

            return Created("v1/idea", newEntity);
        }

        private IdeaModel Map(IdeaRequest entity)
        {
            return _mapper.Map<IdeaModel>(entity);
        }
    }
}
