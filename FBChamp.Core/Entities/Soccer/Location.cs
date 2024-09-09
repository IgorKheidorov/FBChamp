namespace FBChamp.Core.Entities.Soccer
{
    public class Location : Entity<Guid>
    {
        public string Country { get; set; }

        public string City { get; set; }
        
        public string Street { get; set; }
    
        public override string ToString() => $"{Country}, {City}, {Street}";
    }
}
