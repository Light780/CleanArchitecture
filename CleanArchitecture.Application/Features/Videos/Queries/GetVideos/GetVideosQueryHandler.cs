using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideos
{
    public class GetVideosQueryHandler : IRequestHandler<GetVideosQuery, List<VideoVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<VideoVm>> Handle(GetVideosQuery request, CancellationToken cancellationToken)
        {
            var videoList = await _unitOfWork.VideoRepository.GetVideoByUsername(request.Username);

            return _mapper.Map<List<VideoVm>>(videoList);
        }
    }
}
