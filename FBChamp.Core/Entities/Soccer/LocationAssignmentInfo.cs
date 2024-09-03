using System.Security.Authentication;

namespace FBChamp.Core.Entities.Soccer
{
    public class LocationAssignmentInfo : Entity<Guid>
    {
        public string City { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        public LocationAssignmentInfo()
        {
            
        }

        public LocationAssignmentInfo(Guid cityId, string city, Guid countryId, Country country)
        {
            Id = cityId;
            City = city;
            CountryId = countryId;
            Country = country;
        }
    }
}
