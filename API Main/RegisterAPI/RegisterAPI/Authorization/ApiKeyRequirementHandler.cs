using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;

namespace RegisterAPI.Authorization
{
    public class ApiKeyRequirementHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        const string API_KEY_HEADER_NAME = "X-API-KEY";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            SucceedRequirementIfApiKeyPresentAndValid(context, requirement);
            return Task.CompletedTask;
        }
        private static void SucceedRequirementIfApiKeyPresentAndValid(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                string? apiKey = httpContext.Request.Headers[API_KEY_HEADER_NAME].FirstOrDefault();

                if (apiKey != null && apiKey.Equals("2B8C382D-4842-4C0E-9D07-2AC85EFFAF0C"))
                {
                    ClaimsIdentity identity = new(
                    new GenericIdentity(apiKey, "Login"),
                    new[] {
                        new Claim("Cultura", "pt-BR")
                    });

                    ClaimsIdentity claimsIdentity = new(identity);

                    ClaimsPrincipal principal = new(claimsIdentity);

                    httpContext.User = principal;

                    context.Succeed(requirement);
                }
            }
        }
    }
}
