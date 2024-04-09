using Microsoft.EntityFrameworkCore;
using QA_Test_Log.Data;
using QA_Test_Log.Interface;
using QA_Test_Log.Models;

namespace QA_Test_Log.Services
{
    public class TestResultRepo : _AbsGenericRepo<TestResult, int>, ITestResultRepo
    {

        public TestResultRepo(AppDbContext context) : base(context)
        {

        }
        public override IQueryable<TestResult> GetList()
        {
            return base.GetList().Include(x => x.Document);
        }

        
    }
}
