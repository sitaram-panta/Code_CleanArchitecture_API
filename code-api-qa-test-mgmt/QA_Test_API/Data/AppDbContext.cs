using QA_Test_Log.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace QA_Test_Log.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<View_LastTestResult> View_LastTestResult { get; set; }
        public DbSet<View_TestCaseWithLastResult> View_TestCaseWithLastResult { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<View_ChildModulesList> View_ChildModulesList { get; set; }
        public DbSet<GeneralPages> GeneralPages { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<OTPVerificationLog> OTPVerificationLogs { get; set; }
        public DbSet<DocumentUpload> DocumentUploads { get; set; }
        public DbSet<Tasks> Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
