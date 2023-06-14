using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideos
{
    public class GetVideosQuery : IRequest<List<VideoVm>>
    {
        public string Username { get; set; } = string.Empty;

        public GetVideosQuery(string username) 
        { 
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
