namespace FBChamp.Common.Paging;

public static class Queryable
{
    public static IQueryable<T> SelectPage<T>(this IQueryable<T> query, PageInfo pageInfo)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(pageInfo);

        return query.Skip((pageInfo.Page - 1) * pageInfo.PerPage).Take(pageInfo.PerPage);
    }
}