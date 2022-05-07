namespace InteractiveHelper.Identity;

using Duende.IdentityServer.Models;
using InteractiveHelper.Common.Security;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.Common, "Common scope")
        };
}
