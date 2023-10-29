using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string HashedPassowrd { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }

    }
}
