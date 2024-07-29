using FBChamp.Core.DALModels;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Socker;
using System.Collections.ObjectModel;

namespace FBChamp.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    #region Player
    PlayerModel GetPlayerModel(Guid id);
    ReadOnlyCollection<PlayerModel> GetAssignedPlayerModels(Guid teadGuid);
    ReadOnlyCollection<PlayerModel> GetUnassignedPlayerModels();
    ReadOnlyCollection<PlayerModel> GetAllPlayerModels();
    #endregion

    #region Team
    TeamModel GetTeamModel(Guid id);
    ReadOnlyCollection<TeamModel> GetAllTeamModels();
    #endregion

    #region Coach
    CoachModel GetCoachModel(Guid id);
    ReadOnlyCollection<CoachModel> GetAllCoachModels();
    ReadOnlyCollection<CoachModel> GetAssignedCoachModels(Guid teadGuid);
    ReadOnlyCollection<CoachModel> GetUnassignedCoachModels();
    #endregion

    #region Other
    ReadOnlyCollection<PlayerPosition> GetAllPlayerPositions();
    #endregion

    bool Remove(Guid id, Type type);
    
    bool Commit(IReadOnlyCollection<Entity> entities);
    bool Commit(Entity entities);
    
}