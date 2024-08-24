using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;
using System.Collections.Concurrent;
using System.Text.Json;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class PlayerPositionsRepository: JSONRepository<PlayerPosition, Guid>, IPlayerPositionsRepository
{
    protected override string JSONFileName => "PlayerPositions.json";

    protected override void Inflate()
    {
        var context = @"{
            '006965f4-f055-4bdd-aa29-de8e2c962496': {
                'Name': 'Goalkeeper',
                'VisualOrder': 0,
                'Description': 'Goalkeeper',
                'Id': '006965f4-f055-4bdd-aa29-de8e2c962496'
                },
            '7626694a-3993-4004-a192-6aa8fd49ee97': {
                'Name': 'Defender',
                'VisualOrder': 1,
                'Description': 'Defender',
                'Id': '7626694a-3993-4004-a192-6aa8fd49ee97'
                },
            'fdcf66a0-89ab-4460-85a9-7f6a64ab8b66': {
                'Name': 'Midfielder',
                'VisualOrder': 2,
                'Description': 'Midfielder',
                'Id': 'fdcf66a0-89ab-4460-85a9-7f6a64ab8b66'
                },
            'adcf66a0-89ab-4460-85a9-7f6a64ab8b64': {
                'Name': 'Forward',
                'VisualOrder': 3,
                'Description': 'Forward',
                'Id': 'adcf66a0-89ab-4460-85a9-7f6a64ab8b64'
                }
        }";
        context = context.Replace('\'','\"');
       foreach (var item in JsonSerializer.Deserialize<ConcurrentDictionary<Guid,PlayerPosition>>(context)!)
        {
            EntityList.TryAdd(item.Key, item.Value);
        }
    }
}
