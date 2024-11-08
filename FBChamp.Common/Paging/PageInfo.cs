﻿namespace FBChamp.Common.Paging;

public class PageInfo
{
    public int Page { get; }

    public int PerPage { get; }

    public PageInfo(int page = 1, int perPage = int.MaxValue)
    {
        Check.ArgumentSatisfies(page, x => x > 0, nameof(page) + " must be > 0.");
        Check.ArgumentSatisfies(perPage, x => x > 0, nameof(perPage) + " must be > 0.");

        Page = page;
        PerPage = perPage;
    }
}