using System;

namespace CodingDays.Models.Dto.Registration;
public record RegistrationReq
(
    string? Name,
    string? Surname,
    DateTime Birth,
    string? Phone,
    string? Email,
    bool NeedNtb,
    int Level,
    string? Languages,
    string? Note,
    bool? Bonus
);
