using CleanArchitecture.Domain.Commom;

namespace CleanArchitecture.Domain
{
    public class Streamer : Entity
    {
        public Streamer()
        {
            Videos = new HashSet<Video>();
        }

        public string? Name { get; set; }

        public string? Url { get; set; }

        public ICollection<Video>? Videos { get; set; }

    }
}
