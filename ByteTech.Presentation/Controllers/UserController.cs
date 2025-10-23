using ByteTech.Application.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteTech.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] Application.Features.Users.Create.Command command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string searchText = "")
        {
            var response = await _mediator.Send(new Application.Features.Users.GetAll.Query(searchText));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var response = await _mediator.Send(new Application.Features.Users.GetById.Query(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("lock/{id}")]
        public async Task<IActionResult> LockAccount([FromRoute] string id)
        {
            var response = await _mediator.Send(new Application.Features.Users.Lock.Command(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("unlock/{id}")]
        public async Task<IActionResult> UnlockAccount([FromRoute] string id)
        {
            var response = await _mediator.Send(new Application.Features.Users.Unlock.Command(id));
            return Ok(response);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetUserDetails()
        {
            var userId = HttpContext.User.GetUserId();
            var response = await _mediator.Send(new Application.Features.Users.GetById.Query(userId));
            return Ok(response);
        }
    }
}
