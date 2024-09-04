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
                ""3b45cb09-8cc4-4f5f-bbb1-4a0622ff0a1a"": {
                    ""Id"": ""3b45cb09-8cc4-4f5f-bbb1-4a0622ff0a1a"",
                    ""Country"": ""USA"",
                    ""City"": ""New York"",
                    ""Street"": ""5th Avenue""
                },
                ""97595f4c-0f14-4f27-994d-b48ad661a93a"": {
                    ""Id"": ""97595f4c-0f14-4f27-994d-b48ad661a93a"",
                    ""Country"": ""UK"",
                    ""City"": ""London"",
                    ""Street"": ""Baker Street""
                },
                ""aba3552b-f6a0-434c-8805-4388b90277e3"": {
                    ""Id"": ""aba3552b-f6a0-434c-8805-4388b90277e3"",
                    ""Country"": ""Germany"",
                    ""City"": ""Berlin"",
                    ""Street"": ""Unter den Linden""
                },
                ""c189c91e-0978-4d28-a7c3-c27a4c529955"": {
                    ""Id"": ""c189c91e-0978-4d28-a7c3-c27a4c529955"",
                    ""Country"": ""France"",
                    ""City"": ""Paris"",
                    ""Street"": ""Champs-Elysees""
                }
            }";
        
            foreach (var item in JsonSerializer
                                .Deserialize<ConcurrentDictionary<Guid, Location>>(context)!)
            {
                EntityList.TryAdd(item.Key, item.Value);
            }
        }
    }
}
