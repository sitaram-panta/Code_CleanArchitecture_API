using QA_Test_Log.Data;
using QA_Test_Log.Interface;
using QA_Test_Log.Models;

namespace QA_Test_Log.Services
{
    public class TestCaseRepo : _AbsGenericRepo<TestCase, int>, ITestCaseRepo
    {

        public TestCaseRepo(AppDbContext context): base(context)
        {
                
        }
    }
}
