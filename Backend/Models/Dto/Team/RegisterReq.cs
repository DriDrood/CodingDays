using System;

namespace CodingDays.Models.Dto.Team;
public record RegisterReq
(
    string Secret,
    Guid? Id,
    string Password
);
