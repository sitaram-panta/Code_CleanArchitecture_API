using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{
    public class TestResult
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TestCaseId { get; set; }
       
        public bool Status { get; set; }
        [MaxLength(255)]
        
        public string UserId { get; set; }
        [MaxLength(255)]
        public string TestedByUser { get; set; }

        public string ActualResult { get; set; } = string.Empty;
        public DateTime TestedOn { get; set; } = DateTime.Now;

        [ForeignKey("TestCaseId"), JsonIgnore]
        public virtual TestCase TestCase { get; set; }
        public virtual Document Document { get; set; }

        

       

    }
}
