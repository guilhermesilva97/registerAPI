using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;

namespace FirstIntegration.Authorization
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

                if (apiKey != null && apiKey.Equals("E1ACD6A2-ABF2-4CFB-B200-180BB9A1EDB2"))
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
