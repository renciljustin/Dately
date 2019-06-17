using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dately.Core.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Dately.Core.Seeds
{
    public class SeedUsersAndRoles
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUsersAndRoles(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void BeginSeeding()
        {
            SeedRoles();
            SeedUsers();
        }

        private void SeedRoles()
        {
            if (!_roleManager.Roles.Any())
            {
                var data = File.ReadAllText("Core/Seeds/roles.json");
                var roles = JsonConvert.DeserializeObject<List<IdentityRole>>(data);

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }
            }
        }

        private void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var data = File.ReadAllText("Core/Seeds/users.json");
                var users = JsonConvert.DeserializeObject<List<User>>(data);

                var admin = users.SingleOrDefault(u => u.UserName == "Rencil");

                if (admin != null)
                {
                    var result = _userManager.CreateAsync(admin, "P@ssw0rd").Result;

                    if (result.Succeeded)
                        _userManager.AddToRoleAsync(admin, "Admin").Wait();
                }

                foreach(var user in users.Where(u => u.UserName != "Rencil"))
                {
                    var result = _userManager.CreateAsync(user, "P@ssw0rd").Result;

                    if (result.Succeeded)
                        _userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }
    }
}