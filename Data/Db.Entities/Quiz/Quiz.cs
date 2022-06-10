namespace InteractiveHelper.Db.Entities.Quiz;

public class Quiz : BaseEntity
{
    public string Url { get; set; }
    public string Title { get; set; }
    public string HelloMessage { get; set; }

    public int? HeadId { get; set; }
    public virtual Question Head { get; set; }
    public int? RootId { get; set; }
    public virtual ResultNode Root { get; set; }
}
