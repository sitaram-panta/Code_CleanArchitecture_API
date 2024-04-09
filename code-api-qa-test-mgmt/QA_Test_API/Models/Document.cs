using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public String FileName { get; set; }
        public string FilePath { get; set; }
        public string Createdby { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int TestResultId { get; set; }
        [ForeignKey("TestResultId"), JsonIgnore]
        public virtual TestResult TestResult { get; set; }
    }
}
