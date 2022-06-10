using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.Db.Entities.Quiz;

public class ResultNode : BaseEntity
{
    public int? QuestionId { get; set; }
    public virtual Question Question { get; set; }
    public int? AnswerId { get; set; }
    public virtual Answer Answer { get; set; }

    public int? ParentId { get; set; }
    public virtual ResultNode Parent { get; set; }
    public virtual ICollection<ResultNode> Children { get; set; }

    public virtual ICollection<Item> Items { get; set; }
    public virtual Quiz Quiz { get; set; }
}
