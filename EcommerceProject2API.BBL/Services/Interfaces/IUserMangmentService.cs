using EcommerceProject2API.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject2API.BBL.Services.Interfaces
{
    public interface IUserMangmentService
    {
        Task<List<UserListResponse>> GetAllUsers();
        Task<UserDetailsResponse?> GetUser(string userId);
        Task<bool>ChangeRole(string userId, string roleName);
        Task<bool> ToggleBlockUser(string userId);
        Task<bool> DeleteUser(string userId);
    }
}
