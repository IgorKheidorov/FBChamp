using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;
using System.Collections.Concurrent;
using System.Text.Json;

namespace FBChamp.Infrastructure.Repositories.Membership
{
    internal class LocationRepository : JSONRepository<Location, Guid>, ILocationRepository
    {
        protected override string JSONFileName => "Location.json";

        protected override void Inflate()
        {
            var context = @"{
                'b1a1e1f4-1c1d-4a1e-9a1e-1a1e1a1e1a1e': {
                    'Id': 'b1a1e1f4-1c1d-4a1e-9a1e-1a1e1a1e1a1e',
                    'Country': 'USA',
                    'City': 'New York',
                    'Street': '5th Avenue'
                },
                'c2b2e2f5-2d2e-5b2e-0b2e-2b2e2b2e2b2e': {
                    'Id': 'c2b2e2f5-2d2e-5b2e-0b2e-2b2e2b2e2b2e',
                    'Country': 'UK',
                    'City': 'London',
                    'Street': 'Baker Street'
                },
                'd3c3f3g6-3e3f-6c3f-1c3f-3c3f3c3f3c3f': {
                    'Id': 'd3c3f3g6-3e3f-6c3f-1c3f-3c3f3c3f3c3f',
                    'Country': 'Germany',
                    'City': 'Berlin',
                    'Street': 'Unter den Linden'
                },
                'e4d4g4h7-4f4g-7d4g-2d4g-4d4g4d4g4d4g': {
                    'Id': 'e4d4g4h7-4f4g-7d4g-2d4g-4d4g4d4g4d4g',
                    'Country': 'France',
                    'City': 'Paris',
                    'Street': 'Champs-Élysées'
                },
                'f5e5h5i8-5g5h-8e5h-3e5h-5e5h5e5h5e5h': {
                    'Id': 'f5e5h5i8-5g5h-8e5h-3e5h-5e5h5e5h5e5h',
                    'Country': 'Italy',
                    'City': 'Rome',
                    'Street': 'Via del Corso'
                },
                'g6f6i6j9-6h6i-9f6i-4f6i-6f6i6f6i6f6i': {
                    'Id': 'g6f6i6j9-6h6i-9f6i-4f6i-6f6i6f6i6f6i',
                    'Country': 'Spain',
                    'City': 'Madrid',
                    'Street': 'Gran Via'
                },
                'h7g7j7k0-7i7j-0g7j-5g7j-7g7j7g7j7g7j': {
                    'Id': 'h7g7j7k0-7i7j-0g7j-5g7j-7g7j7g7j7g7j',
                    'Country': 'Japan',
                    'City': 'Tokyo',
                    'Street': 'Shibuya'
                },
                'i8h8k8l1-8j8k-1h8k-6h8k-8h8k8h8k8h8k': {
                    'Id': 'i8h8k8l1-8j8k-1h8k-6h8k-8h8k8h8k8h8k',
                    'Country': 'Australia',
                    'City': 'Sydney',
                    'Street': 'George Street'
                }
            }";
        
            context = context.Replace('\'', '\"');

            foreach (var item in JsonSerializer
                .Deserialize<ConcurrentDictionary<Guid, Location>>(context)!)
            {
                EntityList.TryAdd(item.Key, item.Value);
            }
        }
    }
}
