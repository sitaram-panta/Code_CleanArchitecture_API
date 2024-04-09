
using Microsoft.AspNetCore.Mvc;
using QA_Test_Log.Models;
using QA_Test_Log.Interface;
using QA_Test_Log.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using QA_Test_Log.Viewmodel;
using QA_Test_Log.Services;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Identity.Client;

namespace QA_Test_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ModuleController : GenericCRUDController<IModuleRepo, Module, int>
    {
        private readonly AppDbContext  dbContext;

        public ModuleController(IModuleRepo module, AppDbContext _dbContext) : base(module)
        {
            dbContext = _dbContext;
        }

        [HttpGet("ModulesbyProject/{projectId}")]
        public async Task<ActionResult<IEnumerable<ModuleDetailsViewModel>>> GetModulesByProjectId(int projectId)
        {
            var modules = await dbContext.Modules
                .Where(m => m.ProjectId == projectId)
                .ToListAsync();

            var moduleViewModels = new List<ModuleDetailsViewModel>();

            foreach (var module in modules)
            {
                var viewModel = new ModuleDetailsViewModel
                {
                    Id = module.Id,
                    ModuleName = module.ModuleName,
                    Status = module.Status,
                    ProjectId = module.ProjectId,
                    Parent_Module_Id = module.Parent_Module_Id
                };

                if (viewModel.Parent_Module_Id.HasValue && viewModel.Parent_Module_Id > 0)
                {
                    var parentInfo = await dbContext.Modules.FindAsync(viewModel.Parent_Module_Id.Value);
                    if (parentInfo != null)
                    {
                        viewModel.ParentModuleName = parentInfo.ModuleName;
                    }
                }

                moduleViewModels.Add(viewModel);
            }

            return Ok(moduleViewModels);
        }

        [HttpGet("ModulesbyNullParentId/{projectFilterId}")]
        public async Task<ActionResult<IEnumerable<ModuleDetailsViewModel>>> GetModulesForNullParentId(int projectFilterId)
        {
            var modules = await dbContext.Modules
                .Where(m => m.ProjectId == projectFilterId && m.Parent_Module_Id == null)
                .ToListAsync();

            var moduleViewModels = new List<ModuleDetailsViewModel>();

            foreach (var module in modules)
            {
                // Check if the current module has submodules
                bool hasSubmodules = dbContext.Modules.Any(submodule => submodule.Parent_Module_Id == module.Id);

                var viewModel = new ModuleDetailsViewModel
                {
                    Id = module.Id,
                    ModuleName = module.ModuleName,
                    Status = module.Status,
                    ProjectId = module.ProjectId,
                    Parent_Module_Id = module.Parent_Module_Id,
                    HasSubModules = hasSubmodules // Add this property to the view model
                };

                moduleViewModels.Add(viewModel);
            }

            return Ok(moduleViewModels);
        }



        [HttpGet("{id}")]
        public override async Task<ActionResult<Module>> GetDetails(int id)
        {
            var module = await dbContext.Modules.FindAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            var viewModel = new ModuleDetailsViewModel()
            {
                Id = id,
                ModuleName = module.ModuleName,
                Status = module.Status,
                ProjectId = module.ProjectId,
                Parent_Module_Id = module.Parent_Module_Id,

            };
            if (viewModel.Parent_Module_Id.HasValue && viewModel.Parent_Module_Id > 0)
            {
                var parentInfo = await dbContext.Modules.FindAsync(viewModel.Parent_Module_Id.Value);
                if (parentInfo != null)
                {
                    viewModel.ParentModuleName = parentInfo.ModuleName;
                }
                return Ok(viewModel);
            }
            else
            {

                return module;

            }

        }

        [HttpGet("ListSubModules/{Parent_Module_Id}")]
        public async Task<IActionResult> GetChildModulesWithParentId(int Parent_Module_Id)
        {
            var result = await dbContext.View_ChildModulesList
                .Where(module => module.Parent_Module_Id == Parent_Module_Id)
                .ToListAsync();

            return Ok(result);
        }




    }



}


