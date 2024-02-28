using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;

namespace SecondIntegration.Authorization
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

                if (apiKey != null && apiKey.Equals("A8F6F570-6AB8-497C-A69E-670721B1F551"))
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
