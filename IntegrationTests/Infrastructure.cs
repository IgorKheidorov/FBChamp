namespace IntegrationTests
{
    internal static class Infrastructure
    {
        internal const string PathToData = ".//Data";

        internal static void CleanUp()
        {
            if (Directory.Exists(PathToData))
            {
                var fileNames = Directory.GetFiles(PathToData);
                foreach (var fileName in fileNames)
                {
                    File.Delete(fileName);
                }
            }
            else
            {
                Directory.CreateDirectory(PathToData);
            }
        }
    }
}
