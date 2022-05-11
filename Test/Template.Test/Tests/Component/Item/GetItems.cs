namespace InteractiveHelper.Test.Tests.Component.Item;

using InteractiveHelper.Api.Controllers.Catalog.Items.Models;
using InteractiveHelper.Test.Common.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

public partial class ItemIntegrationTest
{
    [Test]
    public async Task GetItems_ValidParameters_OkResponse()
    {
        var accessToken = await AuthenticateUser_EmptyScope();
        var url = Urls.GetItems();

        var response = await apiClient.Get(url, accessToken);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var apiItems = await response.ReadAsObject<IEnumerable<ItemResponse>>();

        await using var context = await DbContext();
        var dbItems = context.Items.Skip(0).Take(10);

        Assert.AreEqual(dbItems.Count(), apiItems.Count());
    }

    [Test]
    public async Task GetItems_NegativeParameters_OkResponse()
    {
        var accessToken = await AuthenticateUser_EmptyScope();
        var url = Urls.GetItems(-1, -1);

        var response = await apiClient.Get(url, accessToken);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var apiItems = await response.ReadAsObject<IEnumerable<ItemResponse>>();

        Assert.AreEqual(0, apiItems.Count());
    }
}
