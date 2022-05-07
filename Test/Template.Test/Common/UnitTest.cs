namespace InteractiveHelper.Test.Common;

using InteractiveHelper.Test.Services;
using InteractiveHelper.Db.Context;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

[TestFixture]
public abstract class UnitTest
{
    protected TestServices services;

    protected IDbContextFactory<MainDbContext> contextFactory;
    protected async Task<MainDbContext> DbContext() => await contextFactory.CreateDbContextAsync();

    [TearDown]
    public async virtual Task TearDown()
    {
        await ClearDb();
    }

    protected async virtual Task ClearDb()
    {
        // Nothing to do
    }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        services = new TestServices();
        contextFactory = services.Get<IDbContextFactory<MainDbContext>>();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        //
    }
}
