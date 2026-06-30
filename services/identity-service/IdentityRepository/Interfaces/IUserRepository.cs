using Identity.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> Register(UserRegistrationModel userRegistrationModel, CancellationToken cancellationToken);
        Task<UserModel> Login(UserLoginModel userLoginModel, CancellationToken cancellationToken);
        Task<UserModel> GetUserAsync(Guid guid, CancellationToken cancellationToken);
    }
}
