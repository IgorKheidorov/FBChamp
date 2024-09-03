using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.DALModels
{
    public class LocationModel : EntityModel
    {
        public Location Location { get; init; }

        public LocationModel(Guid id)
        {
            Location = new Location { Id = id };
        }
        
        public LocationModel(Guid id, string city, Guid countryId, Country country)
        {
            Location = new Location(id, city, countryId, country);
        }
    }
}
