using BankDatabase.Infostructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace BankDatabase.Infostructure.Currents;

public class CurrentEmployee : ICurrent
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(18, 65, ErrorMessage = "Age must be specified and be between 18 and 60 years old")]
    public int Age { get; set; } = 18;

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone Number is required")]
    [Phone(ErrorMessage = "Phone number is invalid")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Position is required")]
    public string Position { get; set; } = string.Empty;
}
