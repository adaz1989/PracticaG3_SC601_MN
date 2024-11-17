using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G3_Practice.Models;

namespace G3_Practice.Models
{
    public class RolInitialize
    {
        public static void Inicializar()
        {
            var rolManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            // Roles predeterminados
            List<string> roles = new List<string>();
            roles.Add("Admin");
            roles.Add("Usuario");

            foreach (var rol in roles)
            {
                if (!rolManager.RoleExists(rol))
                {
                    //Crear el rol
                    rolManager.Create(new IdentityRole(rol));
                }
            }

            //Usuario por defecto, inicio de la aplicacion
            var adminUser = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
            string Contrasena = "Admin123";

            if (userManager.FindByEmail(adminUser.Email) == null)
            {
                var creacion = userManager.Create(adminUser, Contrasena);
                if (creacion.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }
        }
    }
}
