using DealerMarket.Core.Application.Dtos.Account;
using DealerMarket.Core.Application.Enums;
using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Infraestructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager; // This is to use the users and roles.
        private readonly SignInManager<ApplicationUser> _signInManager; //This is to use the process to authenticate logIn/LogOut
        private readonly IEmailService _emailService; //This is to use the send email services.

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        /* ------ 2- Process to authenticate / Login with your Account -------- */

        public async Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new(); // we create an object of AuthenticationResponse

            var user = await _userManager.FindByEmailAsync(request.Email); // We validate that user email not exist.

            if (user == null)
            {
                response.HasError = true;  // if user's email exist, we send an error explaining that user's email exist already.
                response.Error = $"No account registered with {request.Email}";
                return response;
            }

            // This method is used to authenticate an user with their User name and password.
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded) // Here we validate if result was not successful and send the error ocurred. 
            {
                response.HasError = true;
                response.Error = $"Invalid credential for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed) // Here we validate if the user is't confirmed and send the error ocurred.
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }

            //if All is correct, then return the values.
            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.Phone = user.PhoneNumber;
            var RolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false); //search the roles and assign to RolesList
            response.Roles = RolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        /* ------ 1.2- Process to LogOut --------*/
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();  // Here we doing the process to logOut.
        }


        /* ------ 2- Process to Register Account --------*/
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new();
            response.HasError = false;

            /* Here we verify that not exist users with the same
              username or with the same Email. If exist any user, 
              then we send an error whit the ocurred. */

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName); //Search users with the same username.
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"The Username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email); //Search users with the same Email.
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"The Email '{request.Email}' is already registered.";
                return response;
            }

            //If not found any users, then create an user object with the request values.

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirtsName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone
               
            };

            var result = await _userManager.CreateAsync(user, request.Password); //We Create the user in Identity.

            if (result.Succeeded) // We verify if all is successful, then we assign the user roles and send the email verification.
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString()); // We Assign User role.

                var verificationUri = await SendVerificationEmailUrl(user, origin); //We send the email confirmation.

                await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()  //We build the email confirmation.
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirm Registration"
                });
            }
            else
            {                                /*If ocurred an error, send the Error.*/
                response.HasError = true;
                response.Error = "An error ocurred while trying to register the user.";
                return response;
            }

            return response;
        }

        //2.1 With this method build the Uri that need to send in the email confirmation.
        private async Task<string> SendVerificationEmailUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id); 
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code); //aQUI LE ESTAMOS ESPEFICIANDO POR PARAMETRO QUE USE EL
                                                                                           //URI QUE CONTIENE EL USER ID MAS EL TOKEN.

            return verificationUri;                   
        }


        /* ------ 2.2- Process to confirm account --------*/
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId); // Here verify if the user exist by Id.
            if (user == null)
            {
                return $"No account registered With this user.";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token)); // Here "desencodeamos el token"
            var result = await _userManager.ConfirmEmailAsync(user, token); // Here we confirmed that token belong a this user.
            if (result.Succeeded)
            {
                return $"Account Confirmed for {user.Email}. Now you can use the App."; //If all is successful sen a message and finally confirm the account.
            }
            else
            {
                return $"Sorry, an error ocurred while confirming {user.Email}";
            }
        }

        /* ------ 3- Process to Forgot Password --------*/
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new();  //Create an ForgotPasswordResponse object called "response"
            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(request.Email);  // We verify if Email user exist. If not exist we send an error.
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts resgistered with {request.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUrl(user, origin);  //Here we assign the Uri and token to the variable "verificationUri".

            await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()  //Here We Build the email to reset.
            {
                To = user.Email,
                Body = $"Please reset your password account visiting this URL {verificationUri}",
                Subject = "Reset Password"
            });

            return response;
        }


        /* ------ 4- Process to Reset Password --------*/
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new();  //We created an ResetPasswordResponse object called response.
            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(request.Email);   //Here we verify if this user email exist. if not exist we send an error.
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered with {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));  //Here "desencodeamos el token" and we assign to the variable "request.Token".
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);  //Here Reset the password and send the user, the token and the new password by parameters.
            if (!result.Succeeded) //if something fails send the error, otherwise all worked successful.
            {
                response.HasError = true;
                response.Error = "An error ocurred while resetting your password";
                return response;
            }

            return response;
        }

        //4.1- With this method build the Uri that need to send in the reset password.
        private async Task<string> SendForgotPasswordUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user); //Here we generate the reset password token and we assign to variable "code"
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)); //Here "Encodeamos el token" and we assign to variable "code" again.
            var route = "User/ResetPassword";  // Here we epecify the route.
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code); //Here we build the uri to send email.

            return verificationUri; //Here we return the URI Builded
        }

    }
}
