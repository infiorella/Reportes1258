using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PreventFromUrl : ActionFilterAttribute
    {
        
    }
}
