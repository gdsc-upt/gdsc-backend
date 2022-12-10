using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GdscBackend.Database;
using GdscBackend.Utils;
using GdscBackend.Utils.Services;
using GdscBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Features.Contacts;

[ApiController]
[Authorize(Roles = "admin")]
[ApiVersion("1")]
[Route("v1/contact")]
public class ContactController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<ContactModel> _repository;
    private readonly IEmailSender _sender;
    private readonly IWebhookService _webhookService;

    public ContactController(IRepository<ContactModel> repository, IMapper mapper, IEmailSender sender,
        IWebhookService webhookService)
    {
        _repository = repository;
        _mapper = mapper;
        _sender = sender;
        _webhookService = webhookService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ContactRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ContactModel>>> Post(ContactRequest entity)
    {
        if (entity is null)
        {
            return BadRequest(new ErrorViewModel { Message = "Request has no body" });
        }

        if (!new EmailAddressAttribute().IsValid(entity.Email))
        {
            return BadRequest(new ErrorViewModel { Message = "Invalid email provided" });
        }

        var newEntity = await _repository.AddAsync(Map(entity));

        _sender.SendEmail(entity.Email, entity.Subject, entity.Text);

        _webhookService.SendContact(newEntity);

        return Created("v1/contact", newEntity);
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

    private ContactModel Map(ContactRequest entity)
    {
        return _mapper.Map<ContactModel>(entity);
    }

    private ContactRequest Map(ContactModel entity)
    {
        return _mapper.Map<ContactRequest>(entity);
    }

    private IEnumerable<ContactRequest> Map(IEnumerable<ContactModel> entity)
    {
        return _mapper.Map<IEnumerable<ContactRequest>>(entity);
    }

    private IEnumerable<ContactModel> Map(IEnumerable<ContactRequest> entity)
    {
        return _mapper.Map<IEnumerable<ContactModel>>(entity);
    }
}