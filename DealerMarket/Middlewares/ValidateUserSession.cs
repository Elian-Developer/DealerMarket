using DealerMarket.Core.Application.Dtos.Account;
using DealerMarket.Core.Application.Helpers;

namespace DealerMarket.WebApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
                                //Este metodo "HasUser" busca la sesion en el key de "user" y verifica si hay
                                //algun usuario, si hay usuario retorna true, de lo contrario retorna false.
        public bool HasUser()  
        {
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if(user == null)
            {
                return false;        
            }

            return true;
        }
    }
}
