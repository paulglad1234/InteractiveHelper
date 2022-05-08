namespace InteractiveHelper.Db.Entities;

public class ItemCharacteristic
{
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }
    public int CharacteristicId { get; set; }
    public virtual Characteristic Characteristic { get; set; }

    public string Value { get; set; }
}
