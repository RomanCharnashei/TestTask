using System.Security.Claims;

namespace TestTask.Api.Auth
{
    public interface IUserService
    {
        Task Register(string login, string password, bool agree);
        Task CompleteStep2(ClaimsPrincipal principal, string country, string province);
    }
}