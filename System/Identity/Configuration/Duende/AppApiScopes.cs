namespace InteractiveHelper.Identity;

using Duende.IdentityServer.Models;
using InteractiveHelper.Common.Security;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.AdminCatalog, "Apply changes to the catalog"),
            new ApiScope(AppScopes.AdminQuiz, "Construct quizes"),
            new ApiScope(AppScopes.SupportOrders, "Update order details and cancel orders"),
            new ApiScope(AppScopes.AuthenticatedUser, "Make orders"),
        };
}
