using QA_Test_Log.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace QA_Test_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("MyPolicy")]
    public class LogsController : ControllerBase
    {
        private readonly IDatabaseHelper _databaseHelper;

        public LogsController(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public class Log
        {
            public int LogId { get; set; }
            public string Message { get; set; }
            // Add other properties as needed
        }

        [HttpGet]
        public IActionResult GetLogs()
        {
            try
            {
                var logs = _databaseHelper.ExecuteQuery<Log>();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval of logs
                return StatusCode(500, "Failed to retrieve logs");
            }
        }

    }
}
