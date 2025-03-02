using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using TestTask.Api.Database;

namespace TestTask.Api.Auth
{
    public static class AuthExtension
    {
        public static IServiceCollection AddApiAuthentication(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddSignInManager()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services
                .AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddCookie(IdentityConstants.ApplicationScheme, options => {
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = ctx => {
                            ctx.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = ctx => {
                            ctx.Response.StatusCode = 403;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MediumAccessLevel", policy => 
                    policy.RequireAssertion(context => 
                        context.User.HasClaim(c => c.Type == "AccessLevel" && (c.Value == "medium" || c.Value == "full"))));
                options.AddPolicy("FullAccessLevel", policy => policy.RequireClaim("AccessLevel", "full"));
            });

            return services;
        }
    }
}
