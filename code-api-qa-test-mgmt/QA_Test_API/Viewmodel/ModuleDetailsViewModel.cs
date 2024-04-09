using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Viewmodel
{
    public class ModuleDetailsViewModel
    {
        [Key]
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public bool Status { get; set; }
        public int ProjectId { get; set; }
        public int? Parent_Module_Id { get; set; }
       
        public string ParentModuleName { get; set; }

        public bool HasSubModules { get; set; }
    }

}
