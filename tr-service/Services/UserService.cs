using Microsoft.AspNetCore.Identity;
using tr_core.Entities;
using tr_core.Interfaces;

namespace tr_service.Services
{
    public class UserService(UserManager<User> userManager) : IUserService
    {
    }
}
