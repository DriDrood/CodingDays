using System;

namespace CodingDays.Models.Dto.Registration;

public class RegistrationReq
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime Birth { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool NeedNtb { get; set; }
    public int Level { get; set; }
    public string? Languages { get; set; }
    public string? Note { get; set; }
    public bool? Bonus { get; set; }
}
