namespace InteractiveHelper.Db.Entities.TheTest;

public class Question : BaseEntity
{
    public string Text { get; set; }

    public int? TestId { get; set; }
    public virtual TheTest Test { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }
}
