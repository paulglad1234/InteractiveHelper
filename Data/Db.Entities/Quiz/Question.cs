namespace InteractiveHelper.Db.Entities.Quiz;

public class Question : BaseEntity
{
    public string Text { get; set; }
    
    public int? NextId { get; set; }
    public virtual Question Previous { get; set; }
    public virtual Question Next { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }
    public virtual Quiz Quiz { get; set; }

    public virtual ICollection<ResultNode> Nodes { get; set; }
}
