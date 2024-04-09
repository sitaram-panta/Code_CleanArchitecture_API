using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Models
{
   
    public class GeneralPagesVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int? ModuleId { get; set; }
    }
}
