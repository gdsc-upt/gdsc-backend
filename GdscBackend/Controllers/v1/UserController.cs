using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using GdscBackend.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GdscBackend.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v1/roles")]
    [Authorize(Roles = "admin")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize(Roles = "admin")]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Role>>> GetByRole(string roleName)
        {
            //return Ok((await _repository.GetAsync()).ToList());
            return Ok(await _userManager.GetUsersInRoleAsync(roleName));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Role>> Get([FromRoute] string id)
        {
            var entity = await _userManager.FindByIdAsync(id);

            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Post(User entity)
        {
            var result = await _userManager.CreateAsync(entity);
            //entity = await _userManager.CreateAsync(entity);

            if (result.Succeeded) return await _userManager.FindByNameAsync(entity.UserName);

            return BadRequest(result.Errors);

            //return CreatedAtAction(nameof(Post), new {result.Id}, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Delete([FromRoute] string id)
        {
            //var entity = await _repository.DeleteAsync(id);
            var entity = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));

            return entity is null ? NotFound() : Ok(entity);
        }
    }
}