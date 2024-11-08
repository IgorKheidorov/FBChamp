﻿using FBChamp.Core.Entities.Soccer;
using FBChamp.Core.Repositories.Membership;

namespace FBChamp.Infrastructure.Repositories.Membership;

internal class CoachRepository: JSONRepository<Coach, Guid>, ICoachRepository
{
    protected override string JSONFileName => "Trainers.json";
}

