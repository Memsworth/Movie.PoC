using Movie.PoC.Api.Models.Enums;

namespace Movie.PoC.Api.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public UserType Role { get; set; }

        public User()
        {
            CreateDate = DateTime.UtcNow;
        }
    }
}
