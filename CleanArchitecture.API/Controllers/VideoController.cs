using CleanArchitecture.Application.Features.Videos.Queries.GetVideos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}", Name = "GetVideos")]
        [ProducesResponseType(typeof(IEnumerable<VideoVm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VideoVm>>> GetVideosByUser(string username)
        {
            var query = new GetVideosQuery(username);
            var videos = await _mediator.Send(query);
            return videos;
        }
    }
}
