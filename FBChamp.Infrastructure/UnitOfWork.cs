﻿using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using System.Collections.ObjectModel;

namespace FBChamp.Infrastructure;

public sealed partial class UnitOfWork : IUnitOfWork
{
    #region Players
    public PlayerModel GetPlayerModel(Guid id) 
    {
        var player = PlayerRepository.Find(id);
        if (player is null) 
        {
            return null;
        }
        var playerAssignment = PlayerAssignmentInfoRepository.Find(id);
        var playerPosition = PlayerPositionsRepository.Find(player.PositionId);
        var playerTeam = playerAssignment is not null ? TeamRepository.Find(playerAssignment.TeamId) : null;
        return new PlayerModel(player, playerPosition?.Name, playerTeam?.Name, playerAssignment?.PlayingNumber.ToString());
    }

    public ReadOnlyCollection<PlayerModel> GetAllPlayerModels() =>
        PlayerRepository.All().Select(player => GetPlayerModel(player.Id)).ToList().AsReadOnly();

    private List<Guid> GetAssignedPlayerIds(Guid teamId) =>
        PlayerAssignmentInfoRepository.All()
        .Where(x => x.TeamId == teamId)
        .Select(x => x.Id).ToList();

    public ReadOnlyCollection<PlayerModel> GetAssignedPlayerModels(Guid teamGuid) =>
            GetAssignedPlayerIds(teamGuid).Select(id => GetPlayerModel(id)).ToList().AsReadOnly();    
    
    public ReadOnlyCollection<PlayerModel> GetUnassignedPlayerModels() =>
        PlayerRepository.All()
            .Where(x => !PlayerAssignmentInfoRepository.All().Select(x => x.Id).Contains(x.Id))
            .Select(x => GetPlayerModel(x.Id))
            .ToList().AsReadOnly();

    public bool DeassignPlayer(Guid playerId) =>
        PlayerAssignmentInfoRepository.Remove(playerId);

    #endregion

    #region Team
    public ReadOnlyCollection<TeamModel> GetAllTeamModels() =>    
        TeamRepository.All().ToList().Select(team => GetTeamModel(team.Id)).ToList().AsReadOnly();

    private List<Guid> GetAssignedTeamsId(Guid leagueId) =>
      TeamAssignmentInfoRepository.All()
      .Where(x => x.LeagueId == leagueId)
      .Select(x => x.Id)
      .ToList();

    public ReadOnlyCollection<TeamModel> GetAssignedTeamsModels(Guid leagueId) =>
        GetAssignedTeamsId(leagueId)
       .Select(id => GetTeamModel(id))
       .ToList()
       .AsReadOnly();

    public bool DeassignTeam(Guid leagueId) =>
        TeamAssignmentInfoRepository.Remove(leagueId);

    public TeamModel GetTeamModel(Guid id) =>
        new TeamModel(TeamRepository.Find(x => x.Id == id), GetAssignedCoachModel(id),GetAssignedPlayerModels(id));

    #endregion

    #region Coach

    private List<Guid> GetAssignedCoachIds(Guid teamId) =>
        CoachAssignmentInfoRepository.All()
        .Where(x => x.TeamId == teamId)
        .Select(x => x.Id).ToList();

    public CoachModel GetAssignedCoachModel(Guid teamID)
    {
        var coachInfo = CoachAssignmentInfoRepository.Find(x => x.TeamId == teamID);
        return coachInfo is null ? null : new CoachModel(CoachRepository.Find(x => x.Id == coachInfo.Id), 
                                                         TeamRepository.Find(x => x.Id == teamID).Name,
                                                         coachInfo.Role);
    }

    public ReadOnlyCollection<CoachModel> GetAssignedCoachModels(Guid teamGuid) =>
         GetAssignedCoachIds(teamGuid).Select(id => GetCoachModel(id)).ToList().AsReadOnly();


    public ReadOnlyCollection<CoachModel> GetUnassignedCoachModels() =>
        CoachRepository.All()
          .Where(x => !CoachAssignmentInfoRepository.All().Select(x => x.Id).Contains(x.Id))
          .Select(x => GetCoachModel(x.Id))
          .ToList().AsReadOnly();

    public CoachModel GetCoachModel(Guid id)
    {
        var coach = CoachRepository.Find(x => x.Id == id);
        if (coach is null)
        {
            return null;
        }
        var coachInfo = CoachAssignmentInfoRepository.Find(x => x.Id == id);
        var teamName = coachInfo is not null ? TeamRepository.Find(x => x.Id == coachInfo.TeamId)?.Name : null;        
        
        return new CoachModel(coach, teamName, coachInfo?.Role);
    }

    public ReadOnlyCollection<CoachModel> GetAllCoachModels()=>
        CoachRepository.All()
        .Select(x => GetCoachModel(x.Id))
        .ToList().AsReadOnly();

    public bool DeassignCoach(Guid coachId) =>
      _coachAssignmentInfoRepository.Remove(coachId);

    #endregion

    #region League

    public LeagueModel GetLeagueModel(Guid id) =>
        LeagueRepository.Find(id) is var league &&
        league != null
        ? new LeagueModel(league, GetAssignedTeamsModels(id))
        : null;

    public ReadOnlyCollection<LeagueModel> GetAllLeagueModels() =>
        LeagueRepository
        .All()
        .Select(l =>  GetLeagueModel(l.Id))
        .ToList()
        .AsReadOnly();
    #endregion

    public ReadOnlyCollection<PlayerPosition> GetAllPlayerPositions() => PlayerPositionsRepository.All().ToList().AsReadOnly();
}