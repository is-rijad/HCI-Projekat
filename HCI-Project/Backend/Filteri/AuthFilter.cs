using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.Filteri {
    public class AuthFilter : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authServis = context.HttpContext.RequestServices.GetService<AuthServis>();
            var isLogiran = authServis?.IsLogiran().Result;
            if (isLogiran == null) {
                return;
            }
            if (!isLogiran.Value) {
                context.Result = new UnauthorizedObjectResult("Niste logirani!");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
