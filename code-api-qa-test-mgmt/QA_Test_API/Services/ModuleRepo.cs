using QA_Test_Log.Data;
using QA_Test_Log.Interface;
using QA_Test_Log.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace QA_Test_Log.Services
{
    public  class ModuleRepo : _AbsGenericRepo<Module, int>, IModuleRepo


    {
        public ModuleRepo(AppDbContext dbContext) : base(dbContext) { }

    }

   


}
