using DealerMarket.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DealerMarket.WebApp.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;
        public LoginAuthorize(ValidateUserSession userSession)
        {
            _userSession = userSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                var controller = (UserController)context.Controller; //Controlador en el que se aplicará el filter.
                context.Result = controller.RedirectToAction("Index", "Home"); //Lo que hará el controlador
            }
            else
            {
                await next();
            }
        }
    }
}
