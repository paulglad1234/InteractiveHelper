namespace InteractiveHelper.Db.Entities.Quiz;

public class Answer : BaseEntity
{
    public string Text { get; set; }

    public int QuestionId { get; set; }
    public virtual Question Question { get; set; }

    public virtual ICollection<ResultNode> Nodes { get; set; }
}
