using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public String Nationality { get; set; }
        public string MyProperty { get; set; }
        public string PasswordHas { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
