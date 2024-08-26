
namespace FBChamp.Infrastructure.Helpers;

public static class ThreadSafeFileWriter
{
    public static string ReadFile(string filePathAndName)
    {
        // This block will be protected area
        using (var mutex = new Mutex(false, filePathAndName.Replace("\\", "")))
        {
            var hasHandle = false;
            try
            {
                hasHandle = mutex.WaitOne(Timeout.Infinite, false);
                
                if (!File.Exists(filePathAndName))
                {
                    return string.Empty;
                }

                return File.ReadAllText(filePathAndName);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Very important! Release the mutex
                // Or the code will be locked forever
                if (hasHandle)
                {
                    mutex.ReleaseMutex();
                }
            }
        }
    }

    public static void WriteFile(string fileContents, string filePathAndName)
    {
        using (var mutex = new Mutex(false, filePathAndName.Replace("\\", "")))
        {
            var hasHandle = false;
            try
            {
                hasHandle = mutex.WaitOne(Timeout.Infinite, false);
                File.WriteAllText(filePathAndName, fileContents);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (hasHandle)
                {
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}