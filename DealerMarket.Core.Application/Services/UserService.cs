using AutoMapper;
using DealerMarket.Core.Application.Dtos.Account;
using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }


        /*------ 1- SERVICES TO LOGIN OR AUTHENTICATE WITH YOUR ACCOUNT -------*/
        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm) //Here we create the method to login.
        {

            AuthenticationRequest loginResquest = _mapper.Map<AuthenticationRequest>(vm);   /*Aqui creamos un objeto llamdo "LoginRequest" y mapeamos el LoginViewModel  a un "authenticationRequest"*/

            AuthenticationResponse userResponse = await _accountService.AuthenticationAsync(loginResquest);  /*Aqui creamos un objeto llamdo "userResponse" y llamamos el metodo para autenticarse
                                                                                                                * de nuestro "accountService" y le pasamos el objeto que contiene el mapeo que es "loguinRequest" */
            return userResponse;
        }


        /*------ 2- SERVICES TO SignOut  -------*/
        public async Task SignOutAsync()  //Here we create the method to LogOut
        {
            await _accountService.SignOutAsync(); //Here we called our accountService method "SignOutAsync" to LogOut.
        }


        /*------ 3- SERVICES TO REGISTER NEW ACCOUNT -------*/
        public async Task<RegisterResponse> RegisterAsync(RegisterViewModel vm, string origin)  //Here we create the method to register new account.
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);    /*Aqui creamos un objeto llamdo "registerRequest" y mapeamos el RegisterViewModel  a un "registerRequest"*/

            return await _accountService.RegisterBasicUserAsync(registerRequest, origin); /*Aqui retornamos el metodo para registrar usuarios de nuestro "accountServices" y le mandamos por parametro 
                                                                                           los datos del mapeo del registerRequest, mas el origin que seria la tuta para el envio del correo.*/
        }


        /*------ 4- SERVICES TO CONFIRM EMAIL ACCOUNT -------*/
        public async Task<string> ConfirmEmailAsync(string userId, string token)  //Here we create the method to confirm Email account.
        {
            return await _accountService.ConfirmAccountAsync(userId, token);  /*Aqui retornamos el metodo para confirmar Email de nuestro "accountService" y le pasamos el userId y el token a utilizar.*/
        }


        /*------ 5- SERVICES TO FORGOT PASSWORD ACCOUNT -------*/
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)  //Here we create the method to forgot password.
        {
            ForgotPasswordRequest forgotPasswordRequest = _mapper.Map<ForgotPasswordRequest>(vm); /*Aqui creamos un objeto de ForgotPasswordRequest y mapeamos el ForgotViewModel al ForgotRequest*/

            return await _accountService.ForgotPasswordAsync(forgotPasswordRequest, origin);  /*Aqui retornamos el metodo para olvidar contraseña de nuestro "accountServices" y le mandamos por parametro 
                                                                                                los datos del mapeo del forgotRequest, mas el origin que seria la ruta para el envio del correo.*/
        }


        /*------ 6- SERVICES TO RESET PASSWORD ACCOUNT -------*/
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)  //Here we create Reset Password method
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);         //Here mapeamos el Reset View Model al reset request
            return await _accountService.ResetPasswordAsync(resetRequest);          //Here llamamos el metodo rest password de nuestro "accountService y le pasamos como parametro el objeto que contiene el mapping."
        }
    }
}
