using FBChamp.Core.Entities.Soccer;
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


        _unitOfWork.Commit(League1);
        _unitOfWork.Commit(League2);
        _unitOfWork.Commit(Team1);
        _unitOfWork.Commit(Team2);
        _unitOfWork.Commit(TeamAssignmentInfoOne);
        _unitOfWork.Commit(TeamAssignmentInfoTwo);
    }
}
