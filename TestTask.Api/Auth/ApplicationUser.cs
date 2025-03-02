using Microsoft.AspNetCore.Identity;

namespace TestTask.Api.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string? Country { get; set; }
        public string? Province { get; set; }
    }
}
