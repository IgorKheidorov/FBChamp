using FBChamp.Core.Entities;

namespace FBChamp.DataGenerators.Interfaces;

internal interface IEntityGenerator
{
    IEnumerable<Entity<Guid>> Generate(Dictionary<string, string>? options);
}