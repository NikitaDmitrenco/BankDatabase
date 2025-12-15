using BankDatabase.Abstractions.Cron;
using BankDatabase.Entities;
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
    public string CronWorkOrder { get; set; } = CronHandler.EmptyCronString;
}
