using System.ComponentModel.DataAnnotations;

namespace QA_Test_Log.Viewmodel
{
    public class DocumentTypeVm
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
