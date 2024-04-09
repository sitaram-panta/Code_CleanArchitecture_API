using System.ComponentModel.DataAnnotations;

namespace QA_Test_Log.Models
{
    public class OTPVerificationLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string GeneratedOTPCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
