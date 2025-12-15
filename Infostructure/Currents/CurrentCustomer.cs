using System.ComponentModel.DataAnnotations;

namespace BankDatabase.Infostructure.Currents;

public class CurrentCustomer : ICurrent
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone Number is required")]
    [Phone(ErrorMessage = "Phone number is invalid")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "The email address is invalid")]
    public string EmailAddress { get; set; } = string.Empty;
}
