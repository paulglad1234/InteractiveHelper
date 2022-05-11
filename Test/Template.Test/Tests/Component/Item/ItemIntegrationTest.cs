using InteractiveHelper.Db.Entities.Catalog;
using InteractiveHelper.Test.Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveHelper.Test.Tests.Component.Item;

[TestFixture]
public partial class ItemIntegrationTest : ComponentTest
{
    [SetUp]
    public async Task SetUp()
    {
        await using var context = await DbContext();

        context.ItemCharacteristics.RemoveRange(context.ItemCharacteristics);
        context.Characteristics.RemoveRange(context.Characteristics);
        context.Items.RemoveRange(context.Items);
        context.Brands.RemoveRange(context.Brands);
        context.Categories.RemoveRange(context.Categories);
        context.SaveChanges();
    }

    [TearDown]
    public async override Task TearDown()
    {
        await using var context = await DbContext();

        context.ItemCharacteristics.RemoveRange(context.ItemCharacteristics);
        context.Characteristics.RemoveRange(context.Characteristics);
        context.Items.RemoveRange(context.Items);
        context.Brands.RemoveRange(context.Brands);
        context.Categories.RemoveRange(context.Categories);
        context.SaveChanges();

        await base.TearDown();
    }

    protected static class Urls
    {
        private static string BaseUrl => "/api/v1/items";
        public static string GetItems(int? offset = null, int? limit = null)
        {
            if (offset is null && limit is null)
                return BaseUrl;

            var queryParameters = new List<string>();

            if (offset.HasValue)
                queryParameters.Add($"offset={offset}");

            if (limit.HasValue)
                queryParameters.Add($"limit={limit}");

            return BaseUrl + "?" + string.Join("&", queryParameters);
        }

        public static string GetItemById(int id) => BaseUrl + $"/{id}";
        public static string AddItem => BaseUrl;
        public static string UpdateItem(int id) => BaseUrl + $"/{id}";
        public static string DeleteItem(int id) => BaseUrl + $"/{id}";
    }

    public async Task<int> GetExistentCategoryId()
    {
        await using var context = await DbContext();
        if (!context.Categories.Any())
        {
            context.Categories.Add(
                new Category { Title = "Test" });
            context.SaveChanges();
        }

        return context.Categories.First().Id;
    }

    public async Task<int> GetNonExistentCategoryId()
    {
        await using var context = await DbContext();

        return context.Categories.Max(x => x.Id) + 1; // what if there is no categories?
    }

    public async Task<int> GetExistentBrandId()
    {
        await using var context = await DbContext();
        if (!context.Brands.Any())
        {
            context.Brands.Add(
                new Brand { Name = "Test" });
            context.SaveChanges();
        }

        return context.Brands.First().Id;
    }

    public async Task<int> GetNonExistentBrandId()
    {
        await using var context = await DbContext();

        return context.Brands.Max(x => x.Id) + 1; // what if there is no brands?
    }
}
