using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.User;

namespace tr_core.Services
{
    public interface IUserService
    {
        public Task RegisterUserAsync(UserRegisterRequest request);
    }
}
