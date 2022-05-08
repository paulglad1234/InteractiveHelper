namespace InteractiveHelper.Db.Entities;

public class Item : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public byte[] Image { get; set; }

    public int BrandId { get; set; }
    public virtual Brand Brand { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public virtual ICollection<Characteristic> Characteristics { get; set; }
    public virtual ICollection<ItemCharacteristic> ItemCharacteristics { get; set; }
}
