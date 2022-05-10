namespace InteractiveHelper.Db.Entities.TheTest;

/// <summary>
/// Don't get confused. It's not some unit or component test. It's THE test
/// </summary>
public class TheTest : BaseEntity
{
    public bool IsActive { get; set; } = false;

    public virtual ICollection<Question> Questions { get; set; }

    public virtual ICollection<Result> Results { get; set; }
}
