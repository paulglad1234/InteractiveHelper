namespace InteractiveHelper.Db.Entities.Quiz;

public class Quiz : BaseEntity
{
    public bool IsActive { get; set; } = false;

    public virtual ICollection<Question> Questions { get; set; }

    public virtual ICollection<Result> Results { get; set; }
}
