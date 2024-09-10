using FBChamp.Core.DALModels;
using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities.Soccer.Enums;
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

    private List<Guid> GetAssignedPlayerIdsForMatch(Guid matchId) => 
        PlayerMatchAssignmentRepository.All()
        .Where(p => p.MatchId == matchId)
        .Select(x => x.Id)
        .ToList();

    public PlayerMatchAssignmentModel GetPlayerMatchAssignmentModel(Guid playerId)
    {
        var playerAssignment = PlayerMatchAssignmentRepository.Find(playerId);

        if(playerAssignment is null)
        {
            return null;
        }

        return new PlayerMatchAssignmentModel(playerAssignment);
    }

    public ReadOnlyCollection<PlayerMatchAssignmentModel> GetAssignedPlayerModelsForMatch(Guid matchId) =>
        GetAssignedPlayerIdsForMatch(matchId)
        .Select(id => GetPlayerMatchAssignmentModel(id))
        .ToList()
        .AsReadOnly();

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

    // To Do:
    // We have to delete
    // * not finished matches with participating of this team
    // * deassign all players and all coaches
    public bool DeassignTeam(Guid teamId) => 
         TeamAssignmentInfoRepository.Remove(teamId);

    public TeamModel GetTeamModel(Guid id) =>
        new TeamModel(TeamRepository.Find(x => x.Id == id), GetAssignedCoachModel(id), GetAssignedPlayerModels(id));

    public ReadOnlyCollection<TeamModel> GetUnassignedTeamModel() =>
        TeamRepository.All()
        .Where(t => !TeamAssignmentInfoRepository.All().Select(ta => ta.Id).Contains(t.Id))
        .Select(x => GetTeamModel(x.Id))
        .ToList()
        .AsReadOnly();

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

    public ReadOnlyCollection<CoachModel> GetAllCoachModels() =>
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
        .Select(l => GetLeagueModel(l.Id))
        .ToList()
        .AsReadOnly();
    #endregion

    #region Stadium
    public StadiumModel GetStadiumModel(Guid id) =>
        new StadiumModel(StadiumRepository.Find(x => x.Id == id), null);

    public ReadOnlyCollection<StadiumModel> GetAllStadiumModels() =>
        StadiumRepository.All().ToList().Select(stadium => GetStadiumModel(stadium.Id)).ToList().AsReadOnly();
    #endregion

    #region Match

    public MatchStatisticsModel GetMatchStatisticsModel(Guid matchId)
    {
        var matchStatistics = MatchStatisticsRepository.Find(m => m.MatchId == matchId);

        if(matchStatistics is null)
        {
            return null;
        }

        return new MatchStatisticsModel(matchStatistics);
    }

    public MatchModel GetMatchModel(Guid matchId)
    {
        var match = MatchRepository.Find(matchId);

        if(match is null)
        {
            return null;
        }

        var hostTeam = GetTeamModel(match.HostTeamId);
        var guestTeam = GetTeamModel(match.GuestTeamId);

        var stadiumModel = GetStadiumModel(match.StadiumId);

        var playerAssignments = GetAssignedPlayerModelsForMatch(matchId);

        var matchStatistics = GetMatchStatisticsModel(matchId);

        return new MatchModel(match, hostTeam, guestTeam, stadiumModel, playerAssignments, matchStatistics);
    }

    public ReadOnlyCollection<MatchModel> GetAllMatchModels() =>
        MatchRepository.All()
        .Select(x => GetMatchModel(x.Id))
        .ToList()
        .AsReadOnly();

    private List<Guid> GetUnfinishedMatchesIds(Guid leagueId) =>
         MatchRepository.All()
        .Where(m => m.LeagueId == leagueId && m.Status != MatchStatus.Finished)
        .Select(x => x.Id)
        .ToList();

    #endregion

    public ReadOnlyCollection<PlayerPosition> GetAllPlayerPositions() => PlayerPositionsRepository.All().ToList().AsReadOnly();
}