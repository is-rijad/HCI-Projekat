using Backend.Servisi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.Filteri {
    public class MenadzerFilter : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authServis = context.HttpContext.RequestServices.GetService<AuthServis>();
            var nalog = authServis?.GetNalogInfo().Result;
            if (nalog == null)
            {
                return;
            }
            if (!nalog.IsMenadzer) {
                context.Result = new UnauthorizedObjectResult("Niste menadžer!");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
