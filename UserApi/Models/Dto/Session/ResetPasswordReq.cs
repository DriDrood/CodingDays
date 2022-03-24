namespace CodingDays.UserApi.Models.Dto.Session;
public record ResetPasswordReq
(
    string Name,
    string Token,
    string NewPassword
);
