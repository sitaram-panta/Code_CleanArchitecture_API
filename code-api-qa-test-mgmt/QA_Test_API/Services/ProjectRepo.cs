using QA_Test_Log.Data;
using QA_Test_Log.Interface;
using QA_Test_Log.Models;

namespace QA_Test_Log.Services
{
    public class ProjectRepo: _AbsGenericRepo<Project, int>, IProjectRepo
    {
        public ProjectRepo(AppDbContext context):base(context)
        {
            
        }
    }
}
