using DealerMarket.Core.Application.Dtos.Account;
using DealerMarket.Core.Application.Helpers;
using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Core.Application.ViewModels.User;
using DealerMarket.WebApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace DealerMarket.WebApp.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserService _userService;
       

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userAuthentication = await _userService.LoginAsync(vm);
            if(userAuthentication != null && userAuthentication.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userAuthentication);
                return RedirectToRoute(new { controller = "Home", action ="Index" });
            }
            else
            {
                vm.HasError = userAuthentication.HasError;
                vm.Error = userAuthentication.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            RegisterResponse registerResponse = await _userService.RegisterAsync(vm, origin);
            if (registerResponse.HasError)
            {
                vm.HasError = registerResponse.HasError;
                vm.Error = registerResponse.Error;
                return View(vm);

            }
            return RedirectToRoute(new {controller = "User", action="Index"});
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail",response);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse forgotResponse = await _userService.ForgotPasswordAsync(vm, origin);
            if (forgotResponse.HasError)
            {
                vm.HasError = forgotResponse.HasError;
                vm.Error = forgotResponse.Error;
                return View(vm);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token});
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            ResetPasswordResponse resetResponse = await _userService.ResetPasswordAsync(vm);
            if (resetResponse.HasError)
            {
                vm.HasError = resetResponse.HasError;
                vm.Error = resetResponse.Error;
                return View(vm);
            }
            return RedirectToRoute(new {controller = "User", action = "Index"});    
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
