using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using QA_Test_Log.Data;
using QA_Test_Log.Models;

namespace QA_Test_Log.AutoMigration
{
    public class Auto_Mig
    {
        public static void Initialize(AppDbContext appDb)
        {
            try
            {

                appDb.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw new Exception("Database migration failed. Please contact support.", ex);

            }



        }
    }
}
