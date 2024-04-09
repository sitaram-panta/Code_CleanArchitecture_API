using QA_Test_Log.Interface;
using QA_Test_Log.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace QA_Test_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class ProjectController : GenericCRUDController<IProjectRepo,Project, int>
    {
        public ProjectController(IProjectRepo project): base(project)
        {
                
        }
    }
}
