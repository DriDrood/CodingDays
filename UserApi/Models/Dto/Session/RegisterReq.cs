namespace CodingDays.UserApi.Models.Dto.Session;
public record RegisterReq
(
    string Secret,
    string Name,
    string Password
);
