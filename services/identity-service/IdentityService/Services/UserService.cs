using Identity.Domain.Model;
using Identity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Service.Services
{
    public class UserService : IUserService
    {
        public Task<UserModel> GetUserAsync(Guid guid, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Login(UserLoginModel userLoginModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Register(UserRegistrationModel userRegistrationModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
