using System;
using System.ComponentModel.DataAnnotations;
using CodingDays.Models.Dto.Registration;

namespace CodingDays.Database.Entities;
public class Registration
{
    public Registration()
    {
    }
    public Registration(RegistrationReq reg)
    {
        Id = Guid.NewGuid();
        Name = reg.Name;
        Surname = reg.Surname;
        Birth = reg.Birth;
        Phone = reg.Phone;
        Email = reg.Email;
        NeedNtb = reg.NeedNtb;
        Level = reg.Level;
        Languages = reg.Languages;
        Note = reg.Note;
        Bonus = reg.Bonus;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    [Required]
    [StringLength(50)]
    public string? Surname { get; set; }
    public DateTime Birth { get; set; }
    [Required]
    [StringLength(20)]
    public string? Phone { get; set; }
    [Required]
    [StringLength(100)]
    public string? Email { get; set; }
    public bool NeedNtb { get; set; }
    public int Level { get; set; }
    [StringLength(200)]
    public string? Languages { get; set; }
    [StringLength(1000)]
    public string? Note { get; set; }
    public bool? Bonus { get; set; }
    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
    public DateTime CreatedAt { get; set; }
}
