using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DirectorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateDirector")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> CreateDirector(CreateDirectorCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
