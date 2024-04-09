using System.ComponentModel.DataAnnotations;

namespace QA_Test_Log.Models
{
    public class View_TestCaseWithLastResult
    {
        public View_TestCaseWithLastResult()
        {
        }

        [Key]
        public int Id { get; set; }
        public string TestTitle { get; set; } = string.Empty;
        public string Scenario { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PreRequisities { get; set; } = string.Empty;
        public string TestSteps { get; set; } = string.Empty;
        public string TestData { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;
        public bool Status { get; set; }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        //public View_LastTestResult LastTestResult { get; set; }
        public int? LastTestResultId { get; set; }
        public bool? LastTestResultStatus { get; set; }
        public string? LastTestResultActualResult { get; set; }
        public DateTime? LastTestResultTestedOn { get; set; }

    }
}
