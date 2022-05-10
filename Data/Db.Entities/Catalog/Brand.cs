namespace InteractiveHelper.Db.Entities.Catalog;

public class Brand : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Item> Items { get; set; }
}
