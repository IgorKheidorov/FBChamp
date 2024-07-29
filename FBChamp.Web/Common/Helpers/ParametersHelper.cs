using System.Linq;

namespace FBChamp.Web.Common.Helpers;

public static class ParametersHelper
{
    public static string GetValueFor(this string source, string parameter)
    {
        var res = source?.Split(';')?.Where(x => x.Contains(parameter)).FirstOrDefault()?.Split(':').ToArray();
        return res?.Length == 2 ? res[1] : string.Empty;
    }

    public static int? GetIntValueFor(this string source, string parameter)
    {
        int value = 0;
        return int.TryParse(source.GetValueFor(parameter), out value) ? value : null;
    }

    public static Guid GetGuidValueFor(this string source, string parameter)
    {
        Guid guid;
        return Guid.TryParse(source.GetValueFor(parameter), out guid) ? guid : default;
    }

}
