using System;
using System.Collections.Generic;

namespace CodingDays.Models.Dto.Team;
public record GetResp
(
    Dictionary<Guid, GetRespTeam> Teams
);
