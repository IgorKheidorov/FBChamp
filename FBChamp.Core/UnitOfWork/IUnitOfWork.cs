using System.Collections.ObjectModel;
using FBChamp.Core.DALModels;
using FBChamp.Core.Entities;
using FBChamp.Core.Entities.Soccer;

namespace FBChamp.Core.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    #region Other

    ReadOnlyCollection<PlayerPosition> GetAllPlayerPositions();

    #endregion

    CRUDResult Remove(Guid id, Type type);

    CRUDResult Commit(Entity entities);

    bool Exists(Guid id, Type type);

    #region Player

    PlayerModel GetPlayerModel(Guid id);

    ReadOnlyCollection<PlayerModel> GetAssignedPlayerModels(Guid teadGuid);

    ReadOnlyCollection<PlayerModel> GetUnassignedPlayerModels();

    ReadOnlyCollection<PlayerModel> GetAllPlayerModels();

    #endregion

    #region Team

    TeamModel GetTeamModel(Guid id);

    ReadOnlyCollection<TeamModel> GetAssignedTeamsModels(Guid leagueId);

    bool DeassignTeam(Guid leagueId);

    ReadOnlyCollection<TeamModel> GetAllTeamModels();

    #endregion

    #region Coach

    CoachModel GetCoachModel(Guid id);

    ReadOnlyCollection<CoachModel> GetAllCoachModels();

    ReadOnlyCollection<CoachModel> GetAssignedCoachModels(Guid teadGuid);

    ReadOnlyCollection<CoachModel> GetUnassignedCoachModels();

    #endregion

    #region League

    LeagueModel GetLeagueModel(Guid id);

    #region Location
    
    ReadOnlyCollection<LocationModel> GetAllLocationModels();
    
    LocationModel GetLocationModel(Guid id);


    #endregion


    CRUDResult Remove(Guid id, Type type);    

    #endregion  
}