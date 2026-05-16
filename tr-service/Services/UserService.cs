using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Stripe;
using tr_core.Consts;
using tr_core.DTO.User.Request;
using tr_core.DTO.User.Response;
using tr_core.Entities;
using tr_core.Repositories;
using tr_core.Services;
using tr_service.Exceptions;

namespace tr_service.Services
{
    public class UserService(UserManager<User> userManager, IUserRepository userRepository, IMapper mapper) : IUserService
    {
        public async Task<List<UserResponse>> GetAllUsers(UserPaginatedParamsRequest request)
        {
            var users = await userRepository.GetAllAsync();

            var usersToReturn = mapper.Map<List<UserResponse>>(users);

            for(int i = 0; i < usersToReturn.Count; i++)
            {
                List<string> roles = [.. await userManager.GetRolesAsync(users[i])];
                usersToReturn[i].Roles = roles;
            }

            return usersToReturn;
        }

        public async Task<UserResponse> GetLoggedInUserInfoAsync(string userId)
        {
            var user = await userRepository.GetByIdAsync(userId)
                ?? throw new NotFoundException("User not found");

            List<string> roles = [.. await userManager.GetRolesAsync(user)];

            var userToReturn = mapper.Map<UserResponse>(user);
            userToReturn.Roles = roles;

            return userToReturn;
        }

        public async Task RegisterUserAsync(UserRegisterRequest request)
        { 
            var existingUser = await userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new BadRequestException("Account with this email address arelady exists");

            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                NormalizedEmail = request.Email.ToUpper(),
                NormalizedUserName = request.UserName.ToUpper(),
            };

            var result = await userManager.CreateAsync(user, request.Password);
           
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errors);
            }

            await userManager.AddToRoleAsync(user, Roles.User);

            var settings = new UserSetting
            {
                UserId = user.Id,
                IsDarkMode = true,
                ReceiveNotifications = false
            };

            user.UserSettings = settings;
            await userRepository.SaveChangesAsync();
        }

        public async Task SetStripeCustomerId(string userId, string customerId)
        {
            var user = await userRepository.GetByIdAsync(userId)
                ?? throw new NotFoundException("User not found");

            user.StripeCustomerId = customerId;

            await userRepository.SaveChangesAsync();
        }

        public async Task UpdateSubsciptionStatus(string userId, bool status)
        {
            var user = await userRepository.GetByIdAsync(userId)
                ?? throw new NotFoundException("User not found");

            user.IsSubscribed = status;

            await userRepository.SaveChangesAsync();
        }
    }
}
