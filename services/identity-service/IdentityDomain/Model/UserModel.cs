using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Model
{
    public class UserModel : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
