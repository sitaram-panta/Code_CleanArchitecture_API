using System.ComponentModel.DataAnnotations;

namespace QA_Test_Log.Viewmodel
{


    public class TestCaseViewModel
    {
        public int Id { get; set; }
        public string TestTitle { get; set; }
        public string Scenario { get; set; }
        public string Description { get; set; }
        public string PreRequisities { get; set; }
        public string TestSteps { get; set; }
        public string TestData { get; set; }
        public string ExpectedResult { get; set; }
        public bool Status { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; } // Include the module name for display
        public string ParentModuleName { get; set; } // Include the parent module name
        public string LastTestResultActualResult { get; set; }
        public int LastTestResultId { get; set; }
        public bool LastTestResultStatus { get; set; }
        public DateTime LastTestResultTestedOn { get; set; }
        public int ProjectId { get; set; }
        public String ProjectName { get; set; }
        public bool HasSubModules { get; set; }




    }

}

