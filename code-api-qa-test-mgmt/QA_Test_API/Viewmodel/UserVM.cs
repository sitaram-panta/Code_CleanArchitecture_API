using QA_Test_Log.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Viewmodel
{
    public class UserVM
    {

        [Key]
        public string Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Username { get; set; }
        [MaxLength(255)]
        [Required]

        public string Email { get; set; }
        [MaxLength(255)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(255)]
        [Required]
        public string LastName { get; set; }
        [Required]
       
        public EnumApplicationUserType Role { get; set; }

        [NotMapped]
        [MaxLength(255)]
        public string Password { get; set; }

    }
}
