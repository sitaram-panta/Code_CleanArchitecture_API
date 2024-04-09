using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QA_Test_Log.Data;
using QA_Test_Log.Interface;
using QA_Test_Log.Models;
using QA_Test_Log.Viewmodel;

namespace QA_Test_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : GenericCRUDController<ITaskRepo, Tasks, int>
    {
        private readonly AppDbContext context;
        private IWebHostEnvironment env;
        public TaskController(ITaskRepo taskRepo, AppDbContext _context, IWebHostEnvironment _env) : base(taskRepo)
        {
            context = _context;
            env = _env;
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTaskWithFiles([FromForm] TasksVm taskVm, [FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                List<string> uploadedFileNames = new List<string>();
                List<string> uploadedFileUrls = new List<string>();

                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                    {
                        continue;
                    }

                    string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                    string uploadsFolder = Path.Combine(env.ContentRootPath, "TaskFiles");
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    string relativeUrl = $"TaskFiles/{uniqueFileName}"; // Generate a relative URL
                    uploadedFileNames.Add(uniqueFileName); // Store file names
                    uploadedFileUrls.Add(relativeUrl); // Store relative URLs
                }

                // Map the ViewModel to the Model
                Tasks task = new Tasks
                {
                    Types = taskVm.Types,
                    Title = taskVm.Title,
                    IdentityId = taskVm.IdentityId,
                    Description = taskVm.Description,
                    AssignedTo = taskVm.AssignedTo,
                    Stage = taskVm.Stage,
                    FileAttachments = string.Join(";", uploadedFileNames), // Store uploaded file names in FileAttachments
                    Url = string.Join(";", uploadedFileUrls), // Store uploaded file URLs in Url
                    ProjectId = taskVm.ProjectId,
                    ModuleId = taskVm.ModuleId
                };

                // Add the task to the DbContext and save changes
                context.Tasks.Add(task);
                await context.SaveChangesAsync();

                return Ok("Task created with files uploaded successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"File upload failed: {ex.Message}");
            }

        }
    }
}
