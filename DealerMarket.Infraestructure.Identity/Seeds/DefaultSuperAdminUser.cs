using DealerMarket.Core.Application.Enums;
using DealerMarket.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Infraestructure.Identity.Seeds
{
    public class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();    // Here we are setting properties to defaultUser
            defaultUser.UserName = "superadminuser";
            defaultUser.Email = "superadminuser@email.com";
            defaultUser.FirtsName = "John";
            defaultUser.LastName = "Doe";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if(userManager.Users.All(user => user.Id != defaultUser.Id)) // Here we are confirming if user exist already.
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email); // Here we are validiting if user Email is already exist.

                if(user == null)  //if user not exist, then we proceed to create user
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!"); // here we are Creating defaultUser and setting password.
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString()); // here we are assigning basic role.
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());  // here we are assigning admin role.
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString()); // here we are assigning SuperAdmin role.
                }
            }
        }
    }
}
