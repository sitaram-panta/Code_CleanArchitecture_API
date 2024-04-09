using System.ComponentModel.DataAnnotations;

namespace QA_Test_Log.Viewmodel
{
    public class ForgotPasswordVm
    {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        
    }
}
