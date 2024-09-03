namespace FBChamp.Core.Entities.Soccer
{
    public class Location : Entity<Guid>
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        public Location()
        {
            
        }

        public Location(Guid id, string city, Guid countryId, Country country)
        {
            Id = id;
            City = city is not null ? city : "Not assigned value";
            CountryId = countryId;
            Country = country is not null ? country : new Country();
        }

    }
}
