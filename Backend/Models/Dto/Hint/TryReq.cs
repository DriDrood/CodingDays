using System;

namespace CodingDays.Models.Dto.Hint;

public record TryReq
(
    Guid TeamId,
    string CypherResult
);
