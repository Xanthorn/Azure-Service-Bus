using API.Sender.CQRS.Commands;
using API.Sender.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Sender.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _mediator.Send(new GetUsers.Query());
            return Ok(result);
        }

        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> AddUser([FromBody] AddUser.Command request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPatch]
        [Route("users/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUser.Command request)
        {
            request = request with { Id = id };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPatch]
        [Route("users/{id}/activate")]
        public async Task<IActionResult> ActivateUser([FromRoute] int id, [FromBody] ActivateUser.Command request)
        {
            request = request with { Id = id };
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
