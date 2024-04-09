  

using QA_Test_Log.Interface;
using QA_Test_Log.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QA_Test_Log.Data;
using QA_Test_Log.Viewmodel;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.Identity.Client;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace QA_Test_Log.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]

    public class TestResultController : GenericCRUDController<ITestResultRepo, TestResult, int>
    {
        private readonly AppDbContext dbContext;
        private readonly IWebHostEnvironment environment;


        protected ClaimsPrincipal AuthUser => HttpContext.User;
        protected string AuthUserName => AuthUser.Identity.Name;
        protected string AuthUserId => AuthUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;



        public TestResultController(ITestResultRepo testResult, AppDbContext _dbContext, IWebHostEnvironment _environment) : base(testResult)
        {

            dbContext = _dbContext;
            environment = _environment;

        }


        [HttpGet("byTestCaseId")]
        public async Task<ActionResult<IEnumerable<TestResult>>> GetTestResultbyTestCase(int testcaseid)
        {
            try
            {

                var testCases = await dbContext.TestResults
                    .Where(m => m.TestCaseId == testcaseid)// && m.UserId == userId)
                    .Include(X => X.Document)
                    .ToListAsync();

                return testCases;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //private string DecodeJwtToken(string jwtToken)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.ReadJwtToken(jwtToken);

        //    // Extract the user ID claim from the token
        //    var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        //    if (userIdClaim != null)
        //    {
        //        return userIdClaim.Value;
        //    }
        //    else
        //    {
        //        // Log or handle the case where the user ID claim is not found in the token
        //        return null;
        //    }
        //}

        [HttpPost("CreateTestResult")]
        public async Task<ActionResult> CreateTestResult([FromForm] TestresultVm testresultVm)
        {
            try
            {

                //verify given test case it

                var OldTestCaseData = await dbContext.TestCases.FindAsync(testresultVm.TestCaseId);
                if (OldTestCaseData == null) { throw new Exception("Invalid Old Data"); }

                FileUploadResult res = await UploadHelper(testresultVm.File);

                TestResult testResult = new TestResult();
                testResult.TestCaseId = testresultVm.TestCaseId;
                testResult.ActualResult = testresultVm.ActualResult;
                testResult.Status = testresultVm.Status;
                testResult.TestedByUser = AuthUserName;
                testResult.UserId = AuthUserId;

                testResult.Document = new Document()
                {
                    CreatedDate = DateTime.Now,
                    FileName = res.FileName,
                    FilePath = res.RelativePath,
                    Status = true,
                    Createdby = AuthUserName
                };
                await dbContext.AddAsync(testResult);
                await dbContext.SaveChangesAsync();

                return Ok("Saved Successfully");

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        private async Task<FileUploadResult> UploadHelper(IFormFile FileObject)
        {
            FileUploadResult res = new FileUploadResult() { Status = false };
            if (FileObject == null || FileObject.Length == 0)
            {
                return res;
            }

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(FileObject.FileName);
            res.FileName = uniqueFileName;

            // Determine the subfolder path based on the current date
            string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");

            // Construct the full path to save the file
            string uploadsFolder = Path.Combine(environment.WebRootPath, "uploads", yearMonthFolder);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Create the subfolders if they don't exist
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await FileObject.CopyToAsync(stream);
            }
            res.AbsFullPath = uploadsFolder;
            res.RelativePath = "~/uploads/" + yearMonthFolder + "/" + uniqueFileName;
            return res;
        }
    }
}





