using ByteTech.Application.Features.Auth.SignIn;
using ByteTech.Application.Features.Auth.SignUp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteTech.Presentation.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost(nameof(SignIn))]
        public async Task<IActionResult> SignIn([FromBody] SignInQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost(nameof(SignUp))]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
