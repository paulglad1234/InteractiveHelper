namespace InteractiveHelper.CatalogAdminPanel;

public class ItemOutModel
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public string Brand { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public float Price { get; set; }
    public byte[] Image { get; set; }
}
