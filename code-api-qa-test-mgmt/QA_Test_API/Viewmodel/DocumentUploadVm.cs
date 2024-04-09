using QA_Test_Log.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QA_Test_Log.Viewmodel
{

    
    public class DocumentUploadVm
    {
        
        public int? Id { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public int? ModuleId { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
        [Required]
        public IFormFile File { get; set; }
       


    }


}
