using QA_Test_Log.Models;

namespace QA_Test_Log.Viewmodel
{
    public class TasksVm
    {
        public Types Types { get; set; }
        public string Title { get; set; }
        public string IdentityId { get; set; }
        public string Description { get; set; }
        public string? AssignedTo { get; set; }
        public Stage Stage { get; set; }



        // Foreign key properties for relationships
        public int ProjectId { get; set; }
        public int ModuleId { get; set; }

    }
}
