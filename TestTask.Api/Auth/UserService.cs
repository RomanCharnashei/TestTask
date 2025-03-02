using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TestTask.Api.Infrastructure;

namespace TestTask.Api.Auth
{
    public class UserService : IUserService
    {
        private const string AccessLevelClaim = "AccessLevel";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task Register(string login, string password, bool agree)
        {
            var user = new ApplicationUser { UserName = login, Email = password };
            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                throw new ValidationException(createResult);
            }

            var addClaimResult = await _userManager.AddClaimAsync(user, new Claim(AccessLevelClaim, "medium"));

            if (!addClaimResult.Succeeded)
            {
                throw new ValidationException(addClaimResult);
            }

            await _signInManager.SignInAsync(user, isPersistent: agree);
        }

        public async Task CompleteStep2(ClaimsPrincipal principal, string country, string province)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                throw new ValidationException("Country is empty.");
            }

            if (string.IsNullOrWhiteSpace(province))
            {
                throw new ValidationException("Province is empty.");
            }

            var user = await _userManager.GetUserAsync(principal)
                ?? throw new ValidationException("No such user exists");

            user.Country = country;
            user.Province = province;

            var upateResult = await _userManager.UpdateAsync(user);

            if (!upateResult.Succeeded)
            {
                throw new ValidationException(upateResult);
            }

            var claims = await _userManager.GetClaimsAsync(user);

            var oldClaim = claims.First(x => x.Type == AccessLevelClaim);

            await _userManager.ReplaceClaimAsync(user, oldClaim, new Claim(AccessLevelClaim, "full"));

            await _signInManager.RefreshSignInAsync(user);
        }
    }
}
