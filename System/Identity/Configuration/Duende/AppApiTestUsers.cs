namespace InteractiveHelper.Identity;

using Duende.IdentityServer.Test;

public static class AppApiTestUsers
{
    public static List<TestUser> ApiUsers =>
        new()
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "root",
                Password = "password"
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "admin",
                Password = "password"
            }
        };
}