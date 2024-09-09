using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Entities.Soccer.Enums;
using FBChamp.Core.UnitOfWork;
using IntegrationTests.Helpers;

namespace IntegrationTests;

public static class TestDataFactory
{
    public static League? League1 { get; private set; }
    public static League? League2 { get; private set; }
    public static Team? Team1 { get; private set; }
    public static Team? Team2 { get; private set; }
    public static TeamAssignmentInfo? TeamAssignmentInfoOne { get; private set; }
    public static TeamAssignmentInfo? TeamAssignmentInfoTwo { get; private set; }

    public static Match? Match1 {  get; private set; }
    public static Match? Match2 { get; private set; }
    public static Stadium? Stadium {  get; private set; }
    public static MatchStatistics? MatchStatistics1 {  get; private set; }


    public static void SeedEntity(IUnitOfWork _unitOfWork)
    {
        var photoGenerator = new PhotoGenerator();
        var photo = photoGenerator.Generate(300, 500);

        League1 = new League(Guid.NewGuid(), "League1", photo, 1, DateTime.Now, DateTime.Now.AddMonths(6), "Description1");
        League2 = new League(Guid.NewGuid(), "League2", photo, 8, DateTime.Now, DateTime.Now.AddMonths(6), "Description2");

        Team1 = new Team(Guid.NewGuid() ,"TeamOne", Guid.NewGuid(), photo, Guid.NewGuid() ,"DescriptionForTeamOne");
        Team2 = new Team(Guid.NewGuid(), "TeamTwo", Guid.NewGuid(), photo, Guid.NewGuid(), "DescriptionForTeamTwo");

        TeamAssignmentInfoOne = new TeamAssignmentInfo(Team1.Id, Guid.NewGuid());
        TeamAssignmentInfoTwo = new TeamAssignmentInfo(Team2.Id, Guid.NewGuid());

        Stadium = new Stadium(Guid.NewGuid(), "Stadium Name", Guid.NewGuid());

        Match1 = new Match(Guid.NewGuid(), Stadium.Id, League1.Id,
                          MatchStatus.InProgress, DateTime.UtcNow, Team1.Id, Team2.Id);

        Match2 = new Match(Guid.NewGuid(), Stadium.Id, League1.Id,
                           MatchStatus.InProgress, DateTime.Now.AddHours(2), Team2.Id, Team1.Id);

        MatchStatistics1 = new MatchStatistics(Guid.NewGuid(), Match1.Id, 10);

        _unitOfWork.Commit(League1);
        _unitOfWork.Commit(League2);
        _unitOfWork.Commit(Team1);
        _unitOfWork.Commit(Team2);
        _unitOfWork.Commit(TeamAssignmentInfoOne);
        _unitOfWork.Commit(TeamAssignmentInfoTwo);
        _unitOfWork.Commit(Stadium);
        _unitOfWork.Commit(Match1);
        _unitOfWork.Commit(Match2);
        _unitOfWork.Commit(MatchStatistics1);
    }
}
