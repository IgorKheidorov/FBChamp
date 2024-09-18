namespace FBChamp.DataGenerators.Interfaces;

internal interface IDataGenerator
{
    void GeneratePlayer(Dictionary<string, string>? options);

    void GenerateCoach(Dictionary<string, string>? options);

    void GenerateLeague(Dictionary<string, string>? options); 

    // Add additional entity generation methods here
}