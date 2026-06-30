using Identity.Domain.Model;

namespace Identity.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> Register(UserRegistrationModel userRegistrationModel, CancellationToken cancellationToken);
        Task<UserModel> Login(UserLoginModel userLoginModel, CancellationToken cancellationToken);
        Task<UserModel> GetUserAsync(Guid guid, CancellationToken cancellationToken);
    }
}
