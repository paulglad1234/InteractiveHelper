namespace InteractiveHelper.Db.Entities.Catalog;

public class Category : BaseEntity
{
    public string Title { get; set; }

    public virtual ICollection<Item> Items { get; set; }

    public virtual ICollection<Characteristic> Characteristics { get; set; }
}
