using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Deadline { get; set; } = DateTime.Now;

        //NAVIGATION PROPERTY
        [JsonIgnore]
        public virtual ICollection<Module> Module { get; set; }
        [JsonIgnore]
        public virtual ICollection<GeneralPages> GeneralPages { get; set; }

        [JsonIgnore]
        public virtual ICollection<DocumentUpload> DocumentUpload { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
