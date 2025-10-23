using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteTech.Presentation.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost(nameof(SignIn))]
        public async Task<IActionResult> SignIn([FromBody] Application.Features.Auth.SignIn.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost(nameof(SignUp))]
        public async Task<IActionResult> SignUp([FromBody] Application.Features.Auth.SignUp.Command command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
