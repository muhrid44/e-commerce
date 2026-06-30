using Identity.Repository.Interfaces;
using Identity.Repository.Repositories;
using Identity.Service.Interfaces;
using Identity.Service.Services;

namespace IdentityAPI.Utils
{
    public static class DIRegistration
    {
        public static void DIRegister(IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IDbTransactionRepository, DbTransactionRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
