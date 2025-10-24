using ByteTech.Application.Contracts.Requests;
using ByteTech.Application.Features.Users.Create;
using ByteTech.Application.Features.Users.Edit;
using ByteTech.Application.Features.Users.GetAll;
using ByteTech.Application.Features.Users.GetById;
using ByteTech.Application.Features.Users.Lock;
using ByteTech.Application.Features.Users.Unlock;
using ByteTech.Application.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteTech.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string searchText = "")
        {
            var response = await _mediator.Send(new GetAllUsersQuery(searchText));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("lock/{id}")]
        public async Task<IActionResult> LockAccount([FromRoute] string id)
        {
            var response = await _mediator.Send(new LockUserCommand(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("unlock/{id}")]
        public async Task<IActionResult> UnlockAccount([FromRoute] string id)
        {
            var response = await _mediator.Send(new UnlockUserCommand(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("edit/{userId}")]
        public async Task<IActionResult> Edit([FromRoute] string userId, [FromBody] EditUserRequest request)
        {
            var response = await _mediator.Send(new EditUserCommand(userId, request));
            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUserDetails()
        {
            var userId = HttpContext.User.GetUserId();
            var response = await _mediator.Send(new GetUserByIdQuery(userId));
            return Ok(response);
        }

        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> EditMe([FromBody] EditUserRequest request)
        {
            var userId = HttpContext.User.GetUserId();
            var response = await _mediator.Send(new EditUserCommand(userId, request));
            return Ok(response);
        }
    }
}
