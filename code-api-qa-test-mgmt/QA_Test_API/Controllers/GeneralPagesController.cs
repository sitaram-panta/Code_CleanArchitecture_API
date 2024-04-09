using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QA_Test_Log.Data;
using QA_Test_Log.Models;
using System.Text.RegularExpressions;
using static Dapper.SqlMapper;

namespace QA_Test_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("MyPolicy")]
    public class GeneralPagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GeneralPagesController(AppDbContext context, IWebHostEnvironment _environment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            environment = _environment;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IActionResult> GetGeneralPages()
        {
            var getList = await _context.GeneralPages.ToListAsync();
            return Ok(getList);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralPages>> GetGeneralPagesbyId(int id)
        {
            var generalPage = await _context.GeneralPages.FindAsync(id);

            if (generalPage == null)
            {
                return NotFound();
            }

            return Ok(generalPage);
        }


        [HttpPost]
        public async Task<IActionResult> Create(GeneralPagesVM generalPageVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map the properties from ViewModel to the entity
            var generalPage = new GeneralPages
            {
                Title = generalPageVM.Title,
                Description = generalPageVM.Description,
                ProjectId = generalPageVM.ProjectId,
                ModuleId = generalPageVM.ModuleId
            };

            string username = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                generalPage.CreatedBy = username;
                generalPage.UpdatedBy = username;
            }
            generalPage.Slug = TransformTitle(generalPage.Title);

            _context.GeneralPages.Add(generalPage);
            await _context.SaveChangesAsync();

            return Ok(generalPage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GeneralPagesVM generalPageVM)
        {
            var existingEntity = await _context.GeneralPages.FindAsync(id);
            if (existingEntity != null)
            {
                // Map the properties from ViewModel to the existing entity
                existingEntity.Title = generalPageVM.Title;
                existingEntity.Description = generalPageVM.Description;
                existingEntity.ProjectId = generalPageVM.ProjectId;
                existingEntity.ModuleId = generalPageVM.ModuleId;

                existingEntity.Title = TransformTitle(existingEntity.Title);
                _context.GeneralPages.Update(existingEntity);
                await _context.SaveChangesAsync();
                return Ok(existingEntity);
            }
            else
            {
                return NoContent();
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles ="SystemAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var generalPage = await _context.GeneralPages.FindAsync(id);
            if (generalPage == null)
            {
                return NotFound();
            }

            _context.GeneralPages.Remove(generalPage);
            await _context.SaveChangesAsync();

            return Ok(generalPage);
        }


        [HttpGet("byProjectId/{projectid}")]
        public async Task<ActionResult<IEnumerable<GeneralPagesVM>>> GeneralPagewithProjectid(int projectid)
        {
            var generalPages = await _context.GeneralPages
                .Where(G => G.ProjectId == projectid)
                .Select(G => new GeneralPagesVM
                {
                    Id = G.Id, // Replace GeneralpageID with the actual property name from your GeneralPages class
                    Title = G.Title,
                    ProjectId = G.ProjectId
                })
                .ToListAsync();

            return generalPages;
        }

        [HttpGet("byModuleId/{moduleid}")]
        public async Task<ActionResult<IEnumerable<GeneralPagesVM>>> GeneralPagewithModuleid(int moduleid)
        {
            var generalPages = await _context.GeneralPages
                .Where(G => G.ModuleId == moduleid)
                .Select(G => new GeneralPagesVM
                {
                    Id = G.Id, // Replace GeneralpageID with the actual property name from your GeneralPages class
                    Title = G.Title,
                    ProjectId = G.ProjectId,
                    ModuleId = G.ModuleId // Include the ModuleId property
                })
                .ToListAsync();

            return generalPages;
        }

        [HttpGet("byGeneralpageID/{generalpageid}")]
        public async Task<ActionResult<string>> GeneralPageDescription(int generalpageid)
        {
            var generalPage = await _context.GeneralPages.FirstOrDefaultAsync(G => G.Id == generalpageid);

            if (generalPage == null)
            {
                return NotFound(); // Return a 404 Not Found if the GeneralPage with the specified ID is not found.
            }

            // Return the description of the GeneralPage.
            return generalPage.Description;
        }






        private string TransformTitle(string inputTitle)
        {
            return Regex.Replace(inputTitle, "[^-_A-Za-z0-9]", "");
        }



    }
}






















