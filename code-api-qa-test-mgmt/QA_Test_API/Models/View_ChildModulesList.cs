using System.ComponentModel.DataAnnotations;

namespace QA_Test_Log.Models
{
    public class View_ChildModulesList
    {
        [Key]
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public int ProjectId { get; set; }
        public int? Parent_Module_Id { get; set; }
        public string ParentModuleName { get; set; }
        public bool HasSubModules { get; set; }
    }
}
