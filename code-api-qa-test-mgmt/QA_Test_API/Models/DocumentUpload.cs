using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{
    public class DocumentUpload:_AbsDefEntities
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public int? ModuleId { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
       
        public String FileName { get; set; }
        
        public string FilePath { get; set; }

        [ForeignKey("ProjectId"), JsonIgnore]
        public Project Project { get; set; }

        [ForeignKey("ModuleId"), JsonIgnore]
        public Module Module { get; set; }

        [ForeignKey("DocumentTypeId"), JsonIgnore]
        public DocumentType DocumentType { get; set; }


    }
}
