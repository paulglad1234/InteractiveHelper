using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.Db.Entities.TheTest;

public class Result : BaseEntity
{
    public string Conclusion { get; set; }

    public int TestId { get; set; }
    public virtual TheTest Test { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }
    public virtual ICollection<Item> Items { get; set; }
}
