using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class RegistrationDto : LoginDto
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Name should only contain letters and spaces")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
