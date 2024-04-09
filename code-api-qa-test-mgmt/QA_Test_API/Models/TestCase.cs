using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{
    public class TestCase
    {
       

        [Key]
        public int Id { get; set; }
        [Required]
        public string TestTitle { get; set; } = string.Empty;
        public string Scenario { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PreRequisities { get; set; } = string.Empty;
        public string TestSteps { get; set; } = string.Empty;
        public string TestData { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;
        public bool Status { get; set; }
        
        public int ModuleId { get; set; }

        [ForeignKey("ModuleId")]
        [JsonIgnore]
        public virtual Module Module { get; set; }
        [JsonIgnore]
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
