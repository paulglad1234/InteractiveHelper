namespace InteractiveHelper.Test.Common;

using InteractiveHelper.Test.Common.Extensions;
using InteractiveHelper.Test.ApiServerTest;
using InteractiveHelper.Test.IdentityServerTest;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.User;
using InteractiveHelper.Identity;
using InteractiveHelper.Settings;
using Duende.IdentityServer.Test;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InteractiveHelper.Common.Security;

public abstract partial class ComponentTest
{
    protected TestServer apiServer;
    protected HttpClient apiClient;
    protected IApiSettings apiSettings;


    protected TestServer identityServer;
    protected HttpClient identityClient;


    protected IDbContextFactory<MainDbContext> contextFactory;
    protected UserManager<User> userManager;

    protected virtual IConfiguration? ApiServerConfiguration => null;
    protected virtual Action<IServiceCollection>? ApiServerConfigurator => null;

    protected virtual IConfiguration? IdentityServerConfiguration => null;
    protected virtual Action<IServiceCollection>? IdentityServerConfigurator => null;

    protected async Task<MainDbContext> DbContext() => await contextFactory.CreateDbContextAsync();

    [OneTimeSetUp]
    public async virtual Task OneTimeSetUp()
    {
        identityServer = IdentityServerInitializer.Initialize(IdentityServerConfiguration, IdentityServerConfigurator);
        identityClient = identityServer.CreateClient();

        apiServer = ApiServerInitializer.Initialize(identityClient, ApiServerConfiguration, ApiServerConfigurator);
        apiClient = apiServer.CreateClient();

        contextFactory = apiServer.ResolveService<IDbContextFactory<MainDbContext>>();
        userManager = apiServer.ResolveService<UserManager<User>>();
        apiSettings = apiServer.ResolveService<IApiSettings>();
    }

    [TearDown]
    public async virtual Task TearDown()
    {
        await ClearDb();
    }

    [OneTimeTearDown]
    public async virtual Task OneTimeTearDown()
    {
        apiServer?.Dispose();
    }

    protected async virtual Task ClearDb()
    {
        // Nothing to do
    }

    protected async Task<TokenResponse> AuthenticateTestUser(string username, string password, string scope)
    {
        var clientId = apiSettings.IdentityServer.ClientId;
        var clientSecret = apiSettings.IdentityServer.ClientSecret;
        return await identityClient.GetApiAccessToken(username, password, scope, clientId, clientSecret);
    }

    protected TestUser GetTestUser()
    {
        return AppApiTestUsers.ApiUsers.First();
    }

    protected static class Scopes
    {
        public static string Read => "offline_access " + AppScopes.Read;
        public static string Write => "offline_access " + AppScopes.Write;
        public static string ReadAndWrite => "offline_access " + AppScopes.Read + " " + AppScopes.Write;
        public static string Empty => "offline_access";
    }

    public async Task<string> AuthenticateUser_ReadAndWriteScope()
    {
        var user = GetTestUser();
        var tokenResponse = await AuthenticateTestUser(user.Username, user.Password, Scopes.ReadAndWrite);
        return tokenResponse.AccessToken;
    }

    public async Task<string> AuthenticateUser_EmptyScope()
    {
        var user = GetTestUser();
        var tokenResponse = await AuthenticateTestUser(user.Username, user.Password, Scopes.Empty);
        return tokenResponse.AccessToken;
    }
}
