using System.Data;

namespace Movie.PoC.Api.Entities
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Role Role { get; set; }

        public UserModel()
        {
            CreateDate = DateTime.UtcNow;
        }
    }

    public enum Role
    {
        Admin = 1,
        User
    }
}
