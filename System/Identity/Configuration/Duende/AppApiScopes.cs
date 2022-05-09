namespace InteractiveHelper.Identity;

using Duende.IdentityServer.Models;
using InteractiveHelper.Common.Security;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.Read, "Read content"),
            new ApiScope(AppScopes.Write, "Create, Update and Delete content")
        };
}
