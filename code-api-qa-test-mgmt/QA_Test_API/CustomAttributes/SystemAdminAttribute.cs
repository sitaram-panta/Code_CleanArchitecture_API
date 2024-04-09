using Microsoft.AspNetCore.Mvc.Filters;

namespace QA_Test_Log.CustomAttributes
{
    public class SystemAdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
        }
    }
}
