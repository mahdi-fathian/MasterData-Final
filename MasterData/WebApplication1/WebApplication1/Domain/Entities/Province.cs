namespace WebApplication1.Domain.Entities
{
    public class Province
    {
        public int Id { get;  set; }

        public required string Name { get;  set; }

        // Navigation property
        //public ICollection<City> Cities { get; private set; }
    }
}







