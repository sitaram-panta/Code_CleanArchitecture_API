using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{
    public abstract class _AbsDefEntity
    {
       
        public bool Status { get; set; }
       
        public bool Deleted { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        
        public string UpdatedBy { get; set; }
        
        public DateTime UpdatedOn { get; set; }=DateTime.Now;

    }

    public class GeneralPages : _AbsDefEntity
    {
       
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public int ProjectId { get; set; }
        public int? ModuleId { get; set; }

        [ForeignKey("ProjectId"), JsonIgnore]
        public Project Project { get; set; }

        [ForeignKey("ModuleId"), JsonIgnore]
        public Module Module { get; set; }

    }
}
