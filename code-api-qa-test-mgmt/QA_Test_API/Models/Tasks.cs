using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public Types Types { get; set; }
        public string Title { get; set; }
        public string IdentityId { get; set; }//Identity Id
        public string Description { get; set; }
        public string? AssignedTo { get; set; }
        public Stage Stage { get; set; }
        public string FileAttachments { get; set; }
        public string Url { get; set; }
        public int ProjectId { get; set; }
        public int? ModuleId { get; set; }


        [ForeignKey("ProjectId"), JsonIgnore]
        public virtual Project Project { get; set; }

        [ForeignKey("ModuleId"), JsonIgnore]
        public virtual Module Module { get; set; }
    }
}

