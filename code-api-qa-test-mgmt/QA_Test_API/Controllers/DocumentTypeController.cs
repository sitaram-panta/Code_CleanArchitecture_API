using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    public class DocumentTypeController : ControllerBase
    {

        private readonly AppDbContext _context;
        protected ClaimsPrincipal AuthUser => HttpContext.User;
        protected string AuthUserName => AuthUser.Identity.Name;

        public DocumentTypeController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetDocumentTypes()
        {
            var documentTypes = await _context.DocumentTypes.ToListAsync();
            return Ok(documentTypes);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var documentType = await _context.DocumentTypes.FindAsync(id);

            if (documentType == null)
            {
                return NotFound();
            }

            return Ok(documentType);
        }


        [HttpPost]
        public async Task<IActionResult> Create(DocumentTypeVm documentTypeVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = AuthUserName; // Get the username from the token

            var documentType = new DocumentType
            {
                Title = documentTypeVm.Title,
                CreatedName = username, // Set the CreatedBy property to the username
                Status = true,
                Deleted= false,
                UpdatedName = username,
            };

            _context.DocumentTypes.Add(documentType);
            await _context.SaveChangesAsync();

            return Ok(documentType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DocumentTypeVm documentTypeVm)
        {
            if (id != documentTypeVm.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var existingDocumentType = await _context.DocumentTypes.FindAsync(id);

            if (existingDocumentType == null)
            {
                return NotFound();
            }

            var username = AuthUserName; // Get the username from the token

            existingDocumentType.Title = documentTypeVm.Title; // Update only the Title property
            existingDocumentType.UpdatedName = username; // Set the UpdatedBy property to the username

            await _context.SaveChangesAsync();

            var editResult = new EditResult<DocumentType>
            {
                IsSuccess = true,
                Message = "DocumentType updated successfully",
                EditedEntity = existingDocumentType
            };

            return Ok(editResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentType(int id)
        {
            var documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return NotFound();
            }

            _context.DocumentTypes.Remove(documentType);
            await _context.SaveChangesAsync();

            var deleteResult = new DeleteResult<DocumentType>
            {
                IsSuccess = true,
                Message = "DocumentType deleted successfully",
                DeletedEntity = documentType
            };

            return Ok(deleteResult);
        }

    }
}
