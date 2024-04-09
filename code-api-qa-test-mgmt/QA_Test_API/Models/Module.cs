using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ModuleName { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int? Parent_Module_Id { get; set; }
      
        [Required]
        public int ProjectId { get; set; }

        [ForeignKey ("ProjectId"),JsonIgnore]
        public virtual Project Project { get; set; }

        [JsonIgnore]
        public virtual List<TestCase> TestCases { get; set; }


        [JsonIgnore]
        public virtual List<DocumentUpload> DocumentUpload { get; set; }
        [JsonIgnore]
        public virtual List<Tasks> Tasks { get; set; }


    }
}
 