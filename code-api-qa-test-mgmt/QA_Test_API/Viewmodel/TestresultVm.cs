namespace QA_Test_Log.Viewmodel
{
    public class FileUploadResult
    {
        public bool Status { get; set; }
        public string FileName { get; set; }
        public string AbsFullPath { get; set; }
        public string RelativePath { get; set; }
    }

    public class TestresultVm
    {
        public IFormFile? File { get; set; }

        public int TestCaseId { get; set; }

        public string? ActualResult { get; set; }

        public bool Status { get; set; }

    }
    //public class ReqTestResult
    //{
    //    public int? Id { get; set; }
    //    public int TestCaseId { get; set; }
    //    public string? ActualResult { get; set; }
    //    public bool TestResultStatus { get; set; }
    //    public IFormFile? FileAttachment { get; set; }
    //}
}
