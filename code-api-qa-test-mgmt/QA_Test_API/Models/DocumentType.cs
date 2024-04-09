using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{

    public class DocumentType : _AbsDefEntities
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [JsonIgnore]
        public virtual List<DocumentUpload> DocumentUploads{ get; set; }

    }
}
