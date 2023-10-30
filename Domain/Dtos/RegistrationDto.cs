using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class RegistrationDto : LoginDto
    {
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
