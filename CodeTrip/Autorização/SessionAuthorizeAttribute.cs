using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CodeTrip.Autenticacao;

namespace CodeTrip.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public bool AllowAnonymous { get; set; } = false;

        public string? RoleAnyOf { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (AllowAnonymous)
            {
                base.OnActionExecuting(context);
                return;
            }

            var http = context.HttpContext;
            var role = http.Session.GetString(SessionKeys.UserRole);
            var userId = http.Session.GetInt32(SessionKeys.UserId);

            if (userId == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", new { returnUrl = http.Request.Path });
                return;
            }

            if (!string.IsNullOrWhiteSpace(RoleAnyOf))
            {
                var allowed = RoleAnyOf.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (!allowed.Contains(role))
                {
                    context.Result = new RedirectToActionResult("AcessoNegado", "Auth", null);
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}