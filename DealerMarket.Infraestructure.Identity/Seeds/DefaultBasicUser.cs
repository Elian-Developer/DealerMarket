using DealerMarket.Core.Application.Enums;
using DealerMarket.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerMarket.Infraestructure.Identity.Seeds
{
    public class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();  //Estableciendo las propiedades del DefaultUser
            defaultUser.UserName = "basicUser";
            defaultUser.Email = "basicuser@email.com";
            defaultUser.FirtsName = "John";
            defaultUser.LastName = "Doe";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if(userManager.Users.All(user => user.Id != defaultUser.Id)) //Verificando si ya el usuario no está creado.
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email); //Verificando que el Email de el usuario no esté creado.

                if(user == null)  // Si retorna null es porque el usuario no esta creado.
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");  //Procedemos a crear el usuario y asignarle una contraseña.
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString()); // Le asignamos un rol al usuario creado.
                }
            }
        }
    }
}
