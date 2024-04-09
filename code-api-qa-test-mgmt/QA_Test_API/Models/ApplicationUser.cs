using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QA_Test_Log.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }
        public EnumApplicationUserType UserType { get; set; }

    }
}
