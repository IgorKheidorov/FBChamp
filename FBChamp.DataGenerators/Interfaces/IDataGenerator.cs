namespace FBChamp.DataGenerators.Interfaces;

internal interface IDataGenerator
{
    void GeneratePlayer(Dictionary<string, string>? options);

    void GeneratePlayerPosition(Dictionary<string, string>? options);

    // Add additional entity generation methods here
}