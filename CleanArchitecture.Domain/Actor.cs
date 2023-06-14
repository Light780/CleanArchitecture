using CleanArchitecture.Domain.Commom;

namespace CleanArchitecture.Domain
{
    public class Actor : Entity
    {
        public Actor()
        {
            Videos = new HashSet<Video>();
        }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}
