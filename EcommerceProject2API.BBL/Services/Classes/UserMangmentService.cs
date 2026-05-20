using EcommerceProject2API.BBL.Services.Interfaces;
using EcommerceProject2API.DAL.DTO.Response;
using EcommerceProject2API.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Classes
{
    public class UserMangmentService : IUserMangmentService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserMangmentService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager){
           _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> ChangeRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return false;

            var rolesExists = await _roleManager.RoleExistsAsync(role);

            if (!rolesExists)
                return false;

            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await _userManager.AddToRoleAsync(user, role);

            return result.Succeeded;
        }

        public Task<bool> DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserListResponse>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Adapt<List<UserListResponse>>();

        }

        public async Task<UserDetailsResponse?> GetUser(string userId)
        {
            var user=await _userManager.FindByIdAsync(userId);
            var roles=await _userManager.GetRolesAsync(user);
            var result= user.Adapt<UserDetailsResponse>();
            result.Role = roles.FirstOrDefault();
            return result;
        }

        public async Task<bool> ToggleBlockUser(string userId)
        {
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            bool isBlocked = user.LockoutEnd> DateTime.UtcNow;

            //  إذا محظور فك الحظر
            if (isBlocked)
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
            }
            // إذا مش محظور احظره لمدة 5 أيام
            else
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(
                    user,
                    DateTime.UtcNow.AddDays(5)
                );
            }

            return true;
        }
        
    }
}
