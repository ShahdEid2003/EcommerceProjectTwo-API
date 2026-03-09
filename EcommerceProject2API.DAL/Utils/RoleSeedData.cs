using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.DAL.Utils
{
    public class RoleSeedData : ISeedData
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleSeedData(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

        }
        public async Task DataSeed()
        {
            string[] roles = ["User", "Admin", "SuperAdmin"];
            if (!await _roleManager.Roles.AnyAsync())
            {
                foreach (string role in roles)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }
    }
}
