using CleanArchitecture.Domain.Commom;

namespace CleanArchitecture.Domain
{
    public class Video : Entity
    {
        public Video() 
        { 
            Actors = new HashSet<Actor>();
        }

        public string? Name { get; set; }

        public int StreamerId { get; set; }

        public virtual Streamer? Streamer { get; set; }

        public int DirectorId { get; set; }

        public virtual Director? Director { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
    }
}
