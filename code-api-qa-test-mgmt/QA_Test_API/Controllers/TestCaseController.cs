 using QA_Test_Log.Interface;
using QA_Test_Log.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QA_Test_Log.Data;
using QA_Test_Log.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace QA_Test_Log.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]

    public class TestCaseController : GenericCRUDController<ITestCaseRepo, TestCase, int>
    {
        private readonly AppDbContext dbContext;

        public TestCaseController(ITestCaseRepo testCase, AppDbContext _dbContext) : base(testCase)
        {
            this.dbContext = _dbContext;
        }

        [HttpGet("GetListWithLastResult")]
        public async Task<ActionResult<IList<View_TestCaseWithLastResult>>> GetListWithLastResult(int? moduleId)
        {
            IQueryable<View_TestCaseWithLastResult> _Que = dbContext.View_TestCaseWithLastResult.AsNoTracking();

            if (moduleId.HasValue)
            {
                _Que = _Que.Where(x => x.ModuleId == moduleId);
            }

            IList<View_TestCaseWithLastResult> DataList = await _Que
                .Select(x => new View_TestCaseWithLastResult()
                {
                    Description = x.Description,
                    ExpectedResult = x.ExpectedResult,
                    Id = x.Id,
                    //LastTestResult = x.child.FirstOrDefault(),
                    ModuleId = x.ModuleId,
                    ModuleName = x.ModuleName,
                    ProjectId = x.ProjectId,
                    ProjectName = x.ProjectName,
                    PreRequisities = x.PreRequisities,
                    Scenario = x.Scenario,
                    Status = x.Status,
                    TestData = x.TestData,
                    TestSteps = x.TestSteps,
                    TestTitle = x.TestTitle,
                    LastTestResultId = x.LastTestResultId,
                    LastTestResultActualResult = x.LastTestResultActualResult,
                    LastTestResultStatus = x.LastTestResultStatus,
                    LastTestResultTestedOn = x.LastTestResultTestedOn
                })
                .ToListAsync();

            return Ok(DataList);
        }

    }
}
