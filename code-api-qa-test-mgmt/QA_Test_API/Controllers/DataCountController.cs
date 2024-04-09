using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QA_Test_Log.Data;
using QA_Test_Log.Models;
using QA_Test_Log.Viewmodel;

namespace QA_Test_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("MyPolicy")]
    public class DataCountController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataCountController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            dbContext = context;
            _userManager = userManager;
        }

        [HttpGet("viewmodel")]
        public async Task<IActionResult> GetAllCounts()
        {
            var viewModel = new DataCountViewModel
            {
                NumberOfProjects = await dbContext.projects.CountAsync(),
                NumberOfModules = await dbContext.Modules.CountAsync(),
                NumberOfTestCases = await dbContext.TestCases.CountAsync(),
                NumberOfPendingTestCases = await dbContext.TestCases
                .Where(tc => !dbContext.View_LastTestResult.Any(ltr => ltr.TestCaseId == tc.Id))
    .CountAsync(),
                NumberOfFailedTestCases = await dbContext.TestCases
                .Where(tc => dbContext.View_TestCaseWithLastResult
                 .Any(ltr => ltr.LastTestResultId == tc.Id && ltr.LastTestResultStatus == false))
            .CountAsync(),
                NumberOfUsers = await _userManager.Users.CountAsync(),
                NumberOfUsersByRole = await _userManager.Users
                                            .GroupBy(u => u.UserType) // Group users by UserType
                                            .Select(g => new UserRoleCount
                                            {
                                                Role = g.Key, // The UserType value (0, 1, or 2)
                                                Count = g.Count() // Count of users with the same UserType
                                            })
                                            .ToListAsync()
            };


            return Ok(viewModel);
        }
    }
}


