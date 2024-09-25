﻿using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.UnitOfWork;
using FBChamp.DataGenerators;
using FBChamp.Infrastructure;
using IntegrationTests.Helpers;

namespace IntegrationTests.TeamTests;

[TestClass]
public class TeamRepositoryTests
{
    private IUnitOfWork? _unitOfWork;
    private DataGenerator? _dataGenerator;

    [TestInitialize]
    public void Initialize()
    {
        Infrastructure.CleanUp();
        _unitOfWork = new UnitOfWork();
        _dataGenerator = new DataGenerator(_unitOfWork);

        _dataGenerator.GenerateTeam(new Dictionary<string, string>
        {
            { "Count", "1" }
        });
    }

    [TestMethod]
    public void AddTeams()
    {
        var initialCount = _unitOfWork!.GetAllTeamModels().Count;
        _dataGenerator!.GenerateTeam(new Dictionary<string, string> { { "Count", "1" } });
        var team = _unitOfWork.GetAllTeamModels().First().Team;

        List<Task> tasks = Enumerable.Range(0, 100)
            .Select(x => new Task(() =>
            {
                _unitOfWork.Commit(team);
            })).ToList();

        tasks.ForEach(x => x.Start());
        Task.WaitAll(tasks.ToArray());

        var realCount = _unitOfWork.GetAllTeamModels().Count;

        Assert.AreEqual(initialCount + 1, realCount);
    }

    [TestMethod]
    public void AddUniqueTeams()
    {
        var initialCount = _unitOfWork!.GetAllTeamModels().Count;
        _dataGenerator!.GenerateTeam(new Dictionary<string, string> { { "Count", "100" } });

        var realCount = _unitOfWork.GetAllTeamModels().Count;
        Assert.AreEqual(initialCount + 100, realCount);
    }

    [TestMethod]
    public void GetTeamById()
    {
        var teamModel = _unitOfWork!.GetAllTeamModels().First();
        var teamId = teamModel.Team.Id;
        var retrievedTeam = _unitOfWork.GetTeamModel(teamId);

        Assert.IsNotNull(retrievedTeam);
        Assert.AreEqual(teamId, retrievedTeam.Team.Id);
        Assert.AreEqual(teamModel.Team, retrievedTeam.Team);
    }

    [TestMethod]
    public void UpdateTeam()
    {
        var teamModel = _unitOfWork!.GetAllTeamModels().First();
        var teamId = teamModel.Team.Id;

        teamModel.Team.Name = "NewName";
        _unitOfWork.Commit(teamModel.Team);
        var updatedTeam = _unitOfWork.GetTeamModel(teamId);

        Assert.IsNotNull(updatedTeam);
        Assert.AreEqual("NewName", updatedTeam.Team.Name);
    }

    [TestMethod]
    public void DeleteTeam()
    {
        var initialCount = _unitOfWork!.GetAllTeamModels().Count;
        var teamId = _unitOfWork.GetAllTeamModels().First().Team.Id;

        _unitOfWork.Remove(teamId, typeof(Team));
        var realCount = _unitOfWork.GetAllTeamModels().Count;

        Assert.AreEqual(initialCount - 1, realCount);
    }
}