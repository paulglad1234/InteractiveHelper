namespace InteractiveHelper.Db.Entities.TheTest;

public class Answer : BaseEntity
{
    public string Text { get; set; }

    //public int? Metric { get; set; }

    public int QuestionId { get; set; }
    public virtual Question Question { get; set; }

    public virtual ICollection<Result> Results { get; set; }
}
