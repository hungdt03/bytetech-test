using ByteTech.Application.Contracts.Requests;
using ByteTech.Application.Features.Promotions.Apply;
using ByteTech.Application.Features.Promotions.Create;
using ByteTech.Application.Features.Promotions.Edit;
using ByteTech.Application.Features.Promotions.GetActives;
using ByteTech.Application.Features.Promotions.GetAll;
using ByteTech.Application.Features.Promotions.GetById;
using ByteTech.Application.Features.Promotions.GetUseds;
using ByteTech.Application.Features.Promotions.Remove;
using ByteTech.Application.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteTech.Presentation.Controllers
{
    [Route("api/promotions")]
    [ApiController]
    public class PromotionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string searchText = "")
        {
            var response = await _mediator.Send(new GetAllPromotionsQuery(searchText));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id, [FromBody] EditPromotionRequest request)
        {
            var response = await _mediator.Send(new EditPromotionCommand(id, request));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] string id)
        {
            var response = await _mediator.Send(new RemovePromotionCommand(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("status/active")]
        public async Task<IActionResult> GetAllActives()
        {
            var response = await _mediator.Send(new GetActivePromotionsQuery());
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetPromotionByIdQuery(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("useds/{userId}")]
        public async Task<IActionResult> GetUsedsByUserId([FromRoute] string userId)
        {
            var response = await _mediator.Send(new GetUsedPromotionsQuery(userId));
            return Ok(response);
        }

        [Authorize]
        [HttpGet("useds")]
        public async Task<IActionResult> GetUseds()
        {
            var userId = User.GetUserId();
            var response = await _mediator.Send(new GetUsedPromotionsQuery(userId));
            return Ok(response);
        }

        [Authorize]
        [HttpPost("apply/{promotionCode}")]
        public async Task<IActionResult> Apply([FromRoute] string promotionCode)
        {
            var userId = User.GetUserId();
            var response = await _mediator.Send(new ApplyPromotionCommand(userId, promotionCode));
            return Ok(response);
        }
    }
}
