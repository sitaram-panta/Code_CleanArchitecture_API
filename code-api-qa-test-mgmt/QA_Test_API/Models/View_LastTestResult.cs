using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace QA_Test_Log.Models
{
    public class View_LastTestResult
    {
        [Key]
        public int Id { get; set; }
        
        public int TestCaseId { get; set; }
        public bool Status { get; set; }
        
        public int UserId { get; set; }
        public string TestedByUser { get; set; }
        public string ActualResult { get; set; } = string.Empty;
        public DateTime TestedOn { get; set; } = DateTime.Now;

    }
}
