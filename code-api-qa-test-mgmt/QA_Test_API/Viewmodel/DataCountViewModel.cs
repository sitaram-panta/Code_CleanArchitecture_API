using QA_Test_Log.Models;

namespace QA_Test_Log.Viewmodel
{
    public class DataCountViewModel
    {
        public int NumberOfProjects { get; set; }
        public int NumberOfModules { get; set; }
        public int NumberOfTestCases { get; set; }
        public int NumberOfPendingTestCases { get; set; }
        public int NumberOfFailedTestCases { get; set; }
        public int NumberOfUsers { get; set; }
        public List<UserRoleCount> NumberOfUsersByRole { get; set; }
    }


    public class UserRoleCount
    {
        public EnumApplicationUserType Role { get; set; }
        public int Count { get; set; }
    }
}
