using CleanArchitecture.Domain.Commom;

namespace CleanArchitecture.Domain
{
    public class VideoActor : Entity
    {
        public int VideoId { get; set; }

        public int ActorId { get; set; }
    }
}
