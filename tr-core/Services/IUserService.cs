using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.User.Request;
using tr_core.DTO.User.Response;

namespace tr_core.Services
{
    public interface IUserService
    {
        public Task RegisterUserAsync(UserRegisterRequest request);
        public Task<UserResponse> GetLoggedInUserInfoAsync(string userId);
        public Task<List<UserResponse>> GetAllUsers(UserPaginatedParamsRequest request);
    }
}
