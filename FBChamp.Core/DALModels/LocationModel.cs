using FBChamp.Core.Entities.Soccer;
using System.ComponentModel;

namespace FBChamp.Core.DALModels
{
    internal class LocationModel : EntityModel
    {
        public Location Location { get; }
        
        public string Description { get => Location.ToString(); }
    }
}
