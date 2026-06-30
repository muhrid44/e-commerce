using Identity.Domain.Model;
using Identity.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace Identity.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IDbTransactionRepository _dbTransactionRepository;

        public UserRepository(UserManager<UserModel> userManager, IDbTransactionRepository dbTransactionRepository)
        {
            _userManager = userManager;
            _dbTransactionRepository = dbTransactionRepository;
        }

        public async Task<UserModel> GetUserAsync(Guid guid, CancellationToken cancellationToken)
        {
            //read cache from redis first

            //return user if found in redis

            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            return user;
        }

        public async Task<UserModel> Login(UserLoginModel userLoginModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(userLoginModel.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginModel.Password))
                throw new UnauthorizedAccessException("Invalid email or password.");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Your account has been deactivated.");

            var roles = await _userManager.GetRolesAsync(user);

            return user;
        }

        public async Task<Guid> Register(UserRegistrationModel userRegistrationModel, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(userRegistrationModel.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Email already in use.");

            var newUser = new UserModel
            {
                Email = userRegistrationModel.Email,
                Name = userRegistrationModel.Name,
                UserName = userRegistrationModel.Email,
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
            };

            await using var transaction = await _dbTransactionRepository.BeginTransactionAsync(cancellationToken);

            try
            {
                //1st operation
                var result = await _userManager.CreateAsync(newUser, userRegistrationModel.Password);

                if (!result.Succeeded)
                    throw new InvalidOperationException(
                        string.Join(", ", result.Errors.Select(e => e.Description)));

                //2nd operation
                await _userManager.AddToRoleAsync(newUser, userRegistrationModel.Role);

                //store cache on redis

                await transaction.CommitAsync(cancellationToken);

                return newUser.Id;

            } catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
