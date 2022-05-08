namespace InteractiveHelper.Db.Entities;

public class Characteristic : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Item> Items { get; set; }
    public virtual ICollection<ItemCharacteristic> ItemCharacteristics { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
