namespace CodingDays.Models.Dto.Hint;
public record TryResp
(
    bool AlreadyUsed,
    string? Text,
    string? ImageUrl
);
