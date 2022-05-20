namespace InteractiveHelper.CatalogAdminPanel;

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ItemService : IItemService
{
    private readonly HttpClient _httpClient;

    public ItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ItemOutModel>> GetItems(int offset = 0, int limit = 10)
    {
        string url = $"{Settings.ApiRoot}/items?offset={offset}&limit={limit}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<IEnumerable<ItemOutModel>>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ItemOutModel>();

        return data;
    }

    public async Task<ItemOutModel> GetItem(int itemId)
    {
        string url = $"{Settings.ApiRoot}/items/{itemId}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<ItemOutModel>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ItemOutModel();

        return data;
    }

    public async Task AddItem(ItemInModel model)
    {
        string url = $"{Settings.ApiRoot}/items";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, request);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }    
    
    public async Task UpdateItem(int itemId, ItemInModel model)
    {
        string url = $"{Settings.ApiRoot}/items/{itemId}";

        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(url, request);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteItem(int itemId)
    {
        string url = $"{Settings.ApiRoot}/items/{itemId}";

        var response = await _httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task<IEnumerable<BrandModel>> GetBrandsList()
    {
        string url = $"{Settings.ApiRoot}/brands";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<IEnumerable<BrandModel>>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<BrandModel>();

        return data;
    }

    public async Task<IEnumerable<CategoryModel>> GetCategoriesList()
    {
        string url = $"{Settings.ApiRoot}/categories";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var data = JsonSerializer.Deserialize<IEnumerable<CategoryModel>>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CategoryModel>();

        return data;
    }
}
