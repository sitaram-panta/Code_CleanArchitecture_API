using QA_Test_Log.Data;
using QA_Test_Log.Interface;
using QA_Test_Log.Models;

namespace QA_Test_Log.Services
{
    public class TaskRepo: _AbsGenericRepo<Tasks, int>, ITaskRepo
    {
        public TaskRepo(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
