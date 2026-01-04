using Microsoft.AspNetCore.Identity;


namespace GestaoLoja.Data
{
    public static class Inicializacao
    {
        public static async Task CriaDadosIniciais(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // 1. Adicionar os Roles usando as constantes da tua classe Roles
            // Isto garante que os nomes na BD são exatamente: "Administrador", "Funcionario", "Cliente", "Fornecedor"
            string[] roles = { Roles.Admin, Roles.Funcionario, Roles.Cliente, Roles.Fornecedor };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var defaultAdmin = new ApplicationUser
            {
                UserName = "admin@loja.pt",
                Email = "admin@loja.pt",
                Nome = "Administrador",
                Apelido = "Principal",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userAdmin = await userManager.FindByEmailAsync(defaultAdmin.Email);
            if (userAdmin == null)
            {
                var resultado = await userManager.CreateAsync(defaultAdmin, "Admin#123");

                if (resultado.Succeeded)
                {
                    // Usa a constante Roles.Admin em vez de escrever "Administrador"
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin);
                }
            }

            var defaultFunc = new ApplicationUser
            {
                UserName = "func@loja.pt",
                Email = "func@loja.pt",
                Nome = "Funcionário",
                Apelido = "Loja",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userFunc = await userManager.FindByEmailAsync(defaultFunc.Email);
            if (userFunc == null)
            {
                var resultado = await userManager.CreateAsync(defaultFunc, "Func#123");

                if (resultado.Succeeded)
                {
                    // Usa a constante Roles.Funcionario
                    await userManager.AddToRoleAsync(defaultFunc, Roles.Funcionario);
                }
            }
        }
    }
}