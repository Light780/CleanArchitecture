using CleanArchitecture.Domain.Commom;

namespace CleanArchitecture.Domain
{
    public class Director : Entity
    {
        public Director() 
        {
            Videos = new HashSet<Video>();
        }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public ICollection<Video> Videos { get; set; }
    }
}
