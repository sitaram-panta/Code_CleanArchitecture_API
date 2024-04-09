using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QA_Test_Log.Data;
using QA_Test_Log.Models;
using QA_Test_Log.Viewmodel;
using System.Security.Claims;

namespace QA_Test_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("MyPolicy")]

    public class DocumentUploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment environment;
        protected ClaimsPrincipal AuthUser => HttpContext.User;
        protected string AuthUserName => AuthUser.Identity.Name;

        public DocumentUploadController(AppDbContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            environment = _environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentUploads()
        {
            var documentUploads = await _context.DocumentUploads

                .ToListAsync();
            return Ok(documentUploads);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var documentUpload = await _context.DocumentUploads.FindAsync(id);

            if (documentUpload == null)
            {
                return NotFound();
            }

            return Ok(documentUpload);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DocumentUploadVm documentUploadVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (documentUploadVm.File == null || documentUploadVm.File.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var filePath = await UploadHelper(documentUploadVm.File);

                if (filePath == null)
                {
                    return BadRequest("File upload failed.");
                }

                var documentUpload = new DocumentUpload
                {
                    ProjectId = documentUploadVm.ProjectId,
                    ModuleId = documentUploadVm.ModuleId,
                    DocumentTypeId = documentUploadVm.DocumentTypeId,
                    CreatedName = AuthUserName,
                    FileName = Path.GetFileName(filePath),
                    FilePath = filePath,
                    UpdatedName = AuthUserName,
                    Status=true,
                    Deleted=false
                    
                };

                _context.DocumentUploads.Add(documentUpload);
                await _context.SaveChangesAsync();
              
                return Ok($"File uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }






        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] DocumentUploadVm documentUploadsVM)
        {
            if (id != documentUploadsVM.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var existingDocumentUpload = await _context.DocumentUploads.FindAsync(id);

            if (existingDocumentUpload == null)
            {
                return NotFound();
            }

            var username = AuthUserName; // Get the username from the token

            // Update non-file properties
            existingDocumentUpload.ProjectId = documentUploadsVM.ProjectId;
            existingDocumentUpload.ModuleId = documentUploadsVM.ModuleId;
            existingDocumentUpload.DocumentTypeId = documentUploadsVM.DocumentTypeId;
            existingDocumentUpload.UpdatedName = username; // Set the UpdatedBy property to the username

            if (documentUploadsVM.File != null && documentUploadsVM.File.Length > 0)
            {
                // A new file has been uploaded, update the file
                var filePath = await UploadHelper(documentUploadsVM.File);

                if (filePath == null)
                {
                    return BadRequest("File upload failed.");
                }

                // Remove the old file (optional)
                var oldFilePath = existingDocumentUpload.FilePath;
                if (!string.IsNullOrEmpty(oldFilePath) && System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                existingDocumentUpload.FileName = Path.GetFileName(filePath);
                existingDocumentUpload.FilePath = filePath;
            }

            await _context.SaveChangesAsync();

            var editResult = new EditResult<DocumentUpload>
            {
                IsSuccess = true,
                Message = "DocumentUpload updated successfully",
                EditedEntity = existingDocumentUpload
            };

            return Ok(editResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentUpload(int id)
        {
            var documentUpload = await _context.DocumentUploads.FindAsync(id);
            if (documentUpload == null)
            {
                return NotFound();
            }

            _context.DocumentUploads.Remove(documentUpload);
            await _context.SaveChangesAsync();

            var deleteResult = new DeleteResult<DocumentUpload>
            {
                IsSuccess = true,
                Message = "DocumentUpload deleted successfully",
                DeletedEntity = documentUpload
            };

            return Ok(deleteResult);
        }

        private async Task<string> UploadHelper(IFormFile FileObject)
        {
            if (FileObject == null || FileObject.Length == 0)
            {
                return null;
            }

            try
            {

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(FileObject.FileName);


                string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");


                string uploadsFolder = Path.Combine(environment.WebRootPath, "DocumentUpload", yearMonthFolder);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);


                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await FileObject.CopyToAsync(stream);
                }


                return filePath;
            }
            catch (Exception ex)
            {

                return null;
            }


        }

        [HttpGet("byProjectId")]
        public async Task<ActionResult<IEnumerable<DocumentUpload>>> GetUploadsbyProjectId(int projectid)
        {
            var result = await _context.DocumentUploads.Where(du => du.ProjectId == projectid).ToListAsync();

            return Ok(result);

        }
        [HttpGet("byModuleId")]
        public async Task<ActionResult<IEnumerable<DocumentUpload>>> GetUploadsbyModuleId(int moduleid)
        {
            var result = await _context.DocumentUploads.Where(du => du.ModuleId == moduleid).ToListAsync();

            return Ok(result);

        }


    }
}
