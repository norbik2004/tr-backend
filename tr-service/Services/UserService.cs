using Microsoft.AspNetCore.Identity;
using tr_core.DTO.User;
using tr_core.Entities;
using tr_core.Interfaces;
using tr_service.Exceptions;

namespace tr_service.Services
{
    public class UserService(UserManager<User> userManager) : IUserService
    {
        public async Task RegisterUserAsync(UserRegisterRequest request)
        { 
            var existingUser = await userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new BadRequestException("Użytkownik o podanym adresie email już istnieje");

            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errors);
            }

        }
    }
}
